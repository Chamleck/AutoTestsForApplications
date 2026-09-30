using System.Text.Json.Serialization;

namespace AutoTestsForApplications.DTO.Orders;

public class OrderDataDTO
{
    [JsonPropertyName("orderId")]
    public string OrderId { get; set; } = string.Empty;

    [JsonPropertyName("createdAt")]
    public string CreatedAt { get; set; } = string.Empty;

    [JsonPropertyName("customer")]
    public OrderCustomerDTO Customer { get; set; } = new();

    [JsonPropertyName("items")]
    public List<OrderLineItemDTO> Items { get; set; } = new();

    [JsonPropertyName("payment")]
    public OrderPaymentDTO Payment { get; set; } = new();

    [JsonPropertyName("delivery")]
    public OrderDeliveryDTO Delivery { get; set; } = new();

    [JsonPropertyName("summary")]
    public OrderSummaryDTO Summary { get; set; } = new();
}
