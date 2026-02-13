using Stock.Domain.LiveStocks.ValueObjects;

namespace Stock.Domain.LiveStocks.Factories;

public interface ILiveStockFactory
{
    LiveStock Create(
        int itemId,
        string storeId,
        decimal currentStock,
        decimal sale,
        DateTime lastSaleDate,
        DateTime lastStockDate);
}