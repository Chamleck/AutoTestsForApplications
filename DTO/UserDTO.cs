using System.Text.Json.Serialization;

namespace AutoTestsForApplications.DTO;

public class UserDTO
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("username")]
    public string Username { get; set; }

    [JsonPropertyName("profile")]
    public ProfileDTO Profile { get; set; }

    [JsonPropertyName("roles")]
    public List<string> Roles { get; set; }
}