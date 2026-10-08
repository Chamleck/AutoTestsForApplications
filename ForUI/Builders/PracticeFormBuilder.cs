using AutoTestsForApplications.ForUI.Enums;
using AutoTestsForApplications.ForUI.Models;

namespace AutoTestsForApplications.ForUI.Builders;

// каждый With-метод возвращает сам builder, поэтому вызовы собираются в цепочку
public class PracticeFormBuilder
{
    private readonly PracticeFormData _data = new();

    public PracticeFormBuilder WithFirstName(string firstName)
    {
        _data.FirstName = firstName;
        return this;
    }

    public PracticeFormBuilder WithLastName(string lastName)
    {
        _data.LastName = lastName;
        return this;
    }

    public PracticeFormBuilder WithGender(Gender gender)
    {
        _data.Gender = gender;
        return this;
    }

    public PracticeFormBuilder WithMobile(string mobile)
    {
        _data.Mobile = mobile;
        return this;
    }

    public PracticeFormBuilder WithEmail(string email)
    {
        _data.Email = email;
        return this;
    }

    public PracticeFormBuilder WithDateOfBirth(DateOnly dateOfBirth)
    {
        _data.DateOfBirth = dateOfBirth;
        return this;
    }

    public PracticeFormBuilder WithSubjects(params string[] subjects)
    {
        _data.Subjects = subjects.ToList();
        return this;
    }

    public PracticeFormBuilder WithHobbies(params Hobby[] hobbies)
    {
        _data.Hobbies = hobbies.ToList();
        return this;
    }

    // путь относительно папки сборки; полный путь нужен для загрузки файла в браузер
    public PracticeFormBuilder WithPicture(string relativePath)
    {
        _data.PicturePath = Path.Combine(AppContext.BaseDirectory, relativePath);
        return this;
    }

    public PracticeFormBuilder WithCurrentAddress(string address)
    {
        _data.CurrentAddress = address;
        return this;
    }

    // город зависит от штата, поэтому задаются вместе
    public PracticeFormBuilder WithStateAndCity(string state, string city)
    {
        _data.State = state;
        _data.City = city;
        return this;
    }

    public PracticeFormData Build()
    {
        return _data;
    }
}
