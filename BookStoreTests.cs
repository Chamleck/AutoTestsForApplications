using System.Net;
using AutoTestsForApplications.DTO.BookStore;
using AutoTestsForApplications.Interfaces;
using AutoTestsForApplications.Utils;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Refit;

namespace AutoTestsForApplications;

public class BookStoreTests
{
    // пароль должен содержать заглавную, строчную, цифру и спецсимвол, иначе сервер отклонит регистрацию
    private const string Password = "StrongPass10!!!";
    private const string IsbnGitGuide = "9781449325862";
    private const string IsbnLearningJs = "9781449331818";

    private ServiceProvider _provider = null!;
    private IBookStoreApi _api = null!;

    // небольшая "запись" для данных свежесозданного пользователя
    private sealed record TestUser(string UserName, string UserId, string AuthHeader);

    [OneTimeSetUp]
    public void Setup()
    {
        var services = new ServiceCollection();
        services.AddRefitClient<IBookStoreApi>()
            .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://demoqa.com"));

        _provider = services.BuildServiceProvider();
        _api = _provider.GetRequiredService<IBookStoreApi>();
    }

    [OneTimeTearDown]
    public void TearDown()
    {
        _provider.Dispose();
    }

    [Test]
    public async Task CreateUser_ReturnsUserIdAndUserName()
    {
        string userName = NewUserName();

        var response = await _api.CreateUserAsync(new BookStoreCredentialsDTO { UserName = userName, Password = Password });

        response.UserId.Should().NotBeNullOrEmpty();
        response.UserName.Should().Be(userName);
    }

    [Test]
    public async Task GenerateToken_ReturnsSuccessAndToken()
    {
        var user = await RegisterUserAsync();

        var token = await _api.GenerateTokenAsync(new BookStoreCredentialsDTO { UserName = user.UserName, Password = Password });

        token.Token.Should().NotBeNullOrEmpty();
        token.Status.Should().Be("Success");
        token.Result.Should().Contain("authorized");
    }

    [Test]
    public async Task Login_ReturnsSameUserIdAsRegistration()
    {
        var user = await RegisterUserAsync();

        var login = await _api.LoginAsync(new BookStoreCredentialsDTO { UserName = user.UserName, Password = Password });

        login.UserId.Should().Be(user.UserId);
        login.UserName.Should().Be(user.UserName);
    }

    [Test]
    public async Task GetAllBooks_ReturnsListWithKnownBook()
    {
        var response = await _api.GetAllBooksAsync();

        // точное число книг не проверяем: справочник общий и может измениться
        response.Books.Should().NotBeEmpty();
        response.Books.Should().Contain(b => b.Isbn == IsbnGitGuide);
    }

    [Test]
    public async Task GetBookByIsbn_ReturnsSameBookAsInList()
    {
        var all = await _api.GetAllBooksAsync();
        var expected = RandomHelper.GetRandomItem(all.Books);

        var actual = await _api.GetBookByIsbnAsync(expected.Isbn);

        actual.Isbn.Should().Be(expected.Isbn);
        actual.Title.Should().Be(expected.Title);
        actual.SubTitle.Should().Be(expected.SubTitle);
    }

    [Test]
    public async Task AddBook_AddsSingleBookToUser()
    {
        var user = await CreateAuthorizedUserAsync();
        var request = BuildAddRequest(user.UserId, IsbnGitGuide);

        var response = await _api.AddBooksAsync(request, user.AuthHeader);

        response.Books.Should().ContainSingle().Which.Isbn.Should().Be(IsbnGitGuide);
    }

    [Test]
    public async Task AddMultipleBooks_AddsBothBooksToUser()
    {
        var user = await CreateAuthorizedUserAsync();
        var request = BuildAddRequest(user.UserId, IsbnGitGuide, IsbnLearningJs);

        var response = await _api.AddBooksAsync(request, user.AuthHeader);

        response.Books.Select(b => b.Isbn).Should().BeEquivalentTo(IsbnGitGuide, IsbnLearningJs);
    }

    [Test]
    public async Task AddBook_WithoutToken_ReturnsUnauthorized()
    {
        // регистрируем пользователя, но токен намеренно не получаем и не передаём
        var user = await RegisterUserAsync();
        var request = BuildAddRequest(user.UserId, IsbnGitGuide);

        Func<Task> act = () => _api.AddBooksAsync(request, authorization: null);

        // Refit бросает ApiException на любой не-2xx ответ, статус-код лежит в самом исключении
        var exception = await act.Should().ThrowAsync<ApiException>();
        exception.Which.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Test]
    public async Task DeleteBook_RemovesBookFromUserCollection()
    {
        var user = await CreateAuthorizedUserAsync();
        await _api.AddBooksAsync(BuildAddRequest(user.UserId, IsbnGitGuide), user.AuthHeader);

        var before = await _api.GetUserAsync(user.UserId, user.AuthHeader);
        before.Books.Should().ContainSingle().Which.Isbn.Should().Be(IsbnGitGuide);

        await _api.DeleteBookAsync(new DeleteBookRequestDTO { Isbn = IsbnGitGuide, UserId = user.UserId }, user.AuthHeader);

        var after = await _api.GetUserAsync(user.UserId, user.AuthHeader);
        after.Books.Should().BeEmpty();
    }

    // Guid делает имя уникальным, поэтому тесты не конфликтуют между собой и между запусками
    private static string NewUserName() => $"user_{Guid.NewGuid():N}";

    private static AddBooksRequestDTO BuildAddRequest(string userId, params string[] isbns) => new()
    {
        UserId = userId,
        CollectionOfIsbns = isbns.Select(isbn => new BookIsbnDTO { Isbn = isbn }).ToList()
    };

    // регистрация без токена
    private async Task<TestUser> RegisterUserAsync()
    {
        string userName = NewUserName();
        var created = await _api.CreateUserAsync(new BookStoreCredentialsDTO { UserName = userName, Password = Password });

        return new TestUser(userName, created.UserId, AuthHeader: string.Empty);
    }

    // регистрация + токен: пользователь, от имени которого можно менять свою коллекцию книг
    private async Task<TestUser> CreateAuthorizedUserAsync()
    {
        var user = await RegisterUserAsync();
        var token = await _api.GenerateTokenAsync(new BookStoreCredentialsDTO { UserName = user.UserName, Password = Password });

        return user with { AuthHeader = $"Bearer {token.Token}" };
    }
}
