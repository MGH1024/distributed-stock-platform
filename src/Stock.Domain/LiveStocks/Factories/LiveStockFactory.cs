using Stock.Domain.LiveStocks.Constants;
using Stock.Domain.LiveStocks.Policies;
using Stock.Domain.LiveStocks.ValueObjects;

namespace Stock.Domain.LiveStocks.Factories;

public class LiveStockFactory : ILiveStockFactory
{
    private readonly ILiveStockPolicy _policy;

    public LiveStockFactory()
    {
    }

    public LiveStockFactory(ILiveStockPolicy policy)
    {
        _policy = policy;
    }

    public LiveStock Create(
        int itemId,
        string storeId,
        decimal currentStock,
        decimal sale,
        DateTime lastSaleDate,
        DateTime lastStockDate)
    {
        var policyData = new StorePolicyData(StoreDistrict.DistrictOne);

        var newStoreId = _policy.GenerateStoreId(policyData, storeId);

        var liveStockItemId = new ItemId(itemId);
        var liveStockStoreId = new StoreId(newStoreId);
        var liveStockCurrentStock = new CurrentStock(currentStock);
        var liveStockSale = new Sale(sale);

        return LiveStock.Create(
            liveStockItemId,
            liveStockStoreId,
            liveStockCurrentStock,
            liveStockSale,
            lastSaleDate,
            lastStockDate);
    }
}