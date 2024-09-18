using IDTExpress.NET.Models.Responses.Enums;
using System.Text.Json.Serialization;

namespace IDTExpress.NET.Models.Responses
{
    /// <summary>
    /// Represents a response containing a list of orders and associated metadata.
    /// </summary>
    public class OrdersResponse
    {
        /// <summary>
        /// Gets or sets the list of orders included in the response.
        /// </summary>
        [JsonPropertyName("orders")]
        public List<Order> Orders { get; set; }

        /// <summary>
        /// Gets or sets metadata about the pagination of the response.
        /// </summary>
        [JsonPropertyName("meta")]
        public Meta Meta { get; set; }
    }

    /// <summary>
    /// Represents the response from creating an order.
    /// </summary>

    public class OrderResponse
    {
        /// <summary>
        /// The created order details.
        /// </summary>
        [JsonPropertyName("order")]
        public Order Order { get; set; }

        /// <summary>
        /// Additional metadata about the response.
        /// </summary>
        [JsonPropertyName("meta")]
        public Meta Meta { get; set; }
    }
    /// <summary>
    /// Represents an order.
    /// </summary>
    public class Order
    {
        /// <summary>
        /// The order's unique identifier.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; }

        /// <summary>
        /// Date/Time that the order was created in ISO 8601 format.
        /// </summary>
        [JsonPropertyName("created_at")]
        public string CreatedAt { get; set; }

        /// <summary>
        /// The status of the order.
        /// </summary>
        [JsonPropertyName("status")]
        public OrderItemStatus Status { get; set; }

        /// <summary>
        /// Object representing the quantity of what was ordered.
        /// </summary>
        [JsonPropertyName("ordered")]
        public OrderQuantity Ordered { get; set; }

        /// <summary>
        /// Object representing the quantity of what was fulfilled.
        /// </summary>
        [JsonPropertyName("fulfilled")]
        public OrderQuantity Fulfilled { get; set; }

        /// <summary>
        /// The order items.
        /// </summary>
        [JsonPropertyName("order_items")]
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
        [JsonPropertyName("quantity")]
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
        [JsonPropertyName("id")]
        public int Id { get; set; }

        /// <summary>
        /// The status of this order item.
        /// </summary>
        [JsonPropertyName("status")]
        public OrderItemStatus Status { get; set; }

        /// <summary>
        /// The type of order item.
        /// </summary>
        [JsonPropertyName("order_item_type")]
        public string OrderItemType { get; set; }

        /// <summary>
        /// Object representing the quantity and any specific numbers ordered for this order item.
        /// </summary>
        [JsonPropertyName("ordered")]
        public OrderItemDetail Ordered { get; set; }

        /// <summary>
        /// Object representing the quantity of what was fulfilled for this order item.
        /// </summary>
        [JsonPropertyName("fulfilled")]
        public OrderQuantity Fulfilled { get; set; }

        /// <summary>
        /// Indicates if this order item can be canceled.
        /// </summary>
        [JsonPropertyName("cancelable")]
        public bool Cancelable { get; set; }

        /// <summary>
        /// The DID Group that was ordered.
        /// </summary>
        [JsonPropertyName("did_group")]
        public DidGroup DidGroup { get; set; }

        /// <summary>
        /// Any numbers that were added to the account as part of this order item.
        /// </summary>
        [JsonPropertyName("numbers")]
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
        [JsonPropertyName("quantity")]
        public int Quantity { get; set; }

        /// <summary>
        /// The specific numbers that were requested for this order item.
        /// </summary>
        [JsonPropertyName("numbers")]
        public List<OrderedNumber> Numbers { get; set; }

    }
}
