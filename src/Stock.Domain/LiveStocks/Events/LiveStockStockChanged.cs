using MGH.Core.Domain.Events;
using Stock.Domain.LiveStocks.ValueObjects;

namespace Stock.Domain.LiveStocks.Events;

public sealed class LiveStockStockChanged(
    Guid liveStockId,
    CurrentStock currentStock,
    DateTime lastStockDate)
    : DomainEvent(new
    {
        liveStockId,
        currentStock,
        lastStockDate
    })
{
    public Guid LiveStockId { get; } = liveStockId;
    public CurrentStock CurrentStock { get; } = currentStock ?? throw new ArgumentNullException(nameof(currentStock));
    public DateTime LastStockDate { get; } = lastStockDate;
}

