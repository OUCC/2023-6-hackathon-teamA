using UnityEngine.Networking;
using Newtonsoft.Json;

namespace HackathonA
{
    public class ChatGPTConnection
    {
        private readonly JsonSerializerSettings settings = new();
        private readonly string apiKey;
        private readonly string apiUrl;


        public ChatGPTConnection(string apiKey, string apiUrl)
        {
            this.apiKey = apiKey;
            this.apiUrl = apiUrl;
            settings.NullValueHandling = NullValueHandling.Ignore;
        }

        public RequestHandler CreateCompletionRequest(ChatGPTDatas.RequestData requestData)
        {
            var json = JsonConvert.SerializeObject(requestData, settings);

            byte[] data = System.Text.Encoding.UTF8.GetBytes(json);

            var request = new UnityWebRequest(apiUrl, "POST")
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
