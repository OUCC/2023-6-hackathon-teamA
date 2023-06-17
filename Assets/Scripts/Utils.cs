using System;
using System.Collections.Generic;

namespace HackathonA
{
    [Serializable]
    public class RequestData
    {
        public string model = "gpt-3.5-turbo";
        public List<Message> messages;
        public float? temperature = null; // [0.0 - 2.0]
        public float? top_p = null;
        public int? n = null;
        public bool? stream = null;
        public List<string> stop = null;
        public int? max_tokens = null;
        public float? presence_penalty = null;
        public float? frequency_penalty = null;
        public Dictionary<int, int> logit_bias = null;
        public string user = null;
    }

    [Serializable]
    public class Message
    {
        public string role;
        public string content;
    }

    [Serializable]
    public class Usage
    {
        public int prompt_tokens;
        public int completion_tokens;
        public int total_tokens;
    }

    [Serializable]
    public class Choice
    {
        public Message message;
        public string finish_reason;
        public int index;
    }

    [Serializable]
    public class ResponseData
    {
        public string id;
        public string @object;
        public int created;
        public string model;
        public Usage usage;
        public List<Choice> choices;
    }
}
