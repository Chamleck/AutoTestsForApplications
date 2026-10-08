using System.Text.Json.Serialization;

namespace AutoTestsForApplications.DTO.PetStore;

// список питомцев приходит внутри обёртки {"data": [...]}
public class AllPetsResponseDTO
{
    [JsonPropertyName("data")]
    public List<PetDTO> Data { get; set; } = new();
}
