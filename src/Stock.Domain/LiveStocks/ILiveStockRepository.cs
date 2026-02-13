using MGH.Core.Infrastructure.Persistence.Base;

namespace Stock.Domain.LiveStocks;

public interface ILiveStockRepository: IRepository<LiveStock, Guid>
{
    
}