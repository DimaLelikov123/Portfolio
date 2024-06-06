namespace Notebook.Models;

public class CollectionEntry<TKey, TValue>
{
    public readonly TKey key;
    public readonly TValue value;

    public CollectionEntry(TKey key, TValue value)
    {
        this.key = key;
        this.value = value;
    }

    public static bool operator == (CollectionEntry<TKey, TValue> entry1, CollectionEntry<TKey, TValue> entry2)
    {
        return entry1.key?.Equals(entry2.key) == true && entry1.value?.Equals(entry2.value) == true;
    }

    public static bool operator != (CollectionEntry<TKey, TValue> entry1, CollectionEntry<TKey, TValue> entry2)
    {
        return !entry1.key?.Equals(entry2.key) == true || !entry1.value?.Equals(entry2.value) == true;
    }

    public override string ToString()
    {
        return key?.ToString() + " " + value?.ToString();
    }
}