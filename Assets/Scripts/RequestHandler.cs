using UnityEngine.Networking;
using Newtonsoft.Json;
using Cysharp.Threading.Tasks;
using System;

namespace HackathonA
{
    public class RequestHandler: IDisposable
    {
        public bool IsCompleted { get; private set; }
        public bool IsError => Error != null;
        public string Error { get; private set; }
        public ChatGPTDatas.ResponseData Response { get; private set; }

        private UnityWebRequest request;

        public RequestHandler(UnityWebRequest request)
        {
            this.request = request;
        }

        public async UniTask SendAsync()
        {
            using (request)
            {
                try
                {

                    await request.SendWebRequest();

                    if (request.result != UnityWebRequest.Result.Success)
                    {
                        Error = "[ChatGPTConnection] " + request.error + "\n\n" + request.downloadHandler.text;
                    }
                    else
                    {
                        Response = JsonConvert.DeserializeObject<ChatGPTDatas.ResponseData>(request.downloadHandler.text);
                    }
                }
                catch (Exception ex)
                {
                    Error = ex.ToString();
                }
            }
        }

        public void Dispose()
        {
            request?.Dispose();
        }
    }
}
