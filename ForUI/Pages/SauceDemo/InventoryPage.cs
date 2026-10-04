using AutoTestsForApplications.Utils;
using Microsoft.Playwright;

namespace AutoTestsForApplications.ForUI.Pages.SauceDemo;

public class InventoryPage : BasePage
{
    private ILocator ShoppingCartLink => Page.Locator(".shopping_cart_link");

    // локаторы, зависящие от названия товара, - методы-фабрики; селекторы живут только здесь
    private ILocator ProductCard(string productName) =>
        Page.Locator(".inventory_item").Filter(new() { HasText = productName });

    private ILocator AddToCartButton(string productName) =>
        ProductCard(productName).GetByRole(AriaRole.Button, new() { Name = "Add to cart" });

    private ILocator ProductPrice(string productName) =>
        ProductCard(productName).Locator(".inventory_item_price");

    protected override ILocator UniqueElement => Page.GetByText("Products", new() { Exact = true });

    public InventoryPage(IPage page) : base(page)
    {
    }

    public async Task AddToCartAsync(string productName)
    {
        await AddToCartButton(productName).ClickAsync();
    }

    public async Task<decimal> GetPriceAsync(string productName)
    {
        string? priceText = await ProductPrice(productName).TextContentAsync();

        return PriceParser.Parse(priceText);
    }

    public async Task OpenCartAsync()
    {
        await ShoppingCartLink.ClickAsync();
    }
}
