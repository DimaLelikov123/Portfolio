using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Notebook.Models;

abstract class AbstractCollection<TKey, TValue> where TKey : struct, System.Enum
{
    protected Dictionary<TKey, CollectionEntry<TKey, TValue>> entries;
    public AbstractCollection(IEnumerable<CollectionEntry<TKey, TValue>> entries)
    {
        this.entries = new ();
        foreach(var entry in entries) 
        {
            this.entries.Add(entry.key, entry);
        }
    }

    public CollectionEntry<TKey, TValue>? Get(TKey key)
    {
        if(entries.ContainsKey(key))
            return entries[key];
        return null;
    }

    public virtual void AddEntry(TKey key, TValue value)
    {
        if(!entries.ContainsKey(key))
            entries.Add(key, new(key, value));
        else
            entries[key] = new(key, value);
    }
}