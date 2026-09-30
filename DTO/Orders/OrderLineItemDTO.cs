using System.Text.Json.Serialization;

namespace AutoTestsForApplications.DTO.Orders;

public class OrderLineItemDTO
{
    [JsonPropertyName("productId")]
    public int ProductId { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("category")]
    public string Category { get; set; } = string.Empty;

    [JsonPropertyName("quantity")]
    public int Quantity { get; set; }

    // decimal, а не double: деньги считаем без погрешности округления
    [JsonPropertyName("price")]
    public decimal Price { get; set; }
}
