﻿using System.Text.Json.Serialization;

namespace IDTExpress.NET;

[JsonSourceGenerationOptions(WriteIndented = true, PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase)]
[JsonSerializable(typeof(CountryCoverage[]))]
[JsonSerializable(typeof(ErrorResponse))]
[JsonSerializable(typeof(DidGroupsResponse))]
[JsonSerializable(typeof(BrowseAvailableNumbersResponse))]
[JsonSerializable(typeof(CreateOrderRequest))]
[JsonSerializable(typeof(CreateOrderResponse))]
internal partial class IdtExpressJsonSerializerContext : JsonSerializerContext { }