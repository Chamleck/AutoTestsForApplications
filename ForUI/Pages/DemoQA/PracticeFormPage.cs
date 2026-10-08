using System.Globalization;
using AutoTestsForApplications.ForUI.Enums;
using AutoTestsForApplications.ForUI.Models;
using Microsoft.Playwright;

namespace AutoTestsForApplications.ForUI.Pages.DemoQA;

public class PracticeFormPage
{
    private readonly IPage Page;

    private ILocator FirstNameTextBox => Page.Locator("#firstName");
    private ILocator LastNameTextBox => Page.Locator("#lastName");
    private ILocator EmailTextBox => Page.Locator("#userEmail");
    private ILocator MobileTextBox => Page.Locator("#userNumber");
    private ILocator DateOfBirthInput => Page.Locator("#dateOfBirthInput");
    private ILocator MonthSelect => Page.Locator(".react-datepicker__month-select");
    private ILocator YearSelect => Page.Locator(".react-datepicker__year-select");
    private ILocator SubjectsInput => Page.Locator("#subjectsInput");
    private ILocator PictureInput => Page.Locator("#uploadPicture");
    private ILocator CurrentAddressTextArea => Page.Locator("#currentAddress");
    private ILocator StateDropdown => Page.Locator("#state");
    private ILocator CityDropdown => Page.Locator("#city");
    private ILocator SubmitButton => Page.Locator("#submit");
    // ячейки таблицы результата идут парами: "Label", "Values"
    private ILocator ResultTableCells => Page.Locator(".modal-body tbody td");

    // id "genterWrapper" с опечаткой - так на самом сайте
    private ILocator GenderRadio(Gender gender) =>
        Page.Locator("#genterWrapper").GetByText(gender.ToString(), new() { Exact = true });

    private ILocator HobbyCheckBox(Hobby hobby) =>
        Page.Locator("#hobbiesWrapper").GetByText(hobby.ToString(), new() { Exact = true });

    // :not(...outside-month) - чтобы не кликнуть такое же число из соседнего месяца
    private ILocator DayCell(int day) =>
        Page.Locator($".react-datepicker__day--{day:D3}:not(.react-datepicker__day--outside-month)");

    // выпадающие списки subjects/state/city - react-select, их пункты имеют роль option
    private ILocator DropdownOption(string text) =>
        Page.GetByRole(AriaRole.Option, new() { Name = text, Exact = true });

    // заголовок модалки с результатом; тест проверяет его видимость и текст через Expect
    public ILocator ResultModalTitle => Page.Locator("#example-modal-sizes-title-lg");

    public ILocator UniqueElement => FirstNameTextBox;

    public PracticeFormPage(IPage page)
    {
        Page = page;
    }

    public async Task OpenAsync()
    {
        // DOMContentLoaded: на demoqa много рекламы, полной загрузки можно ждать долго
        await Page.GotoAsync("https://demoqa.com/automation-practice-form", new() { WaitUntil = WaitUntilState.DOMContentLoaded });
    }

    public async Task FillFormAsync(PracticeFormData data)
    {
        await FirstNameTextBox.FillAsync(data.FirstName);
        await LastNameTextBox.FillAsync(data.LastName);
        await GenderRadio(data.Gender).ClickAsync();
        await MobileTextBox.FillAsync(data.Mobile);

        if (data.Email != null)
        {
            await EmailTextBox.FillAsync(data.Email);
        }

        if (data.DateOfBirth != null)
        {
            await SelectDateOfBirthAsync(data.DateOfBirth.Value);
        }

        foreach (string subject in data.Subjects)
        {
            await SubjectsInput.FillAsync(subject);
            await DropdownOption(subject).ClickAsync();
        }

        foreach (Hobby hobby in data.Hobbies)
        {
            await HobbyCheckBox(hobby).ClickAsync();
        }

        if (data.PicturePath != null)
        {
            await PictureInput.SetInputFilesAsync(data.PicturePath);
        }

        if (data.CurrentAddress != null)
        {
            await CurrentAddressTextArea.FillAsync(data.CurrentAddress);
        }

        if (data.State != null)
        {
            await StateDropdown.ClickAsync();
            await DropdownOption(data.State).ClickAsync();
        }

        if (data.City != null)
        {
            await CityDropdown.ClickAsync();
            await DropdownOption(data.City).ClickAsync();
        }
    }

    public async Task SubmitAsync()
    {
        await SubmitButton.ClickAsync();
    }

    // таблица в модалке: "Label" -> "Values"
    public async Task<Dictionary<string, string>> GetSubmittedValuesAsync()
    {
        IReadOnlyList<string> cells = await ResultTableCells.AllInnerTextsAsync();

        // Chunk(2) режет плоский список ячеек на пары [label, value]
        return cells.Chunk(2).ToDictionary(pair => pair[0].Trim(), pair => pair[1].Trim());
    }

    private async Task SelectDateOfBirthAsync(DateOnly date)
    {
        await DateOfBirthInput.ClickAsync();
        // InvariantCulture: иначе на русской/чешской Windows месяц будет "января"/"ledna"
        await MonthSelect.SelectOptionAsync(new SelectOptionValue { Label = date.ToString("MMMM", CultureInfo.InvariantCulture) });
        await YearSelect.SelectOptionAsync(date.Year.ToString());
        await DayCell(date.Day).ClickAsync();
    }
}
