using System.Text.Json.Serialization;

namespace IDTExpress.NET.Models.Responses
{
    /// <summary>
    /// Represents the response returned after attempting to delete a phone number.
    /// </summary>
    public class DeleteNumberResponse
    {
        /// <summary>
        /// Gets or sets the phone number that was attempted to be deleted.
        /// </summary>
        [JsonPropertyName("number")]
        public string Number { get; set; }

        /// <summary>
        /// Gets or sets the status of the delete operation.
        /// Possible values might include success, failure, or specific status messages.
        /// </summary>
        [JsonPropertyName("status")]
        public string Status { get; set; }
    }
}