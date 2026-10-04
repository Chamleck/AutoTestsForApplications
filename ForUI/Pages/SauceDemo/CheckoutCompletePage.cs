using Microsoft.Playwright;

namespace AutoTestsForApplications.ForUI.Pages.SauceDemo;

public class CheckoutCompletePage : BasePage
{
    private ILocator CompleteHeader => Page.Locator(".complete-header");

    protected override ILocator UniqueElement => Page.GetByText("Checkout: Complete!", new() { Exact = true });

    public CheckoutCompletePage(IPage page) : base(page)
    {
    }

    public async Task<string> GetHeaderTextAsync()
    {
        return await CompleteHeader.TextContentAsync() ?? string.Empty;
    }
}
