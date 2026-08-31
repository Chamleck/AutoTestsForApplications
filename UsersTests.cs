using AutoTestsForApplications.DTO;
using AutoTestsForApplications.Utils;
using FluentAssertions;

namespace AutoTestsForApplications;

public class UsersTests
{
    private List<UserDTO> _users;

    [OneTimeSetUp]
    public void Setup()
    {
        var root = JsonFileReader.ReadAndDeserialize<UsersRootDTO>("Resources/UsersData.json");
        _users = root.Data;
    }

    [Test]
    public void Test2_1_UsersCount_ShouldBeTen()
    {
        _users.Should().HaveCount(10);
    }

    [Test]
    public void Test2_2_FirstUser_ShouldBeAliceJohnson()
    {
        _users.First().Profile.FullName.Should().Be("Alice Johnson");
    }

    [Test]
    public void Test2_3_AllUserIds_ShouldBeUnique()
    {
        // Select достаёт только Id из каждого юзера, дальше проверяем уникальность
        var ids = _users.Select(u => u.Id).ToList();
        ids.Should().OnlyHaveUniqueItems();
    }

    [Test]
    public void Test2_4_AtLeastOnePremiumUser_ShouldExist()
    {
        _users.Should().Contain(u => u.Profile.Tags.Contains("premium"));
    }

    [Test]
    public void Test2_5_AllUsers_ShouldHaveNonEmptyCity()
    {
        _users.Should().OnlyContain(u => !string.IsNullOrWhiteSpace(u.Profile.Address.City));
    }

    [Test]
    public void Test2_6_AtLeastOneUserFromStockholm_ShouldExist()
    {
        _users.Should().Contain(u => u.Profile.Address.City == "Stockholm");
    }

    [Test]
    public void Test2_7_AllUsersAge_ShouldBeInRange18To60()
    {
        _users.Should().OnlyContain(u => u.Profile.Age >= 18 && u.Profile.Age <= 60);
    }

    [Test]
    public void Test2_8_AtLeastOneAdminUser_ShouldExist()
    {
        _users.Should().Contain(u => u.Roles.Contains("admin"));
    }

    // Задание со звёздочкой №3 — координаты всех юзеров в пределах Швеции
    [Test]
    public void Test3_AllUsersCoordinates_ShouldBeWithinSweden()
    {
        _users.Should().OnlyContain(u =>
            u.Profile.Address.Geo.Lat >= 55.0 && u.Profile.Address.Geo.Lat <= 69.1 &&
            u.Profile.Address.Geo.Lng >= 11.0 && u.Profile.Address.Geo.Lng <= 24.2);
    }

    // Задание со звёздочкой №4 — формат улицы: начинается с буквы, содержит номер дома, не состоит только из цифр
    [Test]
    public void Test4_AllStreets_ShouldMatchFormatRules()
    {
        _users.Should().OnlyContain(u =>
            char.IsLetter(u.Profile.Address.Street[0]) &&
            u.Profile.Address.Street.Any(char.IsDigit) &&
            !u.Profile.Address.Street.All(char.IsDigit));
    }
}