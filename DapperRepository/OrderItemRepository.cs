using AutoTestsForApplications.DTO.Database;
using AutoTestsForApplications.Interfaces.DapperInterfaces;
using Dapper;
using Microsoft.Data.Sqlite;

namespace AutoTestsForApplications.DapperRepository;

public class OrderItemRepository : IOrderItemRepository
{
    private readonly string _connectionString;

    public OrderItemRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task<IEnumerable<OrderItemDTO>> GetByOrderIdAsync(int orderId)
    {
        await using var connection = new SqliteConnection(_connectionString);
        await connection.OpenAsync();

        return await connection.QueryAsync<OrderItemDTO>(
            "SELECT * FROM OrderItems WHERE OrderId = @OrderId",
            new { OrderId = orderId });
    }

    // кто и откуда покупал товары заданной категории —
    // джойним всю цепочку OrderItems -> Products -> Categories -> Orders -> Addresses
    public async Task<IEnumerable<PurchaseLocationDTO>> GetPurchaseLocationsByCategoryAsync(string categoryName)
    {
        await using var connection = new SqliteConnection(_connectionString);
        await connection.OpenAsync();

        return await connection.QueryAsync<PurchaseLocationDTO>(
            """
            SELECT DISTINCT o.UserId AS UserId, a.City AS City
            FROM OrderItems oi
            JOIN Products p ON p.Id = oi.ProductId
            JOIN Categories c ON c.Id = p.CategoryId
            JOIN Orders o ON o.Id = oi.OrderId
            JOIN Addresses a ON a.UserId = o.UserId
            WHERE c.Name = @CategoryName
            """,
            new { CategoryName = categoryName });
    }
}