using System.Text.Json.Serialization;

namespace AutoTestsForApplications.DTO.BookStore;

public class DeleteBookRequestDTO
{
    [JsonPropertyName("isbn")]
    public string Isbn { get; set; } = string.Empty;

    [JsonPropertyName("userId")]
    public string UserId { get; set; } = string.Empty;
}
