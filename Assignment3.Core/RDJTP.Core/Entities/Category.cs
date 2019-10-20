using System.Text.Json.Serialization;

namespace RDJTP.Core
{
    public class Category
    {
        [JsonPropertyName("cid")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}
