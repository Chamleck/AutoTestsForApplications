using System.Text.Json.Serialization;

namespace AutoTestsForApplications.DTO.BookStore;

public class BooksListDTO
{
    [JsonPropertyName("books")]
    public List<BookDTO> Books { get; set; } = new();
}
