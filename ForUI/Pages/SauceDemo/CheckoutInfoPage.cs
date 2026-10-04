using Microsoft.Playwright;

namespace AutoTestsForApplications.ForUI.Pages.SauceDemo;

public class CheckoutInfoPage : BasePage
{
    private ILocator FirstNameTextBox => Page.GetByPlaceholder("First Name");
    private ILocator LastNameTextBox => Page.GetByPlaceholder("Last Name");
    private ILocator PostalCodeTextBox => Page.GetByPlaceholder("Zip/Postal Code");
    private ILocator ContinueButton => Page.GetByRole(AriaRole.Button, new() { Name = "Continue" });

    protected override ILocator UniqueElement => Page.GetByText("Checkout: Your Information", new() { Exact = true });

    public CheckoutInfoPage(IPage page) : base(page)
    {
    }

    // сайт требует все три поля, иначе покажет ошибку валидации
    public async Task FillFormAsync(string firstName, string lastName, string postalCode)
    {
        await FirstNameTextBox.FillAsync(firstName);
        await LastNameTextBox.FillAsync(lastName);
        await PostalCodeTextBox.FillAsync(postalCode);
    }

    public async Task ClickContinueAsync()
    {
        await ContinueButton.ClickAsync();
    }
}
