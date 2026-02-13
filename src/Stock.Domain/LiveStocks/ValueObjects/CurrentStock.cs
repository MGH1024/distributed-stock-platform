using MGH.Core.Domain.Base;

namespace Stock.Domain.LiveStocks.ValueObjects;

public class CurrentStock : ValueObject
{
    public decimal Value { get; }

    public CurrentStock(decimal value)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(value);
        Value = value;
    }

    public static implicit operator decimal(CurrentStock currentStock) => currentStock.Value;
    public static implicit operator CurrentStock(decimal stock) => new(stock);

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}