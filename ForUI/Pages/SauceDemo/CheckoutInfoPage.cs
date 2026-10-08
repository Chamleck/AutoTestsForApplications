using Microsoft.Playwright;

namespace AutoTestsForApplications.ForUI.Pages.SauceDemo;

public class CheckoutInfoPage
{
    private readonly IPage Page;

    private ILocator FirstNameTextBox => Page.GetByPlaceholder("First Name");
    private ILocator LastNameTextBox => Page.GetByPlaceholder("Last Name");
    private ILocator PostalCodeTextBox => Page.GetByPlaceholder("Zip/Postal Code");
    private ILocator ContinueButton => Page.GetByRole(AriaRole.Button, new() { Name = "Continue" });

    // элемент, который есть только на этой странице; тест проверяет его видимость через Expect
    public ILocator UniqueElement => Page.GetByText("Checkout: Your Information", new() { Exact = true });

    public CheckoutInfoPage(IPage page)
    {
        Page = page;
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
