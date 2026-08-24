using System.Text.Json.Serialization;

namespace AutoTestsForApplications.DTO;

public class ProfileDTO
{
    [JsonPropertyName("fullName")]
    public string FullName { get; set; }

    [JsonPropertyName("age")]
    public int Age { get; set; }

    [JsonPropertyName("address")]
    public AddressDTO Address { get; set; }

    [JsonPropertyName("tags")]
    public List<string> Tags { get; set; }
}