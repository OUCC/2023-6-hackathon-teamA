using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using HackathonA.ChatGPTDatas;

namespace HackathonA
{
    public class EnemyAI
    {
        public int DefaultActionOnError { get; set; } = 0;

        private const string API_URL = "https://api.openai.com/v1/chat/completions";
        private readonly string _apiKey;
        private readonly ChatGPTConnection _chatGPTConnection;

        // ChatGPTに入力するメッセージのリスト（過去の内容も一緒に渡すためリスト）
        private List<Message> _messages;

        public EnemyAI(string apiKey)
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
            return @$"Please answer with an integer from 0 to 4 for your action.

You can take the following actions.
0: Physical attack. 20 to 30 damage
1: Magic attack. 20 to 30 damage
2: Recovery. 10 to 15 recovery
3: Physical Counter. bounce back if opponen's action is Physical attack
4: Magic Counter. bounce back if opponent's action is Magic attack

Input example: Your HP is currently 1000 and your opponent's HP is 1000.
               Choose from the options above and respond with the corresponding number.
Output example: 0";
        }

        /// <summary>
        /// ChatGPTとの通信を開始
        /// </summary>
        /// <param name="playerHP">プレイヤーのHP</param>
        /// <param name="enemyHP">敵のHP</param>
        /// <returns>アクションのタイプ(0-4)。エラー時にはDefaultActionOnErrorで設定した値が返ってくる</returns>
        public async UniTask<int> GetEnemyActionAsync(int playerHP, int enemyHP, int playerAction)
        {
            string currentMessage;
            if (playerAction == -1)
            {
                currentMessage = $"Currently, your HP is  {enemyHP} and your opponent's HP is {playerHP}.\nChoose from the options above and respond with the corresponding number.";
            }
            else
            {
                currentMessage = @$"Currently, your HP is  {enemyHP} and your opponent's HP is {playerHP}.
Your opponent did the {playerAction switch
                {
                    0 => "physical attack",
                    1 => "magic attack",
                    2 => "recovery",
                    3 => "physical counter",
                    4 => "magic counter",
                    _ => throw new ArgumentOutOfRangeException()
                }}before.
Choose from the options above and respond with the corresponding number.
Also, your response have to be only figure. You can not respond anything other than figure.";
            }
            DebugLoger.Log(currentMessage);
            _messages.Add(new Message() { role = "user", content = currentMessage });

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
