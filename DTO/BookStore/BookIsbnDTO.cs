using System.Text.Json.Serialization;

namespace AutoTestsForApplications.DTO.BookStore;

// при добавлении книги в коллекцию нужен только ISBN, полная карточка не нужна
public class BookIsbnDTO
{
    [JsonPropertyName("isbn")]
    public string Isbn { get; set; } = string.Empty;
}
