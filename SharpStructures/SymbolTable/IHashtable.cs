using System.Collections;
using System.Collections.Generic;

namespace SharpStructures.SymbolTable
{
    public interface IHashtable<TKey, TValue> : ICollection<KeyValuePair<TKey, TValue>>,
                                                IEnumerable<KeyValuePair<TKey, TValue>>, IEnumerable
    {
        ICollection<TKey> Keys { get; }
        ICollection<TValue> Values { get; }
        TValue this[TKey key] { get; set; }
        void Add(TKey key, TValue value);
        bool ContainsKey(TKey key);
        bool Remove(TKey key);
        bool TryGetValue(TKey key, out TValue value);
    }
}