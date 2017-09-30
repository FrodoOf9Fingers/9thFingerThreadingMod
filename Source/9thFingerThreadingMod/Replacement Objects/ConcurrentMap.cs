using System;
using System.Collections.Generic;

class ConcurrentMap<K, V>
{
    readonly Dictionary<K, V> _map = new Dictionary<K, V>();

    public bool TryGetValue(K key, out V value)
    {
        lock (_map)
        {
            return _map.TryGetValue(key, out value);
        }
    }

    public bool TryAdd(K key, V value)
    {
        lock (_map)
        {
            if (!_map.ContainsKey(key))
            {
                _map.Add(key, value);
                return true;
            }
            return false;
        }
    }

    public bool TryRemove(K key)
    {
        lock (_map)
        {
            return _map.Remove(key);
        }
    }

    public bool ContainsKey(K key)
    {
        lock (_map)
        {
            return _map.ContainsKey(key);
        }
    }

    public V this[K key]
    {
        get
        {
            V obj;
            TryGetValue(key, out obj);
            return obj;
        }
    }

    public IEnumerable<KeyValuePair<K, V>> enumerate()
    {
        foreach (KeyValuePair<K, V> kv in _map)
        {
            yield return kv;
        }
    }
}