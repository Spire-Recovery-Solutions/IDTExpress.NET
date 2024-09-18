using System.Text.Json.Serialization;

namespace IDTExpress.NET.Models.Responses
{
    /// <summary>
    /// Represents a response containing a list of phone numbers and associated metadata.
    /// </summary>
    public class NumberResponse
    {
        /// <summary>
        /// Gets or sets the list of phone numbers included in the response.
        /// </summary>
        [JsonPropertyName("numbers")]
        public List<Number> Numbers { get; set; }

        /// <summary>
        /// Gets or sets metadata about the pagination of the response.
        /// </summary>
        [JsonPropertyName("meta")]
        public Meta Meta { get; set; }
    }

    /// <summary>
    /// Represents a phone number and its details.
    /// </summary>
    public class Number
    {
        /// <summary>
        /// Gets or sets the phone number.
        /// </summary>
        [JsonPropertyName("number")]
        public string NumberValue { get; set; }

        /// <summary>
        /// Gets or sets the status of the phone number.
        /// </summary>
        [JsonPropertyName("status")]
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the phone number was added.
        /// </summary>
        [JsonPropertyName("added_at")]
        public DateTime AddedAt { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the phone number was removed, if applicable.
        /// This property is nullable, as the phone number might not have been removed yet.
        /// </summary>
        [JsonPropertyName("removed_at")]
        public DateTime? RemovedAt { get; set; }

        /// <summary>
        /// Gets or sets the DID group associated with the phone number.
        /// </summary>
        [JsonPropertyName("did_group")]
        public DidGroup DidGroup { get; set; }
    }
}