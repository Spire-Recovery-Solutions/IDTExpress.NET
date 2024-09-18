using System.Text.Json.Serialization;

namespace IDTExpress.NET.Models.Responses
{

    public class RegionsResponse
    {
        [JsonPropertyName("regions")]
        public List<Region>? Regions { get; set; }

        [JsonPropertyName("meta")]
        public Meta Meta { get; set; }
    }

    public class Region
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("code")]
        public string Code { get; set; }
    }
}
