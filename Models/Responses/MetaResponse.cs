using System.Text.Json.Serialization;

namespace IDTExpress.NET.Models.Responses
{

    public class Meta
    {
        [JsonPropertyName("page")]
        public int? Page { get; set; }

        [JsonPropertyName("page_size")]
        public int? PageSize { get; set; }

        [JsonPropertyName("total")]
        public int Total { get; set; }
    }
}
