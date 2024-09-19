using System.Text.Json.Serialization;

namespace IDTExpress.NET.Models.Responses
{
    /// <summary>
    /// Represents metadata about pagination in a paginated response.
    /// </summary>
    public class Meta
    {
        /// <summary>
        /// Gets or sets the current page number in the paginated response.
        /// </summary>
        [JsonPropertyName("page")]
        public int? Page { get; set; }

        /// <summary>
        /// Gets or sets the number of items per page in the paginated response.
        /// </summary>
        [JsonPropertyName("page_size")]
        public int? PageSize { get; set; }

        /// <summary>
        /// Gets or sets the total number of items available across all pages.
        /// </summary>
        [JsonPropertyName("total")]
        public int Total { get; set; }
    }
}