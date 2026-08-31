using AutoTestsForApplications.Database;
using AutoTestsForApplications.DTO.Database;
using Dapper;
using FluentAssertions;
using Microsoft.Data.Sqlite;

namespace AutoTestsForApplications;

public class DatabaseTests
{
    private SqliteConnection _connection;

    [OneTimeSetUp]
    public async Task Setup()
    {
        _connection = new SqliteConnection("Data Source=:memory:");
        await _connection.OpenAsync(); // база начинает существовать только после Open

        await DatabaseInitializer.InitializeAsync(_connection);
    }

    [Test]
    public async Task Test2_1_GetAllCategories_ShouldReturnSixCategories()
    {
        IEnumerable<CategoryDTO> categories =
            await _connection.QueryAsync<CategoryDTO>("SELECT * FROM Categories");

        categories.Should().HaveCount(6);
    }

    [Test]
    public async Task Test2_2_GetProductById_ShouldReturnExpectedProduct()
    {
        const int productId = 1;

        ProductDTO? product = await _connection.QuerySingleOrDefaultAsync<ProductDTO>(
            "SELECT * FROM Products WHERE Id = @Id",
            new { Id = productId });

        product.Should().NotBeNull();
        product!.Name.Should().Be("iPhone 15");
        product.Price.Should().Be(79990);
        product.Stock.Should().Be(15);
        product.CategoryId.Should().Be(1);
    }

    [Test]
    public async Task Test2_3_GetOrderForUser_ShouldContainExpectedItems()
    {
        const int userId = 1;
        const int orderId = 1;

        // сначала убеждаемся, что заказ реально принадлежит этому юзеру
        OrderDTO? order = await _connection.QuerySingleOrDefaultAsync<OrderDTO>(
            "SELECT * FROM Orders WHERE Id = @OrderId AND UserId = @UserId",
            new { OrderId = orderId, UserId = userId });

        order.Should().NotBeNull();

        // джойним OrderItems с Products, чтобы сразу получить название товара
        IEnumerable<OrderItemWithProductNameDTO> items = await _connection.QueryAsync<OrderItemWithProductNameDTO>(
            """
            SELECT oi.ProductId, p.Name AS ProductName, oi.Quantity, oi.UnitPrice
            FROM OrderItems oi
            JOIN Products p ON p.Id = oi.ProductId
            WHERE oi.OrderId = @OrderId
            """,
            new { OrderId = orderId });

        var itemsList = items.ToList();

        itemsList.Should().HaveCount(2);
        itemsList.Should().Contain(i => i.ProductName == "iPhone 15" && i.Quantity == 1);
        itemsList.Should().Contain(i => i.ProductName == "Anker PowerBank" && i.Quantity == 1);
    }

    [OneTimeTearDown]
    public void TearDown()
    {
        _connection.Dispose();
    }
}