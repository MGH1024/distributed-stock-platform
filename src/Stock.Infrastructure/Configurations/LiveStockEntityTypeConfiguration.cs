using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Stock.Domain.LiveStocks;
using Stock.Domain.LiveStocks.ValueObjects;
using Stock.Infrastructure.Configurations.Base;

namespace Stock.Infrastructure.Configurations;

public class LiveStockEntityTypeConfiguration : IEntityTypeConfiguration<LiveStock>
{
    public void Configure(EntityTypeBuilder<LiveStock> builder)
    {
        builder.ToTable(DatabaseTableName.LiveStock, DatabaseSchema.StockSchema);

        builder.Property(t => t.Id).IsRequired();

        var itemIdConvertor = new ValueConverter<ItemId, int>(a => a.Value, a => new ItemId(a));
        builder
            .Property(a => a.ItemId)
            .HasConversion(itemIdConvertor)
            .IsRequired();

        var storeIdConvertor = new ValueConverter<StoreId, string>(a => a.Value, a => new StoreId(a));
        builder
            .Property(a => a.StoreId)
            .HasMaxLength(4)
            .HasConversion(storeIdConvertor)
            .IsRequired();

        var saleConverter = new ValueConverter<Sale?, decimal?>(
            a => a != null ? a.Value : null,
            a => a != null ? new Sale(a.Value) : null
        );
        builder
            .Property(a => a.Sale)
            .HasConversion(saleConverter);

        var currentStockConvertor = new ValueConverter<CurrentStock, decimal>(a => a.Value,
            a => new CurrentStock(a));

        builder
            .Property(a => a.CurrentStock)
            .HasConversion(currentStockConvertor)
            .IsRequired();

        builder.Property(a => a.Version).IsConcurrencyToken();
    }
}