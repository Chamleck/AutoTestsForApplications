using AutoTestsForApplications.ForUI.Pages.DemoQA;
using FluentAssertions;

namespace AutoTestsForApplications.Tests.UITests;

public class SelectMenuTests : BaseTest
{
    [Test]
    public async Task SelectOne_ChooseProf_ShowsSelectedOption()
    {
        var selectMenuPage = new SelectMenuPage(Page);

        await selectMenuPage.OpenAsync();
        (await selectMenuPage.IsOpenedAsync()).Should().BeTrue("должна открыться страница Select Menu");

        await selectMenuPage.SelectOptionAsync("Prof.");

        (await selectMenuPage.GetSelectedOptionAsync()).Should().Be("Prof.");
    }
}
