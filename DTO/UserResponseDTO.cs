using System.Text.Json.Serialization;

namespace AutoTestsForApplications.DTO;

public class UserResponseDTO
{
    [JsonPropertyName("data")]
    public UserDataDTO Data { get; set; }
}