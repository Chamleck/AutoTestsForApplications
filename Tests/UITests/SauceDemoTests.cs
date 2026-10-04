using AutoTestsForApplications.ForUI.Pages.SauceDemo;
using FluentAssertions;

namespace AutoTestsForApplications.Tests.UITests;

public class SauceDemoTests : BaseTest
{
    [Test]
    public async Task Login_WithValidCredentials_ShowsProductsPage()
    {
        var loginPage = new LoginPage(Page);
        var inventoryPage = new InventoryPage(Page);

        await loginPage.OpenAsync();
        await loginPage.LoginAsync("standard_user", "secret_sauce");

        (await inventoryPage.IsOpenedAsync()).Should().BeTrue("после логина должна открыться страница Products");
    }
}
