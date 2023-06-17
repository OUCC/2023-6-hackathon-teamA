using UnityEngine.Networking;
using Newtonsoft.Json;

namespace HackathonA
{
    public class ChatGPTConnection
    {
        private JsonSerializerSettings settings = new();
        private string apiKey;


        public ChatGPTConnection(string apiKey)
        {
            this.apiKey = apiKey;
            settings.NullValueHandling = NullValueHandling.Ignore;
        }

        public RequestHandler CreateCompletionRequest(ChatGPTDatas.RequestData requestData, string url)
        {
            var json = JsonConvert.SerializeObject(requestData, settings);

            byte[] data = System.Text.Encoding.UTF8.GetBytes(json);

            var request = new UnityWebRequest(url, "POST")
            {
                uploadHandler = new UploadHandlerRaw(data),
                downloadHandler = new DownloadHandlerBuffer()
            };
            request.SetRequestHeader("Authorization", $"Bearer {this.apiKey}");
            request.SetRequestHeader("Content-Type", "application/json");

            return new RequestHandler(request);
        }
    }
}
