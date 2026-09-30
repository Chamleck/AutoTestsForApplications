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
        string? priceText = await Item(productName)
            .Locator(".inventory_item_price")
            .TextContentAsync();

        return PriceParser.Parse(priceText);
    }

    public async Task ClickFinishAsync()
    {
        await FinishButton.ClickAsync();
        await Page.WaitForURLAsync("**/checkout-complete.html");
    }
}
