using AutoTestsForApplications.Utils;
using Microsoft.Playwright;

namespace AutoTestsForApplications.ForUI.Pages.SauceDemo;

public class CartPage : BasePage
{
    private ILocator CartItemNames => Page.Locator(".cart_item .inventory_item_name");
    private ILocator CheckoutButton => Page.GetByRole(AriaRole.Button, new() { Name = "Checkout" });

    private ILocator CartItem(string productName) =>
        Page.Locator(".cart_item").Filter(new() { HasText = productName });

    private ILocator ItemPrice(string productName) =>
        CartItem(productName).Locator(".inventory_item_price");

    protected override ILocator UniqueElement => Page.GetByText("Your Cart", new() { Exact = true });

    public CartPage(IPage page) : base(page)
    {
    }

    public async Task<IReadOnlyList<string>> GetItemNamesAsync()
    {
        return await CartItemNames.AllTextContentsAsync();
    }

    public async Task<decimal> GetItemPriceAsync(string productName)
    {
        string? priceText = await ItemPrice(productName).TextContentAsync();

        return PriceParser.Parse(priceText);
    }

    public async Task ClickCheckoutAsync()
    {
        await CheckoutButton.ClickAsync();
    }
}
