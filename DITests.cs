using AutoTestsForApplications.DI;
using Microsoft.Extensions.DependencyInjection;

namespace AutoTestsForApplications;

public class DITests
{
    [Test]
    public void Test1_ManualInjection_NotifierUsesInjectedSender()
    {
        // зависимость создаётся снаружи и передаётся в конструктор явно
        IEmailSender sender = new EmailSender();
        var notifier = new UserNotifier(sender);

        // просто проверяем, что вызов не падает и проходит через инжектированную зависимость
        Assert.DoesNotThrow(() => notifier.Notify(42));
    }

    [Test]
    public void Test2_ContainerInjection_ResolvesUserNotifierWithDependency()
    {
        var services = new ServiceCollection();

        // регистрируем: "когда кто-то просит IEmailSender — дай EmailSender"
        services.AddTransient<IEmailSender, EmailSender>();
        // регистрируем сам UserNotifier — контейнер сам подставит IEmailSender в конструктор
        services.AddTransient<UserNotifier>();

        using ServiceProvider provider = services.BuildServiceProvider();
        var notifier = provider.GetRequiredService<UserNotifier>();

        Assert.DoesNotThrow(() => notifier.Notify(7));
    }
}