using FluentAssertions;
using Microsoft.Playwright;

namespace AutoTestsForApplications.Tests.UITests;

public class SauceDemoTests : BaseTest
{
    [Test]
    public async Task Login_WithValidCredentials_ShowsProductsPage()
    {
        await Page.GotoAsync("https://www.saucedemo.com");

        await Page.GetByPlaceholder("Username").FillAsync("standard_user");
        await Page.GetByPlaceholder("Password").FillAsync("secret_sauce");
        await Page.GetByRole(AriaRole.Button, new() { Name = "Login" }).ClickAsync();

        var productsTitle = Page.GetByText("Products", new() { Exact = true });
        await productsTitle.WaitForAsync();

        (await productsTitle.IsVisibleAsync()).Should().BeTrue();
    }
}