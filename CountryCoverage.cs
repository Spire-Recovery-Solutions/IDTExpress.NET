using System.Text.Json.Serialization;

public class CountryCoverageResponse
{
    [JsonPropertyName("countries")]
    public List<CountryCoverage> Countries { get; set; }
}

public class CountryCoverage
{
    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("iso")]
    public string Iso { get; set; }

    [JsonPropertyName("has_regions")]
    public bool HasRegions { get; set; }

    [JsonPropertyName("supports_toll_free")]
    public bool SupportsTollFree { get; set; }
}

public class ApiResponse<T>
{
    public T Data { get; set; }
}

public class ErrorResponse
{
    public int Status { get; set; }
    public string ApiRequestId { get; set; }
    public ErrorDetail[] Errors { get; set; }
}

public class ErrorDetail
{
    public int Code { get; set; }
    public string Title { get; set; }
    public string Detail { get; set; }
    public string Field { get; set; }
    public string Source { get; set; }
}

/// <summary>
/// Represents the response from the Get DID Groups endpoint.
/// </summary>
public class DidGroupsResponse
{
    /// <summary>
    /// List of DID Groups returned by the API.
    /// </summary>
    public List<DidGroup> DidGroups { get; set; }

    /// <summary>
    /// Metadata about the response.
    /// </summary>
    public DidGroupsMeta Meta { get; set; }
}

/// <summary>
/// Represents a single DID Group.
/// </summary>
public class DidGroup
{
    /// <summary>
    /// The unique identifier for the DID Group. Use this when placing an order.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// The name of the DID Group.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// The international calling code.
    /// </summary>
    public string CountryCallingCode { get; set; }

    /// <summary>
    /// The area code/NPA.
    /// </summary>
    public string AreaCode { get; set; }

    /// <summary>
    /// The NXX. Only applicable for North American numbers.
    /// </summary>
    public string Nxx { get; set; }

    /// <summary>
    /// Indicates if the DID Group is Toll-Free.
    /// </summary>
    public bool TollFree { get; set; }

    /// <summary>
    /// Indicates if the DID Group supports browsing available DID numbers.
    /// </summary>
    public bool SupportsBrowse { get; set; }

    /// <summary>
    /// The quantity of numbers available for purchase in this DID Group.
    /// </summary>
    public int Quantity { get; set; }

    /// <summary>
    /// Object representing the country the DID Group belongs to.
    /// </summary>
    public DidGroupCountry Country { get; set; }

    /// <summary>
    /// Object representing the region the DID Group belongs to. This will only be present if there is a region.
    /// </summary>
    public DidGroupRegion Region { get; set; }

    /// <summary>
    /// Fees associated with this DID Group. These values are in the currency of your account.
    /// </summary>
    public DidGroupFees Fees { get; set; }
}

/// <summary>
/// Represents the country information for a DID Group.
/// </summary>
public class DidGroupCountry
{
    /// <summary>
    /// Country name.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 2 character ISO 3166-1 alpha-2 country code.
    /// </summary>
    public string Iso { get; set; }

    /// <summary>
    /// Flag to indicate if the country supports regions.
    /// </summary>
    public bool HasRegions { get; set; }

    /// <summary>
    /// Flag to indicate if the country supports toll-free numbers.
    /// </summary>
    public bool SupportsTollFree { get; set; }
}

/// <summary>
/// Represents the region information for a DID Group.
/// </summary>
public class DidGroupRegion
{
    /// <summary>
    /// The name of the region.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// The ISO 3166-2 code that uniquely identifies the region.
    /// </summary>
    public string Code { get; set; }
}

/// <summary>
/// Represents the fees associated with a DID Group.
/// </summary>
public class DidGroupFees
{
    /// <summary>
    /// Setup fee.
    /// </summary>
    public string SetupFee { get; set; }

    /// <summary>
    /// The number's monthly fee.
    /// </summary>
    public string MonthlyFee { get; set; }

    /// <summary>
    /// The per minute rate.
    /// </summary>
    public string PerMinuteRate { get; set; }
}

/// <summary>
/// Represents the metadata in the DID Groups response.
/// </summary>
public class DidGroupsMeta
{
    /// <summary>
    /// The total number of DID Groups returned.
    /// </summary>
    public int Total { get; set; }
}

/// <summary>
/// Represents the response from the Browse Available Numbers endpoint.
/// </summary>
public class BrowseAvailableNumbersResponse
{
    /// <summary>
    /// List of available DID numbers and their corresponding SKUs.
    /// </summary>
    public List<AvailableNumber> Numbers { get; set; }
}

/// <summary>
/// Represents an available DID number and its SKU.
/// </summary>
public class AvailableNumber
{
    /// <summary>
    /// A DID number available for purchase.
    /// </summary>
    public string Number { get; set; }

    /// <summary>
    /// The SKU to use when ordering this DID number.
    /// </summary>
    public string Sku { get; set; }
}

/// <summary>
/// Represents the request body for creating an order.
/// </summary>
public class CreateOrderRequest
{
    /// <summary>
    /// If true, the call does not create an order but still goes through all validations.
    /// </summary>
    public bool Preview { get; set; }

    /// <summary>
    /// The order items. Currently, only one order item is supported.
    /// </summary>
    public List<OrderItem> OrderItems { get; set; }
}

/// <summary>
/// Represents an order item in the create order request.
/// </summary>
public class OrderItem
{
    /// <summary>
    /// The DID Group ID that you want to order from.
    /// </summary>
    public int DidGroupId { get; set; }

    /// <summary>
    /// The amount of numbers to order. Maximum is 100.
    /// </summary>
    public int? Quantity { get; set; }

    /// <summary>
    /// The specific number SKUs to order.
    /// </summary>
    public List<string> DidSkus { get; set; }
}

/// <summary>
/// Represents the response from creating an order.
/// </summary>
public class CreateOrderResponse
{
    /// <summary>
    /// The created order details.
    /// </summary>
    public Order Order { get; set; }

    /// <summary>
    /// Additional metadata about the response.
    /// </summary>
    public object Meta { get; set; }
}

/// <summary>
/// Represents an order.
/// </summary>
public class Order
{
    /// <summary>
    /// The order's unique identifier.
    /// </summary>
    public string Id { get; set; }

    /// <summary>
    /// Date/Time that the order was created in ISO 8601 format.
    /// </summary>
    public string CreatedAt { get; set; }

    /// <summary>
    /// The status of the order.
    /// </summary>
    public string Status { get; set; }

    /// <summary>
    /// Object representing the quantity of what was ordered.
    /// </summary>
    public OrderQuantity Ordered { get; set; }

    /// <summary>
    /// Object representing the quantity of what was fulfilled.
    /// </summary>
    public OrderQuantity Fulfilled { get; set; }

    /// <summary>
    /// The order items.
    /// </summary>
    public List<OrderItemResponse> OrderItems { get; set; }
}

/// <summary>
/// Represents the quantity for an order.
/// </summary>
public class OrderQuantity
{
    /// <summary>
    /// The amount of numbers.
    /// </summary>
    public int Quantity { get; set; }
}

/// <summary>
/// Represents an order item in the response.
/// </summary>
public class OrderItemResponse
{
    /// <summary>
    /// Unique identifier for the order item.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// The status of this order item.
    /// </summary>
    public string Status { get; set; }

    /// <summary>
    /// The type of order item.
    /// </summary>
    public string OrderItemType { get; set; }

    /// <summary>
    /// Object representing the quantity and any specific numbers ordered for this order item.
    /// </summary>
    public OrderItemDetail Ordered { get; set; }

    /// <summary>
    /// Object representing the quantity of what was fulfilled for this order item.
    /// </summary>
    public OrderQuantity Fulfilled { get; set; }

    /// <summary>
    /// Indicates if this order item can be canceled.
    /// </summary>
    public bool Cancelable { get; set; }

    /// <summary>
    /// The DID Group that was ordered.
    /// </summary>
    public DidGroup DidGroup { get; set; }

    /// <summary>
    /// Any numbers that were added to the account as part of this order item.
    /// </summary>
    public List<OrderedNumber> Numbers { get; set; }
}

/// <summary>
/// Represents the details of an ordered item.
/// </summary>
public class OrderItemDetail
{
    /// <summary>
    /// The amount of numbers that were requested for this order item.
    /// </summary>
    public int Quantity { get; set; }

    /// <summary>
    /// The specific numbers that were requested for this order item.
    /// </summary>
    public List<OrderedNumber> Numbers { get; set; }
}

public class RegionsResponse
{
    [JsonPropertyName("regions")]
    public List<Region> Regions { get; set; }

    [JsonPropertyName("meta")]
    public Meta Meta { get; set; }
}

public class Region
{
    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("code")]
    public string Code { get; set; }
}

public class Meta
{
    [JsonPropertyName("total")]
    public int Total { get; set; }
}


/// <summary>
/// Represents an ordered number.
/// </summary>
public class OrderedNumber
{
    /// <summary>
    /// A specific number that was requested or added.
    /// </summary>
    public string Number { get; set; }

    /// <summary>
    /// A corresponding number SKU.
    /// </summary>
    public string Sku { get; set; }
}