using System.Collections.Generic;

[System.Serializable]
public class SerializableKeyValuePair<TKey, TValue>
{
    public TKey key;
    public TValue value;

    public KeyValuePair<TKey, TValue> ToPair()
    {
        return new KeyValuePair<TKey, TValue>(key, value);
    }
}