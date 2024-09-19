using System.Text.Json.Serialization;

namespace IDTExpress.NET.Models.Responses.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter<OrderItemStatus>))]
    public enum OrderItemStatus
    {
        Created,
        Processing,
        Preview,
        Received,
        Shipped,
        Complete,
        FulfilledComplete,
        PartiallyFulfilledComplete,
        Backordered,
        BackorderCanceled,
        PartiallyFulfilledBackordered,
        PartiallyFulfilledBackorderCanceled,
        CanceledInsufficientFunds,
        CanceledInsufficientInventory,
        PartiallyFulfilledInsufficientFunds
    }
}
