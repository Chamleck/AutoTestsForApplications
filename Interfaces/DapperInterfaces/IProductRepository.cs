using AutoTestsForApplications.DTO.Database;

namespace AutoTestsForApplications.Interfaces.DapperInterfaces;

public interface IProductRepository
{
    Task<ProductDTO?> GetByIdAsync(int id);
}