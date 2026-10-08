using System.Text.Json.Serialization;

namespace AutoTestsForApplications.DTO.SauceDemo;

public class SauceDemoUserDTO
{
    [JsonPropertyName("username")]
    public string Username { get; set; } = string.Empty;

    [JsonPropertyName("password")]
    public string Password { get; set; } = string.Empty;
}
