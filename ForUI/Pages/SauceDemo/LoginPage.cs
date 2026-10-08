using Microsoft.Playwright;

namespace AutoTestsForApplications.ForUI.Pages.SauceDemo;

public class LoginPage
{
    private readonly IPage Page;

    private ILocator UsernameTextBox => Page.GetByPlaceholder("Username");
    private ILocator PasswordTextBox => Page.GetByPlaceholder("Password");
    private ILocator LoginButton => Page.GetByRole(AriaRole.Button, new() { Name = "Login" });

    // элемент, который есть только на этой странице; тест проверяет его видимость через Expect
    public ILocator UniqueElement => LoginButton;

    public LoginPage(IPage page)
    {
        Page = page;
    }

    public async Task OpenAsync()
    {
        await Page.GotoAsync("https://www.saucedemo.com");
    }

    public async Task LoginAsync(string username, string password)
    {
        await UsernameTextBox.FillAsync(username);
        await PasswordTextBox.FillAsync(password);
        await LoginButton.ClickAsync();
    }
}
