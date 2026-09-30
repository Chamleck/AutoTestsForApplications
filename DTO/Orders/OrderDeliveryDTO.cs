using System.Text.Json.Serialization;

namespace AutoTestsForApplications.DTO.Orders;

public class OrderDeliveryDTO
{
    [JsonPropertyName("type")]
    public string Type { get; set; } = string.Empty;

    [JsonPropertyName("status")]
    public string Status { get; set; } = string.Empty;

    [JsonPropertyName("estimatedDate")]
    public string EstimatedDate { get; set; } = string.Empty;

    [JsonPropertyName("trackingNumber")]
    public string TrackingNumber { get; set; } = string.Empty;
}
