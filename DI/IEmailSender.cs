namespace AutoTestsForApplications.DI;

public interface IEmailSender
{
    void Send(string to, string text);
}