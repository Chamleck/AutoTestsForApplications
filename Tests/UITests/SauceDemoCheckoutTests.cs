using AutoTestsForApplications.ForUI.Pages.SauceDemo;
using FluentAssertions;

namespace AutoTestsForApplications.Tests.UITests;

public class SauceDemoCheckoutTests : BaseTest
{
    [Test]
    public async Task Checkout_TwoItems_ShowsThankYouMessage()
    {
        string[] products = { "Sauce Labs Backpack", "Sauce Labs Bolt T-Shirt" };

        var loginPage = new LoginPage(Page);
        var inventoryPage = new InventoryPage(Page);
        var cartPage = new CartPage(Page);
        var checkoutInfoPage = new CheckoutInfoPage(Page);
        var overviewPage = new CheckoutOverviewPage(Page);
        var completePage = new CheckoutCompletePage(Page);

        await loginPage.OpenAsync();
        await loginPage.LoginAsync("standard_user", "secret_sauce");
        (await inventoryPage.IsPageOpenedAsync()).Should().BeTrue();

        // эталонные цены читаем со страницы Products, в тесте они не захардкожены
        var expectedPrices = new Dictionary<string, decimal>();
        foreach (string product in products)
        {
            expectedPrices[product] = await inventoryPage.GetPriceAsync(product);
            await inventoryPage.AddToCartAsync(product);
        }

        await inventoryPage.OpenCartAsync();

        (await cartPage.GetItemNamesAsync()).Should().BeEquivalentTo(products);
        foreach (string product in products)
        {
            (await cartPage.GetItemPriceAsync(product)).Should().Be(expectedPrices[product]);
        }

        await cartPage.ClickCheckoutAsync();
        await checkoutInfoPage.FillFormAndContinueAsync("Nick", "Tester", "11000");

        (await overviewPage.GetItemNamesAsync()).Should().BeEquivalentTo(products);
        foreach (string product in products)
        {
            (await overviewPage.GetItemPriceAsync(product)).Should().Be(expectedPrices[product]);
        }

        await overviewPage.ClickFinishAsync();

        (await completePage.GetHeaderTextAsync()).Should().Contain("Thank you for your order!");
    }
}
