using AutoTestsForApplications.Interfaces;
using AutoTestsForApplications.Utils;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Refit;

namespace AutoTestsForApplications;

public class PetStoreTests
{
    private ServiceProvider _provider = null!;
    private IPetStoreApi _api = null!;

    [OneTimeSetUp]
    public void Setup()
    {
        var services = new ServiceCollection();

        services.AddRefitClient<IPetStoreApi>()
            .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://petstoreapi.com/v1"))
            // у демо-сервера в референсах не проходила проверка TLS-сертификата; отключаем её только для этого клиента
            .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
            });

        _provider = services.BuildServiceProvider();
        _api = _provider.GetRequiredService<IPetStoreApi>();
    }

    [OneTimeTearDown]
    public void TearDown()
    {
        _provider.Dispose();
    }

    [Test]
    public async Task GetAllPets_ReturnsNonEmptyListOfPets()
    {
        var response = await _api.GetAllPetsAsync();

        response.Data.Should().NotBeEmpty();
        response.Data.Should().OnlyContain(p => !string.IsNullOrEmpty(p.Id) && !string.IsNullOrEmpty(p.Name));
    }

    [Test]
    public async Task GetPetById_ReturnsSamePetAsInList()
    {
        var all = await _api.GetAllPetsAsync();
        var expected = RandomHelper.GetRandomItem(all.Data);

        var actual = await _api.GetPetByIdAsync(expected.Id);

        actual.Id.Should().Be(expected.Id);
        actual.Name.Should().Be(expected.Name);
        actual.Species.Should().Be(expected.Species);
        actual.AgeMonths.Should().Be(expected.AgeMonths);
    }

    [Test]
    public async Task GetPetsByStatusAndLimit_ReturnsRequestedNumberOfAdoptedPets()
    {
        var response = await _api.GetPetsByStatusAsync("ADOPTED", 12);

        response.Data.Should().HaveCount(12);
        response.Data.Select(p => p.Status).Should().AllBe("ADOPTED");
    }
}
