using AutoTestsForApplications.DataProviders;
using AutoTestsForApplications.ForUI.Pages.SauceDemo;
using Microsoft.Playwright;

namespace AutoTestsForApplications.Tests.UITests;

public class SauceDemoTests : BaseTest
{
    [TestCaseSource(typeof(SauceDemoUserProvider), nameof(SauceDemoUserProvider.GetValidUsers))]
    public async Task Login_WithValidCredentials_ShowsProductsPage(string username, string password)
    {
        var loginPage = new LoginPage(Page);
        var inventoryPage = new InventoryPage(Page);

        await loginPage.OpenAsync();
        await loginPage.LoginAsync(username, password);

        await Assertions.Expect(inventoryPage.UniqueElement).ToBeVisibleAsync();
    }
}
