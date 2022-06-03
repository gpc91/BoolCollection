using System.Collections;

namespace Gpc91;

/// <summary>
/// Exposes an <see cref="IEnumerator"/> that allows iteration over the sequence of <see cref="Boolean"/> values stored in a <see cref="BoolCollection"/>
/// </summary>
public class BoolCollectionEnum : IEnumerator
{

    public BoolCollection Collection;

    private int _bit = -1;
        
    public BoolCollectionEnum(BoolCollection collection)
    {
        Collection = collection;
    }
        
    public bool MoveNext()
    {
        ++_bit;
        return (_bit < Collection.Bits);
    }

    public void Reset()
    {
        _bit = -1;
    }

    object IEnumerator.Current => Current;
    public bool Current => Convert.ToBoolean((Collection.Collection >> _bit) & 1);
}