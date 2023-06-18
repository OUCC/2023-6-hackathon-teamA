using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using HackathonA.ChatGPTDatas;
using UnityEngine;

namespace HackathonA
{
    public class EnemyBehaviour
    {
        public int DefaultPhysicalAttack { get; set; } = 20;
        public int DefaultMagicAttack { get; set; } = 20;
        public int DefaultRecoveryPoint { get; set; } = 15;
        public int DefaultActionOnError { get; set; } = 0;

        private const string API_URL = "https://api.openai.com/v1/chat/completions";
        private readonly string _apiKey;
        private readonly ChatGPTConnection _chatGPTConnection;

        // ChatGPTに入力するメッセージのリスト（過去の内容も一緒に渡すためリスト）
        private List<Message> _messages;

        public EnemyBehaviour(string apiKey)
        {
            _apiKey = apiKey;
            _messages = new List<Message>()
            {
                new Message(){role = "system", content = GenerateSystemMessage()},
            };
            _chatGPTConnection = new ChatGPTConnection(_apiKey, API_URL);
        }

        private string GenerateSystemMessage()
        {
            return GenerateSystemMessage(DefaultPhysicalAttack, DefaultMagicAttack, DefaultRecoveryPoint);
        }

        private string GenerateSystemMessage(int physicalAttack, int mamgicAttack, int recoveryPoint)
        {
            return @$"Please answer with an integer from 0-4 for your action

You can take the following actions
0: Physical attack. {physicalAttack} damage
1: Magic attack. {mamgicAttack} damage
2: Recovery. {recoveryPoint} recovery
3: Physical Counter. bounce back if opponen's action is Physical attack
4: Magic counter. bounce back if opponent's action is Magic attack

Input example: Your HP is currently 1000 and your opponent's HP is 1000
Output example: 0";
        }

        // 敵の設定
        public void Set(int physicalAttack, int mamgicAttack, int recoveryPoint)
        {
            _messages = new List<Message>()
            {
                new Message(){role = "system", content = GenerateSystemMessage(physicalAttack, mamgicAttack, recoveryPoint)},
            };
        }

        /// <summary>
        /// ChatGPTとの通信を開始
        /// </summary>
        /// <param name="playerHP">プレイヤーのHP</param>
        /// <param name="enemyHP">敵のHP</param>
        /// <returns>アクションのタイプ(0-4)。エラー時にはDefaultActionOnErrorで設定した値が返ってくる</returns>
        public async UniTask<int> GetEnemyActionAsync(int playerHP, int enemyHP)
        {
            string currentMessage = $"Currently, your HP is  {enemyHP} and your opponent's HP is {playerHP}";
            DebugLoger.Log(currentMessage);
            _messages.Add(new Message() { role = "system", content = currentMessage });

            using var request = _chatGPTConnection.CreateCompletionRequest(new RequestData() { messages = _messages });

            await request.SendAsync();

            // エラーがあった場合は、それをコンソールに出力
            if (request.IsError)
            {
                DebugLoger.LogError(request.Error);
                return DefaultActionOnError;
            }
            else
            {
                var responseMessage = request.Response.choices[0].message.content;
                DebugLoger.Log($"ChatGPT replied '{responseMessage}'");

                // 文字列を数値に変換
                if (int.TryParse(responseMessage[..1], out var action)) 
                {
                    if (0 <= action && action <= 4)
                    {
                        return action;
                    }
                    else
                    {
                        return DefaultActionOnError;
                    }
                }
                else
                {

                    DebugLoger.LogError($"Unable to parse '{responseMessage}'");
                    return DefaultActionOnError;
                }
            }
        }
    }
}
