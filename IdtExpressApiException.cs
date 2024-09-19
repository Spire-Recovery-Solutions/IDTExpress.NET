using IDTExpress.NET.Models.Responses;
using System.Net;

namespace IDTExpress.NET
{
    /// <summary>
    /// Represents an exception thrown by the IDT Express API.
    /// This exception includes details about the error response and HTTP status code.
    /// </summary>
    public class IdtExpressApiException : Exception
    {
        /// <summary>
        /// Gets the error response object containing detailed error information from the IDT Express API.
        /// </summary>
        public ErrorResponse ErrorResponse { get; }

        /// <summary>
        /// Gets the HTTP status code associated with the error response.
        /// </summary>
        public HttpStatusCode StatusCode { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="IdtExpressApiException"/> class with an error response and HTTP status code.
        /// </summary>
        /// <param name="errorResponse">The error response object from the IDT Express API.</param>
        /// <param name="statusCode">The HTTP status code associated with the error.</param>
        public IdtExpressApiException(ErrorResponse errorResponse, HttpStatusCode statusCode)
            : base(errorResponse?.Errors?[0]?.Detail ?? "An error occurred with the IDT Express API")
        {
            ErrorResponse = errorResponse;
            StatusCode = statusCode;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="IdtExpressApiException"/> class with a custom message.
        /// </summary>
        /// <param name="message">The custom error message.</param>
        public IdtExpressApiException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="IdtExpressApiException"/> class with a custom message, detailed error information, and HTTP status code.
        /// </summary>
        /// <param name="message">The custom error message.</param>
        /// <param name="detail">The detailed error description.</param>
        /// <param name="statusCode">The HTTP status code associated with the error.</param>
        public IdtExpressApiException(string message, string detail, HttpStatusCode statusCode) : base(message)
        {
            ErrorResponse = new ErrorResponse
            {
                Errors = new[] { new ErrorDetail { Detail = detail } }
            };
            StatusCode = statusCode;
        }
    }
}