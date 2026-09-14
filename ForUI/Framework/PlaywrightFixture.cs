using Microsoft.Playwright;

namespace AutoTestsForApplications.ForUI.Framework;

public class PlaywrightFixture : IAsyncDisposable
{
    public IPlaywright Playwright { get; private set; }
    public IBrowser Browser { get; private set; }

    public async Task InitializeAsync()
    {
        Playwright = await Microsoft.Playwright.Playwright.CreateAsync();
        Browser = await Playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
        {
            Headless = false, // видим браузер вживую — полезно на этапе обучения
            SlowMo = 500      // небольшая задержка между действиями, чтобы успевать видеть происходящее
        });
    }

    public async ValueTask DisposeAsync()
    {
        if (Browser != null)
        {
            await Browser.CloseAsync();
        }
        Playwright?.Dispose();
    }
}