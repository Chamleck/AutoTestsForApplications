using Microsoft.Playwright;

namespace AutoTestsForApplications.ForUI.Pages.DemoQA;

public class SelectMenuPage : BasePage
{
    // "Select One" - кастомный react-select, не нативный <select>
    private ILocator SelectOneDropdown => Page.Locator("#selectOne");

    // выбранное значение; в имени класса есть хеш, поэтому ищем по подстроке
    private ILocator SelectedValue => SelectOneDropdown.Locator("[class*='singleValue']");

    private ILocator Option(string optionText) =>
        SelectOneDropdown.GetByText(optionText, new() { Exact = true });

    protected override ILocator UniqueElement => SelectOneDropdown;

    public SelectMenuPage(IPage page) : base(page)
    {
    }

    public async Task OpenAsync()
    {
        // DOMContentLoaded вместо load: на demoqa много рекламы, полная загрузка может быть долгой
        await Page.GotoAsync("https://demoqa.com/select-menu", new() { WaitUntil = WaitUntilState.DOMContentLoaded });
    }

    public async Task SelectOptionAsync(string optionText)
    {
        await SelectOneDropdown.ClickAsync();
        await Option(optionText).ClickAsync();
    }

    public async Task<string> GetSelectedOptionAsync()
    {
        string text = await SelectedValue.InnerTextAsync();
        return text.Trim();
    }
}
