namespace AutoTestsForApplications.DI;

public class UserNotifier
{
    private readonly IEmailSender _emailSender;

    // зависимость приходит извне через конструктор — сам класс её не создаёт
    public UserNotifier(IEmailSender emailSender)
    {
        _emailSender = emailSender;
    }

    public void Notify(int userId)
    {
        _emailSender.Send("user@mail.com", $"Hello, user {userId}!");
    }
}