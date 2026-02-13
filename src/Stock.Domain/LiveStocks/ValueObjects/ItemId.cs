using MGH.Core.Domain.Base;

namespace Stock.Domain.LiveStocks.ValueObjects;

public class ItemId : ValueObject
{
    public ItemId(int value)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(value);
        Value = value;
    }

    public int Value { get; }

    public static implicit operator int(ItemId itemId) => itemId.Value;
    public static implicit operator ItemId(int itemId) => new(itemId);

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}