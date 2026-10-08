using System.Globalization;
using AutoTestsForApplications.ForUI.Builders;
using AutoTestsForApplications.ForUI.Enums;
using AutoTestsForApplications.ForUI.Models;
using AutoTestsForApplications.ForUI.Pages.DemoQA;
using FluentAssertions;
using Microsoft.Playwright;

namespace AutoTestsForApplications.Tests.UITests;

public class PracticeFormTests : BaseTest
{
    private const string SuccessMessage = "Thanks for submitting the form";

    private PracticeFormPage _formPage = null!;

    [SetUp]
    public async Task OpenForm()
    {
        _formPage = new PracticeFormPage(Page);
        await _formPage.OpenAsync();
        await Assertions.Expect(_formPage.UniqueElement).ToBeVisibleAsync();
    }

    [Test]
    public async Task Submit_RequiredFieldsOnly_ShowsSubmittedData()
    {
        PracticeFormData student = new PracticeFormBuilder()
            .WithFirstName("Nick")
            .WithLastName("Tester")
            .WithGender(Gender.Male)
            .WithMobile("7771234567")
            .Build();

        await _formPage.FillFormAsync(student);
        await _formPage.SubmitAsync();

        await Assertions.Expect(_formPage.ResultModalTitle).ToHaveTextAsync(SuccessMessage);
        (await _formPage.GetSubmittedValuesAsync()).Should().Contain(BuildExpectedValues(student));
    }

    [Test]
    public async Task Submit_AllFields_ShowsSubmittedData()
    {
        PracticeFormData student = new PracticeFormBuilder()
            .WithFirstName("Anna")
            .WithLastName("Tester")
            .WithEmail("anna.tester@example.com")
            .WithGender(Gender.Female)
            .WithMobile("7779876543")
            .WithDateOfBirth(new DateOnly(1993, 5, 15))
            .WithSubjects("Maths", "English")
            .WithHobbies(Hobby.Sports, Hobby.Music)
            .WithPicture("Resources/Images/avatar.png")
            .WithCurrentAddress("221B Baker Street, London")
            .WithStateAndCity("NCR", "Delhi")
            .Build();

        await _formPage.FillFormAsync(student);
        await _formPage.SubmitAsync();

        await Assertions.Expect(_formPage.ResultModalTitle).ToHaveTextAsync(SuccessMessage);
        (await _formPage.GetSubmittedValuesAsync()).Should().Contain(BuildExpectedValues(student));
    }

    // ожидаемые строки таблицы строятся из тех же данных, что ушли в форму: один источник правды
    private static Dictionary<string, string> BuildExpectedValues(PracticeFormData data)
    {
        var expected = new Dictionary<string, string>
        {
            ["Student Name"] = $"{data.FirstName} {data.LastName}",
            ["Gender"] = data.Gender.ToString(),
            ["Mobile"] = data.Mobile
        };

        if (data.Email != null)
        {
            expected["Student Email"] = data.Email;
        }

        // формат даты в таблице сайта: "15 May,1993"
        if (data.DateOfBirth != null)
        {
            expected["Date of Birth"] = data.DateOfBirth.Value.ToString("dd MMMM,yyyy", CultureInfo.InvariantCulture);
        }

        if (data.Subjects.Count > 0)
        {
            expected["Subjects"] = string.Join(", ", data.Subjects);
        }

        if (data.Hobbies.Count > 0)
        {
            expected["Hobbies"] = string.Join(", ", data.Hobbies);
        }

        if (data.PicturePath != null)
        {
            expected["Picture"] = Path.GetFileName(data.PicturePath);
        }

        if (data.CurrentAddress != null)
        {
            expected["Address"] = data.CurrentAddress;
        }

        if (data.State != null && data.City != null)
        {
            expected["State and City"] = $"{data.State} {data.City}";
        }

        return expected;
    }
}
