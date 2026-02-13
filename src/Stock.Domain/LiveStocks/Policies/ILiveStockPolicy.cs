namespace Stock.Domain.LiveStocks.Policies;

public interface ILiveStockPolicy
{
    string GenerateStoreId(StorePolicyData store,string storeId);
}