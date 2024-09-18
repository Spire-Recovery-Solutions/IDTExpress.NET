using System.Text.Json.Serialization;

namespace IDTExpress.NET.Models.Responses
{
    public class DeleteNumberResponse
    {
        [JsonPropertyName("number")]
        public string Number { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }
    }
}
