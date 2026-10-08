using System.Text.Json.Serialization;

namespace AutoTestsForApplications.DTO.BookStore;

public class AddBooksRequestDTO
{
    [JsonPropertyName("userId")]
    public string UserId { get; set; } = string.Empty;

    [JsonPropertyName("collectionOfIsbns")]
    public List<BookIsbnDTO> CollectionOfIsbns { get; set; } = new();
}
