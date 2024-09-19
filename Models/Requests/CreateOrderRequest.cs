using System.Text.Json.Serialization;

namespace IDTExpress.NET.Models.Requests
{

    /// <summary>
    /// Represents the request body for creating an order.
    /// </summary>
    public class CreateOrderRequest
    {
        /// <summary>
        /// If true, the call does not create an order but still goes through all validations.
        /// </summary>
        [JsonPropertyName("preview")]
        public bool Preview { get; set; }

        /// <summary>
        /// The order items. Currently, only one order item is supported.
        /// </summary>
        [JsonPropertyName("order_items")]
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
        [JsonPropertyName("did_group_id")]
        public int DidGroupId { get; set; }

        /// <summary>
        /// The amount of numbers to order. Maximum is 100.
        /// </summary>
        [JsonPropertyName("quantity")]
        public int? Quantity { get; set; }

        /// <summary>
        /// The specific number SKUs to order.
        /// </summary>
        [JsonPropertyName("did_skus")]
        public List<string> DidSkus { get; set; }
    }
}
