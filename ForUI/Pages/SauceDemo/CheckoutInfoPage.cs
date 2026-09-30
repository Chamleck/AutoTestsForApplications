using Microsoft.Playwright;

namespace AutoTestsForApplications.ForUI.Pages.SauceDemo;

public class CheckoutInfoPage
{
    private readonly IPage Page;

    private ILocator FirstNameTextBox => Page.GetByPlaceholder("First Name");
    private ILocator LastNameTextBox => Page.GetByPlaceholder("Last Name");
    private ILocator PostalCodeTextBox => Page.GetByPlaceholder("Zip/Postal Code");
    private ILocator ContinueButton => Page.GetByRole(AriaRole.Button, new() { Name = "Continue" });

    public CheckoutInfoPage(IPage page)
    {
        Page = page;
    }

    // сайт требует все три поля, иначе покажет ошибку валидации и не пустит дальше
    public async Task FillFormAndContinueAsync(string firstName, string lastName, string postalCode)
    {
        await FirstNameTextBox.FillAsync(firstName);
        await LastNameTextBox.FillAsync(lastName);
        await PostalCodeTextBox.FillAsync(postalCode);
        await ContinueButton.ClickAsync();
        await Page.WaitForURLAsync("**/checkout-step-two.html");
    }
}
