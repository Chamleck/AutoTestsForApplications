using Microsoft.Playwright;

namespace AutoTestsForApplications.ForUI.Pages.Heroku;

public class LoginPage : BasePage
{
    private ILocator UsernameTextBox => Page.GetByRole(AriaRole.Textbox, new() { Name = "Username" });
    private ILocator PasswordTextBox => Page.GetByRole(AriaRole.Textbox, new() { Name = "Password" });
    private ILocator LoginButton => Page.GetByRole(AriaRole.Button, new() { Name = "Login" });
    private ILocator FlashMessage => Page.Locator("#flash");

    protected override ILocator UniqueElement => LoginButton;

    public LoginPage(IPage page) : base(page)
    {
    }

    public async Task OpenAsync()
    {
        await Page.GotoAsync("https://the-internet.herokuapp.com/login");
    }

    public async Task LoginAsync(string username, string password)
    {
        await UsernameTextBox.FillAsync(username);
        await PasswordTextBox.FillAsync(password);
        await LoginButton.ClickAsync();
    }

    // #flash показывает и ошибки, и сообщения об успехе, поэтому метод назван нейтрально
    public async Task<string> GetFlashMessageAsync()
    {
        return await FlashMessage.TextContentAsync() ?? string.Empty;
    }
}
