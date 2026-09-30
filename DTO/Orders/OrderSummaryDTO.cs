using System.Text.Json.Serialization;

namespace AutoTestsForApplications.DTO.Orders;

public class OrderSummaryDTO
{
    [JsonPropertyName("itemsTotal")]
    public decimal ItemsTotal { get; set; }

    [JsonPropertyName("deliveryFee")]
    public decimal DeliveryFee { get; set; }

    [JsonPropertyName("discount")]
    public decimal Discount { get; set; }

    [JsonPropertyName("finalTotal")]
    public decimal FinalTotal { get; set; }
}
