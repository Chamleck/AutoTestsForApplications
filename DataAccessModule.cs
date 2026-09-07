using AutoTestsForApplications.DapperRepository;
using AutoTestsForApplications.Interfaces.DapperInterfaces;
using Microsoft.Extensions.DependencyInjection;

namespace AutoTestsForApplications;

public static class DataAccessModule
{
    // extension-метод для IServiceCollection — регистрирует все репозитории одним вызовом
    public static IServiceCollection AddDataAccess(this IServiceCollection services, string connectionString)
    {
        services.AddScoped<ICategoryRepository>(_ => new CategoryRepository(connectionString));
        services.AddScoped<IProductRepository>(_ => new ProductRepository(connectionString));
        services.AddScoped<IOrderRepository>(_ => new OrderRepository(connectionString));
        services.AddScoped<IOrderItemRepository>(_ => new OrderItemRepository(connectionString));

        return services;
    }
}