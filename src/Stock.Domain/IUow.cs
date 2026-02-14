using MGH.Core.Infrastructure.Persistence.Base;
using Stock.Domain.LiveStocks;
using Stock.Domain.Outboxes;

namespace Stock.Domain;

public interface IUow : IUnitOfWork
{
    ILiveStockRepository LiveStock { get; }
    IOutboxMessageRepository OutBox { get; }
}