using Stock.Domain.LiveStocks;
using Stock.Infrastructure.Contexts;
using MGH.Core.Infrastructure.Persistence.EF.Base.Repositories;

namespace Stock.Infrastructure.Repositories;

public class LiveStockRepository(LiveStockDbContext _liveStockDbContext) :
    Repository<LiveStock, Guid>(_liveStockDbContext),
    ILiveStockRepository
{
    public IQueryable<LiveStock> Query() =>
        _liveStockDbContext.Set<LiveStock>();
}