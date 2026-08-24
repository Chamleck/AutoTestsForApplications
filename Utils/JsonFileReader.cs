using System.Text.Json;

namespace AutoTestsForApplications.Utils;

public static class JsonFileReader
{
    public static T ReadAndDeserialize<T>(string relativePath)
    {
        string fullPath = Path.Combine(AppContext.BaseDirectory, relativePath);
        string json = File.ReadAllText(fullPath);

        return JsonSerializer.Deserialize<T>(json)
               ?? throw new InvalidOperationException($"Не удалось десериализовать файл: {fullPath}");
    }
}