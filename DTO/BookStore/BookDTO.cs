using System.Text.Json.Serialization;

namespace AutoTestsForApplications.DTO.BookStore;

public class BookDTO
{
    [JsonPropertyName("isbn")]
    public string Isbn { get; set; } = string.Empty;

    [JsonPropertyName("title")]
    public string Title { get; set; } = string.Empty;

    [JsonPropertyName("subTitle")]
    public string SubTitle { get; set; } = string.Empty;

    [JsonPropertyName("author")]
    public string Author { get; set; } = string.Empty;

    // в JSON ключ с подчёркиванием, поэтому нужен атрибут
    [JsonPropertyName("publish_date")]
    public DateTime PublishDate { get; set; }

    [JsonPropertyName("publisher")]
    public string Publisher { get; set; } = string.Empty;

    [JsonPropertyName("pages")]
    public int Pages { get; set; }

    [JsonPropertyName("description")]
    public string Description { get; set; } = string.Empty;

    [JsonPropertyName("website")]
    public string Website { get; set; } = string.Empty;
}
