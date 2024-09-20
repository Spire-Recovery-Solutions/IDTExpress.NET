using IDTExpress.NET.Models.Requests;
using IDTExpress.NET.Models.Responses;
using IDTExpress.NET.Models.Responses.Enums;
using System.Text.Json.Serialization;

namespace IDTExpress.NET;

// Set serializer options like indentation and camel case naming
[JsonSourceGenerationOptions(WriteIndented = true, PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase)]

// Request models
[JsonSerializable(typeof(OrderItem))]
[JsonSerializable(typeof(CreateOrderRequest))]

// Response models
[JsonSerializable(typeof(CountryCoverageResponse))]
[JsonSerializable(typeof(DeleteNumberResponse))]
[JsonSerializable(typeof(ErrorResponse))]
[JsonSerializable(typeof(DidGroupsResponse))]
[JsonSerializable(typeof(BrowseAvailableNumbersResponse))]
[JsonSerializable(typeof(OrderResponse))]
[JsonSerializable(typeof(OrdersResponse))]
[JsonSerializable(typeof(NumberResponse))]

// Entities used in responses
[JsonSerializable(typeof(CountryCoverage))]
[JsonSerializable(typeof(DidGroup))]
[JsonSerializable(typeof(DidGroupCountry))]
[JsonSerializable(typeof(DidGroupRegion))]
[JsonSerializable(typeof(DidGroupFees))]
[JsonSerializable(typeof(Region))]
[JsonSerializable(typeof(RegionsResponse))]
[JsonSerializable(typeof(Order))]
[JsonSerializable(typeof(OrderItemResponse))]
[JsonSerializable(typeof(OrderItemDetail))]
[JsonSerializable(typeof(OrderQuantity))]
[JsonSerializable(typeof(Number))]
[JsonSerializable(typeof(OrderedNumber))]

// Error handling related
[JsonSerializable(typeof(ErrorDetail))]
[JsonSerializable(typeof(Meta))]

//Enums
[JsonSerializable(typeof(ErrorCode))]
[JsonSerializable(typeof(OrderItemStatus))]
internal partial class IdtExpressJsonSerializerContext : JsonSerializerContext { }
