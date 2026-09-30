using System.Text.Json.Serialization;

namespace AutoTestsForApplications.DTO.BookStore;

public class AddBooksResponseDTO
{
    [JsonPropertyName("books")]
    public List<BookIsbnDTO> Books { get; set; } = new();
}
