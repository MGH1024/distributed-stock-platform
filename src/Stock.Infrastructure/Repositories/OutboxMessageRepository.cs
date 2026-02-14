using MGH.Core.Infrastructure.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Stock.Domain.Outboxes;
using Stock.Infrastructure.Contexts;

namespace Stock.Infrastructure.Repositories;

public class OutboxMessageRepository(LiveStockDbContext liveStockDbContext) : IOutboxMessageRepository
{
    public IQueryable<OutboxMessage> Query() =>
        liveStockDbContext.Set<OutboxMessage>();

    public async Task AddAsync(
        OutboxMessage message,
        CancellationToken cancellationToken = default)
    {
        await liveStockDbContext.AddAsync(message, cancellationToken);
    }

    public async Task AddToOutBoxAsync(
        OutboxMessage message,
        CancellationToken cancellationToken = default)
    {
        await liveStockDbContext.AddAsync(message, cancellationToken);
    }

    public async Task AddToOutBoxRangeAsync(
        IEnumerable<OutboxMessage> messages,
        CancellationToken cancellationToken = default)
    {
        await liveStockDbContext.AddRangeAsync(messages, cancellationToken);
    }

    public async Task<IEnumerable<OutboxMessage>> GetListAsync(
        int pageIndex = 1,
        int pageSize = 10,
        CancellationToken cancellationToken = default)
    {
        return await Query()
            .Take(pageSize)
            .Skip(pageIndex * pageSize)
            .ToListAsync(cancellationToken);
    }

    public OutboxMessage Update(OutboxMessage entity)
    {
        liveStockDbContext.Update(entity);
        return entity;
    }

    public async Task UpdateRangeAsync(
        IEnumerable<OutboxMessage> outboxMessages,
        CancellationToken cancellationToken = default)
    {
        liveStockDbContext.UpdateRange(outboxMessages);
        await liveStockDbContext.SaveChangesAsync(cancellationToken);
    }
}
