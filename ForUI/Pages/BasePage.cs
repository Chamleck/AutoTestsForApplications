using Microsoft.Playwright;

namespace AutoTestsForApplications.ForUI.Pages;

public abstract class BasePage
{
    private const float OpenedCheckTimeoutMs = 5000;

    protected readonly IPage Page;

    protected BasePage(IPage page)
    {
        Page = page;
    }

    // элемент, который есть только на этой странице: по нему определяем, что открыта именно она
    protected abstract ILocator UniqueElement { get; }

    // чистый запрос без побочных действий; проверку результата делает тест
    public async Task<bool> IsOpenedAsync()
    {
        try
        {
            await UniqueElement.WaitForAsync(new() { State = WaitForSelectorState.Visible, Timeout = OpenedCheckTimeoutMs });
            return true;
        }
        // отдельного типа для таймаута нет, поэтому отличаем его по тексту; остальные ошибки пробрасываются
        catch (PlaywrightException ex) when (ex.Message.Contains("Timeout"))
        {
            return false;
        }
    }
}
