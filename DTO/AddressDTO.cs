using System.Text.Json.Serialization;

namespace AutoTestsForApplications.DTO;

public class AddressDTO
{
    [JsonPropertyName("street")]
    public string Street { get; set; }

    [JsonPropertyName("city")]
    public string City { get; set; }

    [JsonPropertyName("geo")]
    public GeoDTO Geo { get; set; }
}