using System.Text.Json.Serialization;

namespace AutoTestsForApplications.DTO.Orders;

public class OrderCustomerDTO
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("email")]
    public string Email { get; set; } = string.Empty;

    [JsonPropertyName("phone")]
    public string Phone { get; set; } = string.Empty;

    [JsonPropertyName("address")]
    public OrderAddressDTO Address { get; set; } = new();
}
