using System.Text.Json.Serialization;

namespace AutoTestsForApplications.DTO.PetStore;

public class PetDTO
{
    // id здесь строка (UUID), а не число
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("species")]
    public string Species { get; set; } = string.Empty;

    [JsonPropertyName("breed")]
    public string Breed { get; set; } = string.Empty;

    [JsonPropertyName("ageMonths")]
    public int AgeMonths { get; set; }

    [JsonPropertyName("size")]
    public string Size { get; set; } = string.Empty;

    [JsonPropertyName("status")]
    public string Status { get; set; } = string.Empty;

    [JsonPropertyName("price")]
    public string Price { get; set; } = string.Empty;

    [JsonPropertyName("currency")]
    public string Currency { get; set; } = string.Empty;

    [JsonPropertyName("goodWithKids")]
    public bool GoodWithKids { get; set; }

    [JsonPropertyName("createdAt")]
    public string CreatedAt { get; set; } = string.Empty;

    [JsonPropertyName("updatedAt")]
    public string UpdatedAt { get; set; } = string.Empty;

    // ? потому что блок может отсутствовать в ответе
    [JsonPropertyName("medicalInfo")]
    public MedicalInfoDTO? MedicalInfo { get; set; }
}
