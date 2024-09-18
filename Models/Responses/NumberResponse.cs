using System.Text.Json.Serialization;

namespace IDTExpress.NET.Models.Responses
{
    public class NumberResponse
    {
        [JsonPropertyName("numbers")]
        public List<Number> Numbers { get; set; }

        [JsonPropertyName("meta")]
        public Meta Meta { get; set; }
    }

    public class Number
    {
        [JsonPropertyName("number")]
        public string NumberValue { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("added_at")]
        public DateTime AddedAt { get; set; }

        [JsonPropertyName("removed_at")]
        public DateTime? RemovedAt { get; set; }

        [JsonPropertyName("did_group")]
        public DidGroup DidGroup { get; set; }
    }

}
