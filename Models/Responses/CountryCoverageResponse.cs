using System.Text.Json.Serialization;

namespace IDTExpress.NET.Models.Responses
{
    /// <summary>
    /// Represents the response containing a list of countries and their coverage details.
    /// </summary>
    public class CountryCoverageResponse
    {
        /// <summary>
        /// Gets or sets the list of countries with their coverage details.
        /// </summary>
        [JsonPropertyName("countries")]
        public List<CountryCoverage> Countries { get; set; }

        /// <summary>
        /// Metadata about the response.
        /// </summary>
        [JsonPropertyName("meta")]
        public Meta Meta { get; set; }
    }

    /// <summary>
    /// Represents the coverage details for a specific country.
    /// </summary>
    public class CountryCoverage
    {
        /// <summary>
        /// Gets or sets the name of the country.
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the ISO code of the country (e.g., "US" for United States).
        /// </summary>
        [JsonPropertyName("iso")]
        public string Iso { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the country has regional divisions for coverage.
        /// </summary>
        [JsonPropertyName("has_regions")]
        public bool HasRegions { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the country supports toll-free numbers.
        /// </summary>
        [JsonPropertyName("supports_toll_free")]
        public bool SupportsTollFree { get; set; }
    }
}
