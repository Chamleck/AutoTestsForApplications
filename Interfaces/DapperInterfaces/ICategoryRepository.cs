using AutoTestsForApplications.DTO.Database;

namespace AutoTestsForApplications.Interfaces.DapperInterfaces;

public interface ICategoryRepository
{
    Task<IEnumerable<CategoryDTO>> GetAllAsync();
    Task<CategoryDTO?> GetByNameAsync(string name);
}