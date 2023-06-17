using System;
using System.Collections;
using System.Collections.Generic;
using HackathonA.ChatGPTDatas;
using UnityEngine;

namespace HackathonA
{
    public class EnemyBehaviour : MonoBehaviour
    {
        public int DefaultPhysicalAttack = 20;
        public int DefaultMagicAttack = 20;
        public int DefaultRecoveryPoint = 15;
        public int DefaultActionOnError = 0;

        const string API_URL = "https://api.openai.com/v1/chat/completions";
        private string _apiKey;

        // ChatGPTに入力するメッセージのリスト（過去の内容も一緒に渡すためリスト）
        private List<Message> _messages;

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

        void Awake()
        {
            TextAsset config = Resources.Load("ApiKey") as TextAsset;
            _apiKey = config.text.Trim();
            _messages = new List<Message>()
            {
                new Message(){role = "system", content = GenerateSystemMessage()},
            };
        }

        // 敵の設定
        public void Set(int physicalAttack, int mamgicAttack, int recoveryPoint)
        {
            _messages = new List<Message>()
            {
                new Message(){role = "system", content = GenerateSystemMessage(physicalAttack, mamgicAttack, recoveryPoint)},
            };
        }

        public IEnumerator GetEnemyAction(int playerHP, int enemyHP, System.Action<int> callback)
        {
            var chatGPTConnection = new ChatGPTConnection(_apiKey);

            string currentMessage = $"Currently, your HP is  {enemyHP} and your opponent's HP is {playerHP}";
            Debug.Log(currentMessage);
            _messages.Add(new Message() { role = "system", content = currentMessage });

            var request = chatGPTConnection.CreateCompletionRequest(
                new RequestData() { messages = _messages },
                API_URL
            );

            yield return request.Send();

            // エラーがあった場合は、それをコンソールに出力
            if (request.IsError)
            {
                Debug.LogError(request.Error);
                callback(DefaultActionOnError);
            }
            else
            {
                var responseMessage = request.Response.choices[0].message.content;
                Debug.Log($"ChatGPT replied '{responseMessage}'");

                // 文字列を数値に変換
                try
                {
                    int action = int.Parse(responseMessage.Substring(0, 1));
                    // 数値に変換できれば、それをコールバック関数に渡す
                    if (0 <= action && action <= 4)
                    {
                        callback(action);
                    }
                    else
                    {
                        callback(DefaultActionOnError);
                    }
                }
                catch (FormatException)
                {
                    Debug.LogError($"Unable to parse '{responseMessage}'");
                    callback(DefaultActionOnError);
                }
            }
        }
    }
}
