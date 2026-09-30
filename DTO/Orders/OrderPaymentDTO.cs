using System.Text.Json.Serialization;

namespace AutoTestsForApplications.DTO.Orders;

public class OrderPaymentDTO
{
    [JsonPropertyName("method")]
    public string Method { get; set; } = string.Empty;

    [JsonPropertyName("status")]
    public string Status { get; set; } = string.Empty;

    [JsonPropertyName("transactionId")]
    public string TransactionId { get; set; } = string.Empty;
}
