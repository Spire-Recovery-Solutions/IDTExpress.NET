using System.Net;

namespace IDTExpress.NET;

public class IdtExpressApiException : Exception
{
    public ErrorResponse ErrorResponse { get; }
    public HttpStatusCode StatusCode { get; }

    public IdtExpressApiException(ErrorResponse errorResponse, HttpStatusCode statusCode)
        : base(errorResponse?.Errors?[0]?.Detail ?? "An error occurred with the IDT Express API")
    {
        ErrorResponse = errorResponse;
        StatusCode = statusCode;
    }

    public IdtExpressApiException(string message) : base(message)
    {
    }

    public IdtExpressApiException(string message, string detail, HttpStatusCode statusCode) : base(message)
    {
        ErrorResponse = new ErrorResponse
        {
            Errors = new[] { new ErrorDetail { Detail = detail } }
        };
        StatusCode = statusCode;
    }
}