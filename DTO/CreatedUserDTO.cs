using System.Text.Json.Serialization;

namespace AutoTestsForApplications.DTO;

public class CreatedUserDTO
{
    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("job")]
    public string Job { get; set; }

    [JsonPropertyName("createdAt")]
    public string CreatedAt { get; set; }
}