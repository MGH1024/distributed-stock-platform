using MGH.Core.Domain.Events;
using Stock.Domain.LiveStocks.ValueObjects;

namespace Stock.Domain.LiveStocks.Events;

public sealed class LiveStockUpdated : DomainEvent
{
    public Guid LiveStockId { get; }
    public CurrentStock CurrentStock { get; }
    public Sale Sale { get; }
    public DateTime LastSaleDate { get; }
    public DateTime LastStockDate { get; }

    public LiveStockUpdated(
        Guid liveStockId,
        CurrentStock currentStock,
        Sale sale,
        DateTime lastSaleDate,
        DateTime lastStockDate)
        : base(new
        {
            liveStockId,
            currentStock,
            sale,
            lastSaleDate,
            lastStockDate
        })
    {
        if (liveStockId == Guid.Empty)
            throw new ArgumentException("LiveStockId cannot be empty.", nameof(liveStockId));

        LiveStockId = liveStockId;
        CurrentStock = currentStock ?? throw new ArgumentNullException(nameof(currentStock));
        Sale = sale ?? throw new ArgumentNullException(nameof(sale));
        LastSaleDate = lastSaleDate;
        LastStockDate = lastStockDate;
    }
}