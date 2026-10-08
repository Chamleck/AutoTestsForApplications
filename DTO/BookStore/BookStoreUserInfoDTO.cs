using System.Text.Json.Serialization;

namespace AutoTestsForApplications.DTO.BookStore;

public class BookStoreUserInfoDTO
{
    [JsonPropertyName("userId")]
    public string UserId { get; set; } = string.Empty;

    [JsonPropertyName("username")]
    public string UserName { get; set; } = string.Empty;

    [JsonPropertyName("books")]
    public List<BookDTO> Books { get; set; } = new();
}
