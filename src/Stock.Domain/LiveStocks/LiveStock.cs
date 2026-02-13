using MGH.Core.Domain.Base;
using Stock.Domain.LiveStocks.Events;
using Stock.Domain.LiveStocks.ValueObjects;

namespace Stock.Domain.LiveStocks;

public class LiveStock : AggregateRoot<Guid>
{
    public ItemId ItemId { get; private set; }
    public StoreId StoreId { get; private set; }
    public CurrentStock CurrentStock { get; private set; }
    public Sale? Sale { get; private set; }
    public DateTime? LastSaleDate { get; private set; }
    public DateTime LastStockDate { get; private set; }


    private LiveStock()
    {
    }

    public static LiveStock Create(
        ItemId itemId,
        StoreId storeId,
        CurrentStock currentStock,
        Sale sale,
        DateTime lastSaleDate,
        DateTime lastStockDate)
    {
        var liveStock = new LiveStock
        {
            Id = Guid.NewGuid(),
            ItemId = itemId,
            StoreId = storeId,
            CurrentStock = currentStock,
            Sale = sale,
            LastSaleDate = lastSaleDate,
            LastStockDate = lastStockDate,
        };

        liveStock.AddDomainEvent(new LiveStockAdded(
            itemId, storeId, currentStock, sale, lastSaleDate, lastStockDate));

        return liveStock;
    }

    public void ChangeStock(CurrentStock currentStock, DateTime lastStockDate)
    {
        CurrentStock = currentStock;
        LastStockDate = lastStockDate;
        AddDomainEvent(new LiveStockStockChanged(Id, currentStock, lastStockDate));
    }

    public void ChangeSale(Sale sale, DateTime lastSaleDate)
    {
        Sale = sale;
        LastSaleDate = lastSaleDate;
        AddDomainEvent(new LiveStockSaleChanged(Id, sale, lastSaleDate));
    }

    public void UpdateSale(
        CurrentStock currentStock,
        Sale sale,
        DateTime lastSaleDate,
        DateTime lastStockDate
       )
    {
        CurrentStock = currentStock;
        Sale = sale;
        LastSaleDate = lastSaleDate;
        LastStockDate = lastStockDate;

        AddDomainEvent(new LiveStockUpdated(
            Id, currentStock, sale, lastSaleDate, lastStockDate));
    }
}