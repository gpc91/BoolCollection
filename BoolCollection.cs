using System.Collections;
using System.Text;

namespace Gpc91;

/// <summary>
/// Contains a collection of <see cref="Boolean"/> types that each take up the space of a bit within a byte.
/// The BoolCollection creates a dynamically sized collection to store a collection of booleans. 
/// <remarks>Normally a <see cref="Boolean"/> takes up a single <see cref="Byte"/> of 8-bits. In those 8-bits we can instead, using a collection, store up to 8 boolean values.</remarks>
/// </summary>
public class BoolCollection : IEnumerable
    {
        public dynamic Collection { get; private set; } = 0;

        /// <summary>
        /// The bit-size of the collection
        /// </summary>
        public byte Bits { get; private set; } = 0; 
        /// <summary>
        /// The size of the collection in bytes
        /// </summary>
        public byte Bytes => (byte) (Bits / 0b1000);

        /// <summary>
        /// Creates a <see cref="BoolCollection"/>
        /// </summary>
        /// <param name="values">the initial <see cref="Boolean"/> values to store in the collection</param>
        /// <returns>returns the created <see cref="BoolCollection"/></returns>
        public static BoolCollection Create(params bool[] values)
        {
            BoolCollection collection = new BoolCollection();
            collection.Build(values);
            return collection;
        }

        /// <summary>
        /// Get the <see cref="Boolean"/> value at bit address i
        /// </summary>
        /// <param name="i">the index of the bit to get (little-endian)</param>
        /// <exception cref="IndexOutOfRangeException">throws <see cref="IndexOutOfRangeException"/> if an index greater than the maximum number of bits or a number less than 0 is passed in.</exception>
        public bool this[int i]
        {
            get
            {
                if (i < 0 || i > Bits) throw new IndexOutOfRangeException();
                return Convert.ToBoolean((Collection >> i) & 1);
            }
            set
            {
                if (i < 0 || i > Bits) throw new IndexOutOfRangeException();
                Collection |= (value ? 1 : 0) << i;
            }
        }
        
        /// <summary>
        /// Build a collection of booleans
        /// </summary>
        /// <param name="values">the boolean values to store within the collection</param>
        public void Build(params bool[] values)
        {
            for (byte i = 0; i < values.Length; ++i)
            {
                Collection |= (values[i] ? 1 : 0) << i;
            }
            SetCollectionType();
        }

        /// <summary>
        /// Extract the collection of booleans currently stored in this collection
        /// </summary>
        /// <param name="values">an array containing the booleans in this collection in the order they were added</param>
        /// <returns>true if the values could be extracted</returns>
        public bool Extract(out bool[]? values)
        {
            values = null;
            if (Bits is <= 0 or > 64) return false;
            
            values = new bool[Bits];
            for (byte i = 0; i < Bits; ++i)
            {
                values[i] = Convert.ToBoolean((Collection >> i) & 1);
            }
            return true;
        }
        
        /// <summary>
        /// Set the type of the dynamic collection object
        /// </summary>
        /// <exception cref="Exception"></exception>
        private void SetCollectionType()
        {
            switch ((ulong)Collection)
            {
                case <= byte.MaxValue:
                    Collection = Convert.ChangeType(Collection, typeof(byte));
                    Bits = 8;
                    break;
                case <= ushort.MaxValue:
                    Collection = Convert.ChangeType(Collection, typeof(ushort));
                    Bits = 16;
                    break;
                case <= uint.MaxValue:
                    Collection = Convert.ChangeType(Collection, typeof(uint));
                    Bits = 32;
                    break;
                case <= ulong.MaxValue:
                    Collection = Convert.ChangeType(Collection, typeof(ulong));
                    Bits = 64;
                    break;
                default: throw new Exception("Could not determine type of BoolCollection");
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator) GetEnumerator();
        }
        
        /// <summary>
        /// Get the Enumerator for <see cref="bool"/> types stored within a <see cref="BoolCollection"/>
        /// </summary>
        /// <returns>An enumerator that can iterate over booleans stored within a <see cref="BoolCollection"/></returns>
        public BoolCollectionEnum GetEnumerator()
        {
            return new BoolCollectionEnum(this);
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            for (sbyte i = (sbyte)(Bits - 1); i >= 0; --i)
            {
                stringBuilder.Append((Collection >> i) & 1);
            }
            stringBuilder.Append($" (bits: {Bits}, bytes: {Bytes}, type: {Collection.GetType()}, value: {Collection})");
            return stringBuilder.ToString();
        }
    }