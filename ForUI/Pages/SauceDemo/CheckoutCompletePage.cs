using Microsoft.Playwright;

namespace AutoTestsForApplications.ForUI.Pages.SauceDemo;

public class CheckoutCompletePage
{
    private readonly IPage Page;

    private ILocator CompleteHeader => Page.Locator(".complete-header");

    public CheckoutCompletePage(IPage page)
    {
        Page = page;
    }

    public async Task<string> GetHeaderTextAsync()
    {
        return await CompleteHeader.TextContentAsync() ?? string.Empty;
    }
}
