using Microsoft.Playwright;

namespace AutoTestsForApplications.ForUI.Pages.SauceDemo;

public class CheckoutCompletePage
{
    private readonly IPage Page;

    private ILocator CompleteHeader => Page.Locator(".complete-header");

    // элемент, который есть только на этой странице; тест проверяет его видимость через Expect
    public ILocator UniqueElement => Page.GetByText("Checkout: Complete!", new() { Exact = true });

    public CheckoutCompletePage(IPage page)
    {
        Page = page;
    }

    public async Task<string> GetHeaderTextAsync()
    {
        return await CompleteHeader.TextContentAsync() ?? string.Empty;
    }
}
