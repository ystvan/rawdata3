using System;

namespace RDJTPClient
{
    public class Request
    {
        public string Method { get; set; }
        public string Path { get; set; }
        public string Date => DateTimeOffset.Now.ToUnixTimeSeconds().ToString();
        public string Body { get; set; }
    }
}
