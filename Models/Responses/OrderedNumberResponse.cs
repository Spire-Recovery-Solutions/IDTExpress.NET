using System.Text.Json.Serialization;

namespace IDTExpress.NET.Models.Responses
{

    /// <summary>
    /// Represents an ordered number.
    /// </summary>
    public class OrderedNumber
    {
        /// <summary>
        /// A specific number that was added.
        /// </summary>
        [JsonPropertyName("number")]
        public string Number { get; set; }

        /// <summary>
        /// The corresponding SKU of the number.
        /// </summary>
        [JsonPropertyName("sku")]
        public string Sku { get; set; }
    }
}
