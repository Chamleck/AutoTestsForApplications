using System.Net;
using AutoTestsForApplications.DTO;
using AutoTestsForApplications.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Refit;

namespace AutoTestsForApplications;

public class RefitTests
{
    private IUserApiClient _client;

    [OneTimeSetUp]
    public void Setup()
    {
        var services = new ServiceCollection();

        services.AddRefitClient<IUserApiClient>()
            .ConfigureHttpClient(c =>
            {
                c.BaseAddress = new Uri("https://reqres.in/api");
                c.DefaultRequestHeaders.Add("x-api-key", "free_user_3IM1euXaembNb243zEyEKaKq2M7");
            });

        var provider = services.BuildServiceProvider();
        _client = provider.GetRequiredService<IUserApiClient>();
    }

    [Test]
    public async Task Test1_GetUser_ReturnsCorrectData()
    {
        UserResponseDTO result = await _client.GetUserAsync(2);

        Assert.Multiple(() =>
        {
            Assert.That(result.Data.Id, Is.EqualTo(2));
            Assert.That(result.Data.Email, Is.Not.Null);
        });
    }

    [Test]
    public async Task Test2_CreateUser_ReturnsCreatedName()
    {
        var newUser = new CreateUserRequestDTO
        {
            Name = "Nick Chamleck",
            Job = "QA Automation Engineer"
        };

        CreatedUserDTO response = await _client.PostUserAsync(newUser);

        Assert.That(response.Name, Is.EqualTo("Nick Chamleck"));
    }

    [Test]
    public async Task Test3_UpdateUser_ReturnsUpdatedJob()
    {
        var updateUser = new CreateUserRequestDTO
        {
            Name = "Nick Chamleck",
            Job = "Senior QA Automation Engineer"
        };

        CreatedUserDTO response = await _client.PutUserAsync(2, updateUser);

        Assert.That(response.Job, Is.EqualTo("Senior QA Automation Engineer"));
    }

    [Test]
    public async Task Test4_DeleteUser_ReturnsNoContentStatus()
    {
        ApiResponse<string> response = await _client.DeleteUserAsync(2);

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NoContent));
    }
}