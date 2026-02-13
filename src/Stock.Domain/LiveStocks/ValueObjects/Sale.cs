using MGH.Core.Domain.Base;

namespace Stock.Domain.LiveStocks.ValueObjects;

public class Sale : ValueObject
{
    public decimal Value { get; }

    public Sale(decimal value)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(value);
        Value = value;
    }

    public static implicit operator decimal(Sale sale) => sale.Value;
    public static implicit operator Sale(decimal sale) => new(sale);

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}