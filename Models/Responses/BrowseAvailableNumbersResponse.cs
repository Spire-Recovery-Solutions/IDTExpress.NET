using System.Text.Json.Serialization;

namespace IDTExpress.NET.Models.Responses
{
    /// <summary>
    /// Represents the response from the Browse Available Numbers endpoint.
    /// </summary>
    public class BrowseAvailableNumbersResponse
    {
        /// <summary>
        /// List of available DID numbers and their corresponding SKUs.
        /// </summary>
        [JsonPropertyName("numbers")]
        public List<OrderedNumber> Numbers { get; set; }
    }
}
