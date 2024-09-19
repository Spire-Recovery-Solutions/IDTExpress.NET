using IDTExpress.NET.Models.Requests;
using IDTExpress.NET.Models.Responses;
using System.Text.Json.Serialization;

namespace IDTExpress.NET;

[JsonSourceGenerationOptions(WriteIndented = true, PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase)]
[JsonSerializable(typeof(CountryCoverage[]))]
[JsonSerializable(typeof(ErrorResponse))]
[JsonSerializable(typeof(DidGroupsResponse))]
[JsonSerializable(typeof(BrowseAvailableNumbersResponse))]
[JsonSerializable(typeof(CreateOrderRequest))]
[JsonSerializable(typeof(OrderResponse))]
internal partial class IdtExpressJsonSerializerContext : JsonSerializerContext { }