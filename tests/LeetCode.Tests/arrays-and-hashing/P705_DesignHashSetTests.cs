namespace LeetCode.Tests;

public class P705_DesignHashSetTests
{
    [Fact]
    public void Example_FromProblem()
    {
        var set = new P705.MyHashSet();

        set.Add(1);
        set.Add(2);
        Assert.True(set.Contains(1));
        Assert.False(set.Contains(3));
        set.Add(2);
        Assert.True(set.Contains(2));
        set.Remove(2);
        Assert.False(set.Contains(2));
    }

    [Fact]
    public void NewSet_ContainsNothing()
    {
        var set = new P705.MyHashSet();

        Assert.False(set.Contains(0));
        Assert.False(set.Contains(1));
        Assert.False(set.Contains(1_000_000));
    }

    [Fact]
    public void Add_ThenContains_ReturnsTrue()
    {
        var set = new P705.MyHashSet();

        set.Add(5);

        Assert.True(set.Contains(5));
    }

    [Fact]
    public void Add_Duplicate_StillSingleEntry()
    {
        var set = new P705.MyHashSet();

        set.Add(7);
        set.Add(7);
        set.Add(7);
        set.Remove(7);

        // One remove must be enough, so duplicates were not stored separately
        Assert.False(set.Contains(7));
    }

    [Fact]
    public void Remove_MissingKey_DoesNothing()
    {
        var set = new P705.MyHashSet();

        set.Add(1);
        set.Remove(2);

        Assert.True(set.Contains(1));
        Assert.False(set.Contains(2));
    }

    [Fact]
    public void Remove_OnEmptySet_DoesNotThrow()
    {
        var set = new P705.MyHashSet();

        set.Remove(10);

        Assert.False(set.Contains(10));
    }

    [Fact]
    public void Remove_ThenAddAgain_ContainsKey()
    {
        var set = new P705.MyHashSet();

        set.Add(3);
        set.Remove(3);
        set.Add(3);

        Assert.True(set.Contains(3));
    }

    [Fact]
    public void Remove_OnlyRemovesThatKey()
    {
        var set = new P705.MyHashSet();

        set.Add(1);
        set.Add(2);
        set.Add(3);
        set.Remove(2);

        Assert.True(set.Contains(1));
        Assert.False(set.Contains(2));
        Assert.True(set.Contains(3));
    }

    [Theory]
    [InlineData(0)]                 // smallest key
    [InlineData(1_000_000)]         // largest key
    public void ConstraintExtremes_AddContainsRemove(int key)
    {
        var set = new P705.MyHashSet();

        set.Add(key);
        Assert.True(set.Contains(key));

        set.Remove(key);
        Assert.False(set.Contains(key));
    }

    [Fact]
    public void KeysThatLikelyCollide_AreKeptSeparate()
    {
        // Keys that share a bucket under common bucket counts (769, 1000, 1009, 10007)
        var set = new P705.MyHashSet();
        int[] keys = { 1, 770, 1539, 2308, 1001, 2001, 1010, 10008 };

        foreach (int k in keys) set.Add(k);

        foreach (int k in keys) Assert.True(set.Contains(k));

        set.Remove(770);

        Assert.False(set.Contains(770));
        Assert.True(set.Contains(1));
        Assert.True(set.Contains(1539));
        Assert.True(set.Contains(2308));
    }

    [Fact]
    public void Remove_MiddleOfCollisionChain_KeepsOthers()
    {
        // 1, 1001, 2001 collide when the bucket count is 1000
        var set = new P705.MyHashSet();

        set.Add(1);
        set.Add(1001);
        set.Add(2001);
        set.Remove(1001);

        Assert.True(set.Contains(1));
        Assert.False(set.Contains(1001));
        Assert.True(set.Contains(2001));
    }

    [Fact]
    public void KeyNotAdded_ButSharesBucketWithAddedKey_ReturnsFalse()
    {
        var set = new P705.MyHashSet();

        set.Add(1);

        Assert.False(set.Contains(1001));
        Assert.False(set.Contains(770));
    }

    [Fact]
    public void ManyOperations_MatchReferenceHashSet()
    {
        // Compare against the built-in HashSet over 10,000 pseudo-random operations.
        // The built-in is only used here in the test, not in your solution.
        var set = new P705.MyHashSet();
        var reference = new HashSet<int>();
        var rng = new Random(12345);

        for (int i = 0; i < 10_000; i++)
        {
            int key = rng.Next(0, 1_000_001);
            // Small range sometimes, to force repeated adds, removes and collisions
            if (i % 3 == 0) key = rng.Next(0, 50);

            switch (rng.Next(3))
            {
                case 0:
                    set.Add(key);
                    reference.Add(key);
                    break;
                case 1:
                    set.Remove(key);
                    reference.Remove(key);
                    break;
                default:
                    Assert.Equal(reference.Contains(key), set.Contains(key));
                    break;
            }
        }
    }

    [Fact]
    public void MaxOperations_AddThenRemoveAll()
    {
        var set = new P705.MyHashSet();

        for (int i = 0; i < 5_000; i++) set.Add(i * 200);
        for (int i = 0; i < 5_000; i++) Assert.True(set.Contains(i * 200));
        for (int i = 0; i < 5_000; i++) set.Remove(i * 200);
        for (int i = 0; i < 5_000; i++) Assert.False(set.Contains(i * 200));
    }
}