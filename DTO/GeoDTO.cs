using System.Text.Json.Serialization;

namespace AutoTestsForApplications.DTO;

public class GeoDTO
{
    [JsonPropertyName("lat")]
    public double Lat { get; set; }

    [JsonPropertyName("lng")]
    public double Lng { get; set; }
}