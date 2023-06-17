using System.Collections;
using UnityEngine.Networking;
using Newtonsoft.Json;

namespace HackathonA
{
    public class RequestHandler
    {
        public bool IsCompleted { get; private set; }
        public bool IsError => Error != null;
        public string Error { get; private set; }
        public ResponseData Response { get; private set; }

        UnityWebRequest request;

        public RequestHandler(UnityWebRequest request)
        {
            this.request = request;
        }

        public IEnumerator Send()
        {
            using (request)
            {
                yield return request.SendWebRequest();

                if (request.result != UnityWebRequest.Result.Success)
                {
                    Error = "[ChatGPTConnection] " + request.error + "\n\n" + request.downloadHandler.text;
                }
                else
                {
                    Response = JsonConvert.DeserializeObject<ResponseData>(request.downloadHandler.text);
                }
            }
        }
    }
}
