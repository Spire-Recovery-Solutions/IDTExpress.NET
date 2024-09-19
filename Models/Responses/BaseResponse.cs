using System.Text.Json.Serialization;

namespace IDTExpress.NET.Models.Responses
{
    /// <summary>
    /// Base class for API responses that include metadata.
    /// </summary>
    public abstract class BaseResponse
    {
        /// <summary>
        /// Metadata about the response.
        /// </summary>
        [JsonPropertyName("meta")]
        public Meta Meta { get; set; }
    }
}
