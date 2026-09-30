using System.Text.Json.Serialization;

namespace AutoTestsForApplications.DTO.BookStore;

public class BookStoreTokenDTO
{
    [JsonPropertyName("token")]
    public string Token { get; set; } = string.Empty;

    [JsonPropertyName("expires")]
    public string Expires { get; set; } = string.Empty;

    [JsonPropertyName("status")]
    public string Status { get; set; } = string.Empty;

    [JsonPropertyName("result")]
    public string Result { get; set; } = string.Empty;
}
