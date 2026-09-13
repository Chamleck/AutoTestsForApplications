using AutoTestsForApplications.Database;
using AutoTestsForApplications.Interfaces.DapperInterfaces;
using FluentAssertions;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.DependencyInjection;

namespace AutoTestsForApplications;

public class DatabaseTests
{
    private ServiceProvider _provider;
    private string _dbPath;

    [OneTimeSetUp]
    public async Task Setup()
    {
        // файловая база — репозитории открывают отдельные соединения на каждый вызов,
        // поэтому база должна реально существовать на диске между вызовами
        _dbPath = Path.Combine(AppContext.BaseDirectory, "marketplace.db");
        if (File.Exists(_dbPath))
        {
            File.Delete(_dbPath);
        }

        string connectionString = $"Data Source={_dbPath};";

        // сидинг делаем один раз через отдельное соединение, затем закрываем его
        await using (var seedConnection = new SqliteConnection(connectionString))
        {
            await seedConnection.OpenAsync();
            await DatabaseInitializer.InitializeAsync(seedConnection);
        }

        var services = new ServiceCollection();
        services.AddDataAccess(connectionString);
        _provider = services.BuildServiceProvider();
    }

    [Test]
    public async Task Test2_1_GetAllCategories_ShouldReturnSixCategories()
    {
        var repo = _provider.GetRequiredService<ICategoryRepository>();
        var categories = await repo.GetAllAsync();

        categories.Should().HaveCount(6);
    }

    [Test]
    public async Task Test2_2_GetProductById_ShouldReturnExpectedProduct()
    {
        var repo = _provider.GetRequiredService<IProductRepository>();
        var product = await repo.GetByIdAsync(1);

        product.Should().NotBeNull();
        product!.Name.Should().Be("iPhone 15");
        product.Price.Should().Be(79990);
        product.Stock.Should().Be(15);
        product.CategoryId.Should().Be(1);
    }

    [Test]
    public async Task Test2_3_GetOrderForUser_ShouldContainExpectedItems()
    {
        var orderRepo = _provider.GetRequiredService<IOrderRepository>();
        var orderItemRepo = _provider.GetRequiredService<IOrderItemRepository>();
        var productRepo = _provider.GetRequiredService<IProductRepository>();

        var order = await orderRepo.GetByIdAsync(1);
        order.Should().NotBeNull();
        order!.UserId.Should().Be(1);

        var items = (await orderItemRepo.GetByOrderIdAsync(order.Id)).ToList();
        items.Should().HaveCount(2);

        var productNames = new List<string>();
        foreach (var item in items)
        {
            var product = await productRepo.GetByIdAsync(item.ProductId);
            product.Should().NotBeNull();
            productNames.Add(product!.Name);
        }

        productNames.Should().BeEquivalentTo(new[] { "iPhone 15", "Anker PowerBank" });
    }

    [Test]
    public async Task Test2_AccessoriesBuyers_ShouldLiveInDifferentCities()
    {
        var orderItemRepo = _provider.GetRequiredService<IOrderItemRepository>();

        var locations = (await orderItemRepo.GetPurchaseLocationsByCategoryAsync("Аксессуары")).ToList();

        var distinctCities = locations.Select(l => l.City).Distinct().ToList();
        distinctCities.Count.Should().BeGreaterThan(1);
    }

    [Test]
    public async Task Test3_TvBuyers_ShouldAlsoBuyAccessories()
    {
        var orderItemRepo = _provider.GetRequiredService<IOrderItemRepository>();

        var tvBuyers = (await orderItemRepo.GetPurchaseLocationsByCategoryAsync("Телевизоры"))
            .Select(l => l.UserId)
            .ToList();

        var accessoryBuyers = (await orderItemRepo.GetPurchaseLocationsByCategoryAsync("Аксессуары"))
            .Select(l => l.UserId)
            .ToList();

        // намеренно строгая проверка: по факту нет ни одного юзера, купившего и то и другое —
        // тест должен быть красным, это подтверждено препodом в чате как ожидаемое поведение
        tvBuyers.Should().BeSubsetOf(accessoryBuyers);
    }

    [OneTimeTearDown]
    public void TearDown()
    {
        _provider.Dispose();

        // SQLite пулит соединения на уровне процесса — файл остаётся залоченным
        // даже после Dispose() отдельных подключений, пока пул не очищен явно
        SqliteConnection.ClearAllPools();

        if (File.Exists(_dbPath))
        {
            File.Delete(_dbPath);
        }
    }
}