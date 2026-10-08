using AutoTestsForApplications.ForUI.Pages.SauceDemo;
using FluentAssertions;
using Microsoft.Playwright;

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
        await Assertions.Expect(loginPage.UniqueElement).ToBeVisibleAsync();

        await loginPage.LoginAsync("standard_user", "secret_sauce");
        await Assertions.Expect(inventoryPage.UniqueElement).ToBeVisibleAsync();

        // эталонные цены читаем со страницы Products, в тесте они не захардкожены
        var expectedPrices = new Dictionary<string, decimal>();
        foreach (string product in products)
        {
            expectedPrices[product] = await inventoryPage.GetPriceAsync(product);
            await inventoryPage.AddToCartAsync(product);
        }

        await inventoryPage.OpenCartAsync();
        await Assertions.Expect(cartPage.UniqueElement).ToBeVisibleAsync();

        (await cartPage.GetItemNamesAsync()).Should().BeEquivalentTo(products);
        foreach (string product in products)
        {
            (await cartPage.GetItemPriceAsync(product)).Should().Be(expectedPrices[product]);
        }

        await cartPage.ClickCheckoutAsync();
        await Assertions.Expect(checkoutInfoPage.UniqueElement).ToBeVisibleAsync();

        await checkoutInfoPage.FillFormAsync("Nick", "Tester", "11000");
        await checkoutInfoPage.ClickContinueAsync();
        await Assertions.Expect(overviewPage.UniqueElement).ToBeVisibleAsync();

        (await overviewPage.GetItemNamesAsync()).Should().BeEquivalentTo(products);
        foreach (string product in products)
        {
            (await overviewPage.GetItemPriceAsync(product)).Should().Be(expectedPrices[product]);
        }

        await overviewPage.ClickFinishAsync();
        await Assertions.Expect(completePage.UniqueElement).ToBeVisibleAsync();

        (await completePage.GetHeaderTextAsync()).Should().Contain("Thank you for your order!");
    }
}
