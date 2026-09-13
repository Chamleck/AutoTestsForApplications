using AutoTestsForApplications.DTO.Database;

namespace AutoTestsForApplications.Interfaces.DapperInterfaces;

public interface IOrderRepository
{
    Task<OrderDTO?> GetByIdAsync(int id);
}