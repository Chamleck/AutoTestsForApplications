using AutoTestsForApplications.ForUI.Pages.Heroku;
using FluentAssertions;
using Microsoft.Playwright;

namespace AutoTestsForApplications.Tests.UITests;

public class HerokuLoginTests : BaseTest
{
    [Test]
    public async Task Login_WithWrongCredentials_ShowsInvalidUsernameError()
    {
        var loginPage = new LoginPage(Page);

        await loginPage.OpenAsync();
        await Assertions.Expect(loginPage.UniqueElement).ToBeVisibleAsync();

        await loginPage.LoginAsync("wrong-username", "wrong-password");

        (await loginPage.GetFlashMessageAsync()).Should().Contain("Your username is invalid!");
    }
}
