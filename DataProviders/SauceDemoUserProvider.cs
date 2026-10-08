using AutoTestsForApplications.DTO.SauceDemo;
using AutoTestsForApplications.Utils;

namespace AutoTestsForApplications.DataProviders;

// единственное место, где читаются юзеры saucedemo; тесты не хранят логины и пароли у себя
public static class SauceDemoUserProvider
{
    private const string ValidUsersFilePath = "Resources/SauceDemoUsers.json";

    // источник для TestCaseSource: класс и метод должны быть public static
    public static IEnumerable<TestCaseData> GetValidUsers()
    {
        foreach (var user in LoadValidUsers())
        {
            // имя кейса в Test Explorer - логин юзера, а не "(\"standard_user\",\"secret_sauce\")"
            yield return new TestCaseData(user.Username, user.Password).SetArgDisplayNames(user.Username);
        }
    }

    // для тестов, которым нужен конкретный юзер (например, чекаут работает корректно только у standard_user)
    public static SauceDemoUserDTO GetUser(string username)
    {
        return LoadValidUsers().FirstOrDefault(user => user.Username == username)
               ?? throw new ArgumentException($"Юзер '{username}' не найден в {ValidUsersFilePath}", nameof(username));
    }

    private static List<SauceDemoUserDTO> LoadValidUsers()
    {
        return JsonFileReader.ReadAndDeserialize<List<SauceDemoUserDTO>>(ValidUsersFilePath);
    }
}
