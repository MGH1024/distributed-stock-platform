using MGH.Core.Domain.Events;
using Stock.Domain.LiveStocks.ValueObjects;

namespace Stock.Domain.LiveStocks.Events;

public sealed class LiveStockAdded : DomainEvent
{
    public ItemId ItemId { get; }
    public StoreId StoreId { get; }
    public CurrentStock CurrentStock { get; }
    public Sale Sale { get; }
    public DateTime LastSaleDate { get; }
    public DateTime LastStockDate { get; }

    public LiveStockAdded(
        ItemId itemId,
        StoreId storeId,
        CurrentStock currentStock,
        Sale sale,
        DateTime lastSaleDate,
        DateTime lastStockDate)
        : base(new
        {
            itemId,
            storeId,
            currentStock,
            sale,
            lastSaleDate,
            lastStockDate
        })
    {
        ItemId = itemId ?? throw new ArgumentNullException(nameof(itemId));
        StoreId = storeId ?? throw new ArgumentNullException(nameof(storeId));
        CurrentStock = currentStock ?? throw new ArgumentNullException(nameof(currentStock));
        Sale = sale ?? throw new ArgumentNullException(nameof(sale));

        LastSaleDate = lastSaleDate;
        LastStockDate = lastStockDate;
    }
}