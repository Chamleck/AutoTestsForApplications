using System.Text.Json.Serialization;

namespace AutoTestsForApplications.DTO;

public class UsersRootDTO
{
    [JsonPropertyName("data")]
    public List<UserDTO> Data { get; set; }
}