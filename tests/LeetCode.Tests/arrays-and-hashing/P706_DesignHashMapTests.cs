namespace LeetCode.Tests;

public class P706_DesignHashMapTests
{
    [Fact]
    public void Example_FromProblem()
    {
        var map = new P706.MyHashMap();

        map.Put(1, 1);
        map.Put(2, 2);
        Assert.Equal(1, map.Get(1));
        Assert.Equal(-1, map.Get(3));
        map.Put(2, 1);
        Assert.Equal(1, map.Get(2));
        map.Remove(2);
        Assert.Equal(-1, map.Get(2));
    }

    [Fact]
    public void NewMap_ReturnsMinusOneForAnyKey()
    {
        var map = new P706.MyHashMap();

        Assert.Equal(-1, map.Get(0));
        Assert.Equal(-1, map.Get(1));
        Assert.Equal(-1, map.Get(1_000_000));
    }

    [Fact]
    public void Put_ThenGet_ReturnsValue()
    {
        var map = new P706.MyHashMap();

        map.Put(5, 50);

        Assert.Equal(50, map.Get(5));
    }

    [Fact]
    public void Put_ExistingKey_UpdatesValue()
    {
        var map = new P706.MyHashMap();

        map.Put(4, 10);
        map.Put(4, 20);
        map.Put(4, 30);

        Assert.Equal(30, map.Get(4));
    }

    [Fact]
    public void Put_ExistingKey_DoesNotCreateDuplicate()
    {
        var map = new P706.MyHashMap();

        map.Put(4, 10);
        map.Put(4, 20);
        map.Remove(4);

        // One remove must be enough, so the update replaced the entry instead of adding another
        Assert.Equal(-1, map.Get(4));
    }

    [Fact]
    public void Remove_MissingKey_DoesNothing()
    {
        var map = new P706.MyHashMap();

        map.Put(1, 1);
        map.Remove(2);

        Assert.Equal(1, map.Get(1));
        Assert.Equal(-1, map.Get(2));
    }

    [Fact]
    public void Remove_OnEmptyMap_DoesNotThrow()
    {
        var map = new P706.MyHashMap();

        map.Remove(10);

        Assert.Equal(-1, map.Get(10));
    }

    [Fact]
    public void Remove_ThenPutAgain_ReturnsNewValue()
    {
        var map = new P706.MyHashMap();

        map.Put(3, 30);
        map.Remove(3);
        map.Put(3, 99);

        Assert.Equal(99, map.Get(3));
    }

    [Fact]
    public void Remove_OnlyRemovesThatKey()
    {
        var map = new P706.MyHashMap();

        map.Put(1, 10);
        map.Put(2, 20);
        map.Put(3, 30);
        map.Remove(2);

        Assert.Equal(10, map.Get(1));
        Assert.Equal(-1, map.Get(2));
        Assert.Equal(30, map.Get(3));
    }

    [Fact]
    public void Value_Zero_IsDistinctFromMissing()
    {
        // A stored value of 0 must not be confused with "not found" (-1)
        var map = new P706.MyHashMap();

        map.Put(7, 0);

        Assert.Equal(0, map.Get(7));
        Assert.Equal(-1, map.Get(8));
    }

    [Theory]
    [InlineData(0, 0)]                      // smallest key and value
    [InlineData(1_000_000, 1_000_000)]      // largest key and value
    [InlineData(0, 1_000_000)]
    [InlineData(1_000_000, 0)]
    public void ConstraintExtremes_PutGetRemove(int key, int value)
    {
        var map = new P706.MyHashMap();

        map.Put(key, value);
        Assert.Equal(value, map.Get(key));

        map.Remove(key);
        Assert.Equal(-1, map.Get(key));
    }

    [Fact]
    public void CollidingKeys_KeepTheirOwnValues()
    {
        // 1, 1001, 2001 share a bucket when the bucket count is 1000; 770 and 1539 share one with 769
        var map = new P706.MyHashMap();
        int[] keys = { 1, 1001, 2001, 770, 1539 };

        for (int i = 0; i < keys.Length; i++) map.Put(keys[i], i + 100);

        for (int i = 0; i < keys.Length; i++) Assert.Equal(i + 100, map.Get(keys[i]));
    }

    [Fact]
    public void CollidingKeys_UpdateOnlyAffectsThatKey()
    {
        var map = new P706.MyHashMap();

        map.Put(1, 10);
        map.Put(1001, 20);
        map.Put(2001, 30);
        map.Put(1001, 99);

        Assert.Equal(10, map.Get(1));
        Assert.Equal(99, map.Get(1001));
        Assert.Equal(30, map.Get(2001));
    }

    [Fact]
    public void Remove_MiddleOfCollisionChain_KeepsOthers()
    {
        var map = new P706.MyHashMap();

        map.Put(1, 10);
        map.Put(1001, 20);
        map.Put(2001, 30);
        map.Remove(1001);

        Assert.Equal(10, map.Get(1));
        Assert.Equal(-1, map.Get(1001));
        Assert.Equal(30, map.Get(2001));
    }

    [Fact]
    public void Get_KeyNotAdded_ButSharesBucketWithAddedKey_ReturnsMinusOne()
    {
        var map = new P706.MyHashMap();

        map.Put(1, 10);

        Assert.Equal(-1, map.Get(1001));
        Assert.Equal(-1, map.Get(770));
    }

    [Fact]
    public void ManyOperations_MatchReferenceDictionary()
    {
        // Compare against the built-in Dictionary over 10,000 pseudo-random operations.
        // The built-in is only used here in the test, not in your solution.
        var map = new P706.MyHashMap();
        var reference = new Dictionary<int, int>();
        var rng = new Random(12345);

        for (int i = 0; i < 10_000; i++)
        {
            int key = rng.Next(0, 1_000_001);
            // Small key range sometimes, to force repeated puts, updates and collisions
            if (i % 3 == 0) key = rng.Next(0, 50);

            switch (rng.Next(3))
            {
                case 0:
                    int value = rng.Next(0, 1_000_001);
                    map.Put(key, value);
                    reference[key] = value;
                    break;
                case 1:
                    map.Remove(key);
                    reference.Remove(key);
                    break;
                default:
                    int expected = reference.TryGetValue(key, out var v) ? v : -1;
                    Assert.Equal(expected, map.Get(key));
                    break;
            }
        }
    }

    [Fact]
    public void MaxOperations_PutThenRemoveAll()
    {
        var map = new P706.MyHashMap();

        for (int i = 0; i < 3_000; i++) map.Put(i * 300, i);
        for (int i = 0; i < 3_000; i++) Assert.Equal(i, map.Get(i * 300));
        for (int i = 0; i < 3_000; i++) map.Remove(i * 300);
        for (int i = 0; i < 3_000; i++) Assert.Equal(-1, map.Get(i * 300));
    }
}