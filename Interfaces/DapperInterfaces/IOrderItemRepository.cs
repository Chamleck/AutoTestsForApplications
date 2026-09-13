using AutoTestsForApplications.DTO.Database;

namespace AutoTestsForApplications.Interfaces.DapperInterfaces;

public interface IOrderItemRepository
{
    Task<IEnumerable<OrderItemDTO>> GetByOrderIdAsync(int orderId);

    // джойнит OrderItems -> Products -> Categories -> Orders -> Users -> Addresses,
    // возвращает кто (UserId) и откуда (City) покупал товары конкретной категории
    Task<IEnumerable<PurchaseLocationDTO>> GetPurchaseLocationsByCategoryAsync(string categoryName);
}