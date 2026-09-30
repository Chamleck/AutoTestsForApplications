using System.Text.Json.Serialization;

namespace AutoTestsForApplications.DTO.PetStore;

public class MedicalInfoDTO
{
    [JsonPropertyName("vaccinated")]
    public bool Vaccinated { get; set; }

    [JsonPropertyName("spayedNeutered")]
    public bool SpayedNeutered { get; set; }

    [JsonPropertyName("microchipped")]
    public bool Microchipped { get; set; }

    [JsonPropertyName("specialNeeds")]
    public bool SpecialNeeds { get; set; }

    [JsonPropertyName("healthNotes")]
    public string? HealthNotes { get; set; }
}
