using Stock.Domain.LiveStocks.Constants;

namespace Stock.Domain.LiveStocks.Policies;

public class StorePolicy : ILiveStockPolicy
{
    public string GenerateStoreId(StorePolicyData storePolicyData, string storeId)
    {
        var res = storePolicyData.storeDistrict switch
        {
            StoreDistrict.DistrictOne => "1_",
            StoreDistrict.DistrictTwo => "2_",
            StoreDistrict.DistrictThree => "3_",
            _ => ""
        };
        return $"{res}{storeId}";
    }
}