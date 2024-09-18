using IDTExpress.NET.Models.Responses.Enums;
using System.Text.Json.Serialization;

namespace IDTExpress.NET.Models.Responses
{
    /// <summary>
    /// Represents the error response returned by the API when a request fails.
    /// </summary>
    public class ErrorResponse
    {
        /// <summary>
        /// Gets or sets the HTTP status code applicable to this problem.
        /// </summary>
        [JsonPropertyName("status")]
        public int Status { get; set; }

        /// <summary>
        /// Gets or sets a unique identifier for the API request, useful for troubleshooting.
        /// </summary>
        [JsonPropertyName("api_request_id")]
        public string ApiRequestId { get; set; }

        /// <summary>
        /// Gets or sets an array of error details that describe what went wrong.
        /// </summary>
        [JsonPropertyName("errors")]
        public ErrorDetail[] Errors { get; set; }
    }

    /// <summary>
    /// Represents detailed information about a specific error encountered.
    /// </summary>
    public class ErrorDetail
    {
        /// <summary>
        /// Gets or sets the application-specific error code representing the type of problem encountered.
        /// </summary>
        [JsonPropertyName("code")]
        public ErrorCode Code { get; set; }

        /// <summary>
        /// Gets or sets a short, human-readable summary of the problem.
        /// </summary>
        [JsonPropertyName("title")]
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets a human-readable explanation specific to this occurrence of the problem.
        /// </summary>
        [JsonPropertyName("detail")]
        public string Detail { get; set; }

        /// <summary>
        /// Gets or sets the field or parameter name that is causing the issue (if applicable).
        /// </summary>
        [JsonPropertyName("field")]
        public string Field { get; set; }

        /// <summary>
        /// Gets or sets a JSON Pointer to the associated entity in the request body (if applicable).
        /// </summary>
        [JsonPropertyName("source")]
        public string Source { get; set; }
    }
}