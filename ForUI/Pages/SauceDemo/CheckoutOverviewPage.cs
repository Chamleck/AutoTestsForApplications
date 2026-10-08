using AutoTestsForApplications.Utils;
using Microsoft.Playwright;

namespace AutoTestsForApplications.ForUI.Pages.SauceDemo;

public class CheckoutOverviewPage
{
    private readonly IPage Page;

    private ILocator ItemNames => Page.Locator(".cart_item .inventory_item_name");
    private ILocator FinishButton => Page.GetByRole(AriaRole.Button, new() { Name = "Finish" });

    private ILocator Item(string productName) =>
        Page.Locator(".cart_item").Filter(new() { HasText = productName });

    private ILocator ItemPrice(string productName) =>
        Item(productName).Locator(".inventory_item_price");

    // элемент, который есть только на этой странице; тест проверяет его видимость через Expect
    public ILocator UniqueElement => Page.GetByText("Checkout: Overview", new() { Exact = true });

    public CheckoutOverviewPage(IPage page)
    {
        Page = page;
    }

    public async Task<IReadOnlyList<string>> GetItemNamesAsync()
    {
        return await ItemNames.AllTextContentsAsync();
    }

    public async Task<decimal> GetItemPriceAsync(string productName)
    {
        string? priceText = await ItemPrice(productName).TextContentAsync();

        return PriceParser.Parse(priceText);
    }

    public async Task ClickFinishAsync()
    {
        await FinishButton.ClickAsync();
    }
}
