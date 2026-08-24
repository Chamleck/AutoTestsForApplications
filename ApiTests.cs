using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using AutoTestsForApplications.DTO;

namespace AutoTestsForApplications;

public class ApiTests
{
    private static HttpClient _client;

    [OneTimeSetUp]
    public void Setup()
    {
        _client = new HttpClient
        {
            BaseAddress = new Uri("https://reqres.in/api/")
        };
        _client.DefaultRequestHeaders.Add("x-api-key", "free_user_3IM1euXaembNb243zEyEKaKq2M7");
    }

    [Test]
    public async Task Test1_GetUser_ReturnsSuccessStatusCode()
    {
        using HttpResponseMessage response = await _client.GetAsync("users/2");
        response.EnsureSuccessStatusCode();
    }

    [Test]
    public async Task Test2_GetUser_DeserializesCorrectId()
    {
        using HttpResponseMessage response = await _client.GetAsync("users/2");
        string json = await response.Content.ReadAsStringAsync();

        UserResponseDTO userResponse = JsonSerializer.Deserialize<UserResponseDTO>(json);

        Assert.That(userResponse.Data.Id, Is.EqualTo(2));
    }

    [Test]
    public async Task Test3_CreateUser_ReturnsCreatedUserData()
    {
        var payload = new CreateUserRequestDTO
        {
            Name = "Nick Chamleck",
            Job = "QA Automation Engineer"
        };

        using HttpResponseMessage response = await _client.PostAsJsonAsync("users", payload);
        string json = await response.Content.ReadAsStringAsync();

        CreatedUserDTO createdUser = JsonSerializer.Deserialize<CreatedUserDTO>(json);

        Assert.That(createdUser, Is.Not.Null);
        Assert.That(createdUser.Id, Is.Not.Null);
        Assert.That(createdUser.CreatedAt, Is.Not.Null);
        Assert.That(createdUser.Name, Is.EqualTo("Nick Chamleck"));
        Assert.That(createdUser.Job, Is.EqualTo("QA Automation Engineer"));
    }

    [Test]
    public async Task Test4_UpdateUser_ReturnsSuccessAndUpdatedData()
    {
        var payload = new CreateUserRequestDTO
        {
            Name = "Nick Chamleck",
            Job = "Senior QA Automation Engineer"
        };

        using HttpResponseMessage response = await _client.PutAsJsonAsync("users/2", payload);
        response.EnsureSuccessStatusCode();

        string json = await response.Content.ReadAsStringAsync();
        CreatedUserDTO updatedUser = JsonSerializer.Deserialize<CreatedUserDTO>(json);

        Assert.That(updatedUser.Name, Is.EqualTo("Nick Chamleck"));
        Assert.That(updatedUser.Job, Is.EqualTo("Senior QA Automation Engineer"));
    }

    [Test]
    public async Task Test5_DeleteUser_ReturnsSuccessStatusCode()
    {
        using HttpResponseMessage response = await _client.DeleteAsync("users/2");
        response.EnsureSuccessStatusCode();
    }

    [OneTimeTearDown]
    public void TearDown()
    {
        _client.Dispose();
    }
}