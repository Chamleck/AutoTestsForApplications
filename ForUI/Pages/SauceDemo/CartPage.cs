using AutoTestsForApplications.Utils;
using Microsoft.Playwright;

namespace AutoTestsForApplications.ForUI.Pages.SauceDemo;

public class CartPage
{
    private readonly IPage Page;

    private ILocator CartItemNames => Page.Locator(".cart_item .inventory_item_name");
    private ILocator CheckoutButton => Page.GetByRole(AriaRole.Button, new() { Name = "Checkout" });

    private ILocator CartItem(string productName) =>
        Page.Locator(".cart_item").Filter(new() { HasText = productName });

    public CartPage(IPage page)
    {
        Page = page;
    }

    public async Task<IReadOnlyList<string>> GetItemNamesAsync()
    {
        return await CartItemNames.AllTextContentsAsync();
    }

    public async Task<decimal> GetItemPriceAsync(string productName)
    {
        string? priceText = await CartItem(productName)
            .Locator(".inventory_item_price")
            .TextContentAsync();

        return PriceParser.Parse(priceText);
    }

    public async Task ClickCheckoutAsync()
    {
        await CheckoutButton.ClickAsync();
        await Page.WaitForURLAsync("**/checkout-step-one.html");
    }
}
