using System.Text.Json.Serialization;

namespace IDTExpress.NET;

[JsonSourceGenerationOptions(WriteIndented = true, PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase)]
[JsonSerializable(typeof(CountryCoverage[]))]
[JsonSerializable(typeof(ErrorResponse))]
[JsonSerializable(typeof(ApiResponse<CountryCoverage[]>))]
[JsonSerializable(typeof(ApiResponse<DidGroupsResponse>))]
[JsonSerializable(typeof(ApiResponse<BrowseAvailableNumbersResponse>))]
[JsonSerializable(typeof(CreateOrderRequest))]
[JsonSerializable(typeof(ApiResponse<CreateOrderResponse>))]
internal partial class IdtExpressJsonSerializerContext : JsonSerializerContext { }