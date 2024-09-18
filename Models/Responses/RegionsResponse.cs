using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace IDTExpress.NET.Models.Responses
{
    /// <summary>
    /// Represents a response containing a list of regions and associated metadata.
    /// </summary>
    public class RegionsResponse : BaseResponse
    {
        /// <summary>
        /// Gets or sets the list of regions included in the response.
        /// This property is nullable, as it may not be present in every response.
        /// </summary>
        [JsonPropertyName("regions")]
        public List<Region>? Regions { get; set; }
    }

    /// <summary>
    /// Represents a region with its details.
    /// </summary>
    public class Region
    {
        /// <summary>
        /// Gets or sets the name of the region.
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the code of the region.
        /// </summary>
        [JsonPropertyName("code")]
        public string Code { get; set; }
    }
}