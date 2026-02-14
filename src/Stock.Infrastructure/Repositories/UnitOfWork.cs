using Microsoft.EntityFrameworkCore;
using Stock.Domain;
using Stock.Domain.LiveStocks;
using Stock.Domain.Outboxes;
using Stock.Infrastructure.Contexts;

namespace Stock.Infrastructure.Repositories;

public class UnitOfWork(
    LiveStockDbContext context,
    ILiveStockRepository liveStockRepository,
    IOutboxMessageRepository outBoxRepository)
    : IUow, IDisposable
{
    public IOutboxMessageRepository OutBox => outBoxRepository;
    public ILiveStockRepository LiveStock => liveStockRepository;

    public async Task<int> CompleteAsync(CancellationToken cancellationToken = default)
    {
        var strategy = context.Database.CreateExecutionStrategy();

        return await strategy.ExecuteAsync(async () =>
        {
            await using var transaction =
                await context.Database.BeginTransactionAsync(cancellationToken);

            var result = await context.SaveChangesAsync(cancellationToken);

            await transaction.CommitAsync(cancellationToken);
            return result;
        });
    }

    public void Dispose()
    {
        context.Dispose();
    }
}