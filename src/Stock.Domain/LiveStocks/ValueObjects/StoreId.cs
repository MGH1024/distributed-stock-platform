using MGH.Core.Domain.Base;

namespace Stock.Domain.LiveStocks.ValueObjects;

public class StoreId : ValueObject
{
    public string Value { get; }
    public StoreId(string value)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(value);
        Value = value;
    }

    public static implicit operator string(StoreId storeId) => storeId.Value;
    public static implicit operator StoreId(string storeId) => new(storeId);

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}