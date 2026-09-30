using System.Text.Json.Serialization;

namespace AutoTestsForApplications.DTO.BookStore;

public class BookStoreCreatedUserDTO
{
    // в ответе ключ именно "userID" (две заглавные буквы)
    [JsonPropertyName("userID")]
    public string UserId { get; set; } = string.Empty;

    [JsonPropertyName("username")]
    public string UserName { get; set; } = string.Empty;
}
