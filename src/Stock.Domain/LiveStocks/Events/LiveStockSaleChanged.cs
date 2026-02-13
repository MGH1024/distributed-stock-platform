using MGH.Core.Domain.Events;
using Stock.Domain.LiveStocks.ValueObjects;

namespace Stock.Domain.LiveStocks.Events;

public sealed class LiveStockSaleChanged(
    Guid liveStockId,
    Sale sale,
    DateTime lastSaleDate) : DomainEvent(new
{
    liveStockId,
    sale,
    lastSaleDate
})
{
    public Guid LiveStockId { get; } = liveStockId;
    public Sale Sale { get; } = sale ?? throw new ArgumentNullException(nameof(sale));
    public DateTime LastSaleDate { get; } = lastSaleDate;
}