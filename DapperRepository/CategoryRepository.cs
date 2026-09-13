using AutoTestsForApplications.DTO.Database;
using AutoTestsForApplications.Interfaces.DapperInterfaces;
using Dapper;
using Microsoft.Data.Sqlite;

namespace AutoTestsForApplications.DapperRepository;

public class CategoryRepository : ICategoryRepository
{
    private readonly string _connectionString;

    public CategoryRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task<IEnumerable<CategoryDTO>> GetAllAsync()
    {
        await using var connection = new SqliteConnection(_connectionString);
        await connection.OpenAsync();

        return await connection.QueryAsync<CategoryDTO>("SELECT * FROM Categories");
    }

    public async Task<CategoryDTO?> GetByNameAsync(string name)
    {
        await using var connection = new SqliteConnection(_connectionString);
        await connection.OpenAsync();

        return await connection.QuerySingleOrDefaultAsync<CategoryDTO>(
            "SELECT * FROM Categories WHERE Name = @Name",
            new { Name = name });
    }
}