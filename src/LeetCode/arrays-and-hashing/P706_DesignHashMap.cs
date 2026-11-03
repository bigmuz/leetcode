namespace P706;

// chaining
public class MyHashMap {
    private const int Size = 1000;
    private readonly List<Pair>[] buckets;

    public MyHashMap() {
        buckets = new List<Pair>[Size];

        for (int i = 0; i < buckets.Length; i++)
        {
            buckets[i] = [];
        }
    }

    private int Index(int key)
    {
        return key % buckets.Length;
    }
    
    public void Put(int key, int value) {
        int i = Index(key);

        foreach (var pair in buckets[i])
        {
            if (pair.Key == key)
            {
                pair.Value = value;
                return;
            }
        }

        buckets[i].Add(new Pair(key, value));
    }
    
    public int Get(int key) {
        int i = Index(key);

        foreach (var pair in buckets[i])
        {
            if (pair.Key == key)
            {
                return pair.Value;
            }
        }

        return -1;
    }
    
    public void Remove(int key) {
        int i = Index(key);

        buckets[i].RemoveAll(p => p.Key == key);
    }

    private class Pair
    {
        public int Key;
        public int Value;

        public Pair(int key, int value)
        {
            Key = key;
            Value = value;
        }
    }
}

public class MyHashMapIntArray {
    private readonly int[] values;
    private const int Size = 1000001;

    public MyHashMapIntArray() {
        values = new int[Size];

        // initialise map with -1 values
        Array.Fill(values, -1);
    }
    
    public void Put(int key, int value) {
        values[key] = value;
    }
    
    public int Get(int key) {
        return values[key];
    }
    
    public void Remove(int key) {
        values[key] = -1;
    }
}

/**
 * Your MyHashMap object will be instantiated and called as such:
 * MyHashMap obj = new MyHashMap();
 * obj.Put(key,value);
 * int param_2 = obj.Get(key);
 * obj.Remove(key);
 */