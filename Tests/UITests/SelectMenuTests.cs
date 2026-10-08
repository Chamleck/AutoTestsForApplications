using AutoTestsForApplications.ForUI.Pages.DemoQA;
using FluentAssertions;
using Microsoft.Playwright;

namespace AutoTestsForApplications.Tests.UITests;

public class SelectMenuTests : BaseTest
{
    [Test]
    public async Task SelectOne_ChooseProf_ShowsSelectedOption()
    {
        var selectMenuPage = new SelectMenuPage(Page);

        await selectMenuPage.OpenAsync();
        await Assertions.Expect(selectMenuPage.UniqueElement).ToBeVisibleAsync();

        await selectMenuPage.SelectOptionAsync("Prof.");

        (await selectMenuPage.GetSelectedOptionAsync()).Should().Be("Prof.");
    }
}
