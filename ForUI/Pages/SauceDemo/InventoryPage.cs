using AutoTestsForApplications.Utils;
using Microsoft.Playwright;

namespace AutoTestsForApplications.ForUI.Pages.SauceDemo;

public class InventoryPage
{
    private readonly IPage Page;

    private ILocator PageTitle => Page.GetByText("Products", new() { Exact = true });
    private ILocator ShoppingCartLink => Page.Locator(".shopping_cart_link");

    // карточка товара ищется по названию, поэтому методы ниже работают для любого товара на странице
    private ILocator ProductCard(string productName) =>
        Page.Locator(".inventory_item").Filter(new() { HasText = productName });

    public InventoryPage(IPage page)
    {
        Page = page;
    }

    public async Task<bool> IsPageOpenedAsync()
    {
        return await PageTitle.IsVisibleAsync();
    }

    public async Task AddToCartAsync(string productName)
    {
        await ProductCard(productName)
            .GetByRole(AriaRole.Button, new() { Name = "Add to cart" })
            .ClickAsync();
    }

    // цена берётся со страницы, а не хардкодится в тесте
    public async Task<decimal> GetPriceAsync(string productName)
    {
        string? priceText = await ProductCard(productName)
            .Locator(".inventory_item_price")
            .TextContentAsync();

        return PriceParser.Parse(priceText);
    }

    public async Task OpenCartAsync()
    {
        await ShoppingCartLink.ClickAsync();
        await Page.WaitForURLAsync("**/cart.html");
    }
}
