using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace RDJTP.Core
{
    public class Response
    {
        [JsonPropertyName("status")]
        public List<string> Status { get; set; }

        [JsonPropertyName("body")]
        public string Body { get; set; }
    }
}
