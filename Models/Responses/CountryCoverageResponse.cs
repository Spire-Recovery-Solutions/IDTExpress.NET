using System.Text.Json.Serialization;

namespace IDTExpress.NET.Models.Responses
{
    public class CountryCoverageResponse
    {
        [JsonPropertyName("countries")]
        public List<CountryCoverage> Countries { get; set; }
    }

    public class CountryCoverage
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("iso")]
        public string Iso { get; set; }

        [JsonPropertyName("has_regions")]
        public bool HasRegions { get; set; }

        [JsonPropertyName("supports_toll_free")]
        public bool SupportsTollFree { get; set; }
    }
}
