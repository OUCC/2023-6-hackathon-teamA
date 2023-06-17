using System.Collections.Generic;
using UnityEngine.Networking;
using Newtonsoft.Json;

namespace HackathonA
{
    public partial class ChatGPTConnection
    {
        JsonSerializerSettings settings = new();
        string apiKey;


        public ChatGPTConnection(string apiKey)
        {
            this.apiKey = apiKey;
            settings.NullValueHandling = NullValueHandling.Ignore;
        }

        public RequestHandler CreateCompletionRequest(RequestData requestData, string url)
        {
            var json = JsonConvert.SerializeObject(requestData, settings);

            byte[] data = System.Text.Encoding.UTF8.GetBytes(json);

            var request = new UnityWebRequest(url, "POST");
            request.SetRequestHeader("Authorization", $"Bearer {this.apiKey}");
            request.SetRequestHeader("Content-Type", "application/json");
            request.uploadHandler = new UploadHandlerRaw(data);
            request.downloadHandler = new DownloadHandlerBuffer();

            return new RequestHandler(request);
        }
    }
}
