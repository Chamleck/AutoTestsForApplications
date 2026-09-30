using System.Text.Json.Serialization;

namespace AutoTestsForApplications.DTO.BookStore;

// одно и то же тело используется для создания юзера, получения токена и логина
public class BookStoreCredentialsDTO
{
    [JsonPropertyName("userName")]
    public string UserName { get; set; } = string.Empty;

    [JsonPropertyName("password")]
    public string Password { get; set; } = string.Empty;
}
