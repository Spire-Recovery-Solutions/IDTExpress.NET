using System.Text.Json.Serialization;

namespace IDTExpress.NET.Models.Responses.Enums
{
    /// <summary>
    /// Enum representing various error codes and their descriptions.
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter<ErrorCode>))]
    public enum ErrorCode
    {
        // General errors
        InvalidRequest = 1000, // Used when a more specific error code is not defined
        RequiredFieldMissing = 1001, // A required field or parameter is missing
        InvalidField = 1002, // A validation error on a field or parameter
        ResourceNotFound = 1003, // A request to a resource or endpoint that does not exist

        // Access and quota errors
        AccessDenied = 2000, // Request access is denied
        QuotaExceeded = 2001, // Quota exceeded allotment for the account
        Throttled = 2002, // Throttling request limit exceeded for the account

        // DID-related errors
        DidsFeatureNotEnabled = 3000, // Account no longer has access to purchase DIDs/numbers
        InsufficientFunds = 3001, // Insufficient funds available on the account
        TollFreeNumberNotOffered = 4000, // Toll-Free numbers not available in the region or country
        NumberNotFound = 4001, // The number does not belong to the account or is not found
        RegionNotOffered = 4002, // DID Groups are not offered in the region
        CountryNotOffered = 4003, // DID Groups are not offered in the country
        InvalidDidGroup = 4004 // A request to order from a DID Group that is not offered
    }

}
