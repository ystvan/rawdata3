using System.Text.Json.Serialization;

namespace RDJTP.Core
{
    public class Response
    {
        [JsonPropertyName("status")]
        public string Status { get; set; } = string.Empty;

        [JsonPropertyName("body")]
        public string Body { get; set; } 
    }
}
