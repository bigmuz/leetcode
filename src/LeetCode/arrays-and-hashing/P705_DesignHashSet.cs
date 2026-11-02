namespace P705;

// using chaining - buckets with List<int> - hold multiple keys in each bucket
public class MyHashSet
{
    private readonly List<int>[] buckets;
    private const int Size = 1000;

    public MyHashSet()
    {
        buckets = new List<int>[Size];

        for (int i = 0; i < buckets.Length; i++)
        {
            buckets[i] = [];
        }
    }

    private int Index(int key)
    {
        return key % buckets.Length;
    }

    public void Add(int key)
    {
        int i = Index(key);

        if (!buckets[i].Contains(key))
        {
            buckets[i].Add(key);
        }
    }

    public void Remove(int key)
    {
        int i = Index(key);

        buckets[i].Remove(key);
    }

    public bool Contains(int key)
    {
        int i = Index(key);

        return buckets[i].Contains(key);
    }
}

// using bool[] - each possible key has a bool value
public class MyHashSetBoolArray
{
    private readonly bool[] keys;

    public MyHashSetBoolArray()
    {
        keys = new bool[1000001]; // max key is 10^6
    }

    public void Add(int key)
    {
        keys[key] = true;
    }

    public void Remove(int key)
    {
        keys[key] = false;
    }

    public bool Contains(int key)
    {
        return keys[key];
    }
}

// using List<int>
public class MyHashSetListInt
{
    private readonly List<int> keys;

    public MyHashSetListInt()
    {
        keys = [];
    }

    public void Add(int key)
    {
        if (!keys.Contains(key))
        {
            keys.Add(key);
        }
    }

    public void Remove(int key)
    {
        keys.Remove(key);
    }

    public bool Contains(int key)
    {
        return keys.Contains(key);
    }
}

/**
 * Your MyHashSet object will be instantiated and called as such:
 * MyHashSet obj = new MyHashSet();
 * obj.Add(key);
 * obj.Remove(key);
 * bool param_3 = obj.Contains(key);
 */