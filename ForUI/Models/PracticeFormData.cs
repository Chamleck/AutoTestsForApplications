using AutoTestsForApplications.ForUI.Enums;

namespace AutoTestsForApplications.ForUI.Models;

// данные формы demoqa; null или пустой список - поле не заполняется
public class PracticeFormData
{
    // обязательные поля
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public Gender Gender { get; set; }
    public string Mobile { get; set; } = string.Empty;

    // необязательные поля
    public string? Email { get; set; }
    public DateOnly? DateOfBirth { get; set; }
    public List<string> Subjects { get; set; } = new();
    public List<Hobby> Hobbies { get; set; } = new();
    public string? PicturePath { get; set; }
    public string? CurrentAddress { get; set; }
    public string? State { get; set; }
    public string? City { get; set; }
}
