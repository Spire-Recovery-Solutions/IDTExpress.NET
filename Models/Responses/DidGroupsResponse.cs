using System.Text.Json.Serialization;

namespace IDTExpress.NET.Models.Responses
{
    /// <summary>
    /// Represents the response from the Get DID Groups endpoint.
    /// </summary>
    public class DidGroupsResponse
    {
        /// <summary>
        /// List of DID Groups returned by the API.
        /// </summary>
        [JsonPropertyName("did_groups")]
        public List<DidGroup> DidGroups { get; set; }

        /// <summary>
        /// Metadata about the response.
        /// </summary>
        [JsonPropertyName("meta")]
        public Meta Meta { get; set; }
    }

    /// <summary>
    /// Represents a single DID Group.
    /// </summary>
    public class DidGroup
    {
        /// <summary>
        /// The unique identifier for the DID Group.
        /// </summary>
        [JsonPropertyName("id")]
        public int Id { get; set; }

        /// <summary>
        /// The name of the DID Group.
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }

        /// <summary>
        /// The international calling code.
        /// </summary>
        [JsonPropertyName("country_calling_code")]
        public string CountryCallingCode { get; set; }

        /// <summary>
        /// The area code/NPA.
        /// </summary>
        [JsonPropertyName("area_code")]
        public string AreaCode { get; set; }

        /// <summary>
        /// The NXX. Only applicable for North American numbers.
        /// </summary>
        [JsonPropertyName("nxx")]
        public string Nxx { get; set; }

        /// <summary>
        /// Indicates if the DID Group is Toll-Free.
        /// </summary>
        [JsonPropertyName("toll_free")]
        public bool TollFree { get; set; }

        /// <summary>
        /// Indicates if the DID Group supports browsing available DID numbers.
        /// </summary>
        [JsonPropertyName("supports_browse")]
        public bool SupportsBrowse { get; set; }

        /// <summary>
        /// Object representing the country the DID Group belongs to.
        /// </summary>
        [JsonPropertyName("country")]
        public DidGroupCountry Country { get; set; }

        /// <summary>
        /// Object representing the region the DID Group belongs to. This will only be present if there is a region.
        /// </summary>
        [JsonPropertyName("region")]
        public DidGroupRegion Region { get; set; }

        /// <summary>
        /// Fees associated with this DID Group. These values are in the currency of your account.
        /// </summary>
        [JsonPropertyName("fees")]
        public DidGroupFees Fees { get; set; }

        /// <summary>
        /// The quantity of numbers available for purchase in this DID Group.
        /// </summary>
        [JsonPropertyName("quantity")]
        public int Quantity { get; set; }
    }

    /// <summary>
    /// Represents the country information for a DID Group.
    /// </summary>
    public class DidGroupCountry
    {
        /// <summary>
        /// Country name.
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }

        /// <summary>
        /// 2 character ISO 3166-1 alpha-2 country code.
        /// </summary>
        [JsonPropertyName("iso")]
        public string Iso { get; set; }

        /// <summary>
        /// Flag to indicate if the country supports regions.
        /// </summary>
        [JsonPropertyName("has_regions")]
        public bool HasRegions { get; set; }

        /// <summary>
        /// Flag to indicate if the country supports toll-free numbers.
        /// </summary>
        [JsonPropertyName("supports_toll_free")]
        public bool SupportsTollFree { get; set; }
    }

    /// <summary>
    /// Represents the region information for a DID Group.
    /// </summary>
    public class DidGroupRegion
    {
        /// <summary>
        /// The name of the region.
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }

        /// <summary>
        /// The ISO 3166-2 code that uniquely identifies the region.
        /// </summary>
        [JsonPropertyName("code")]
        public string Code { get; set; }
    }

    /// <summary>
    /// Represents the fees associated with a DID Group.
    /// </summary>
    public class DidGroupFees
    {
        /// <summary>
        /// Setup fee.
        /// </summary>
        [JsonPropertyName("setup_fee")]
        public string SetupFee { get; set; }

        /// <summary>
        /// The number's monthly fee.
        /// </summary>
        [JsonPropertyName("monthly_fee")]
        public string MonthlyFee { get; set; }

        /// <summary>
        /// The per minute rate.
        /// </summary>
        [JsonPropertyName("per_minute_rate")]
        public string PerMinuteRate { get; set; }
    }
}
