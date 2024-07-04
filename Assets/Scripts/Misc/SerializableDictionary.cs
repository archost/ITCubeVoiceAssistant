using System.Collections.Generic;

[System.Serializable]
public class SerializableDictionary<TKey, TValue>
{
    public List<SerializableKeyValuePair<TKey, TValue>> list = new List<SerializableKeyValuePair<TKey, TValue>>();

    public Dictionary<TKey, TValue> ToDictionary()
    {
        Dictionary<TKey, TValue> dictionary = new Dictionary<TKey, TValue>();
        foreach (var kvp in list)
        {
            dictionary[kvp.key] = kvp.value;
        }
        return dictionary;
    }

    public void FromDictionary(Dictionary<TKey, TValue> dictionary)
    {
        list.Clear();
        foreach (var kvp in dictionary)
        {
            list.Add(new SerializableKeyValuePair<TKey, TValue> { key = kvp.Key, value = kvp.Value });
        }
    }
}