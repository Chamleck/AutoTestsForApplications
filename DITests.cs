using AutoTestsForApplications.DI;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;

namespace AutoTestsForApplications;

// тестовый дублёр IEmailSender — не отправляет реальные письма,
// а просто запоминает, с чем его вызвали
public class FakeEmailSender : IEmailSender
{
    public string? To;
    public string? Text;
    public int CallCount;

    public void Send(string to, string text)
    {
        To = to;
        Text = text;
        CallCount++;
    }
}

public class DITests
{
    [Test]
    public void Test1_ManualInjection_NotifierCallsSenderExactlyOnce()
    {
        var fakeSender = new FakeEmailSender();
        var notifier = new UserNotifier(fakeSender);

        notifier.Notify(42);

        // проверяем реальное взаимодействие, а не просто "не упало" —
        // это и есть смысл DI: подменить реальную зависимость на тестовую и проверить вызов
        fakeSender.CallCount.Should().Be(1);
        fakeSender.To.Should().Be("user@mail.com");
        fakeSender.Text.Should().Be("Hello, user 42!");
    }

    [Test]
    public void Test2_ContainerInjection_ResolvesUserNotifierWithDependency()
    {
        var services = new ServiceCollection();

        services.AddTransient<IEmailSender, EmailSender>();
        services.AddTransient<UserNotifier>();

        using ServiceProvider provider = services.BuildServiceProvider();
        var notifier = provider.GetRequiredService<UserNotifier>();

        Assert.DoesNotThrow(() => notifier.Notify(7));
    }
}