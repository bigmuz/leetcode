namespace LeetCode.Tests;

public class P75_SortColorsTests
{
    [Theory]
    // Examples from the problem
    [InlineData(new[] { 2, 0, 2, 1, 1, 0 }, new[] { 0, 0, 1, 1, 2, 2 })]
    [InlineData(new[] { 2, 0, 1 }, new[] { 0, 1, 2 })]
    // Single element
    [InlineData(new[] { 0 }, new[] { 0 })]
    [InlineData(new[] { 1 }, new[] { 1 })]
    [InlineData(new[] { 2 }, new[] { 2 })]
    // Two elements
    [InlineData(new[] { 1, 0 }, new[] { 0, 1 })]
    [InlineData(new[] { 2, 0 }, new[] { 0, 2 })]
    [InlineData(new[] { 2, 1 }, new[] { 1, 2 })]
    [InlineData(new[] { 0, 2 }, new[] { 0, 2 })]
    // Already sorted, reverse sorted
    [InlineData(new[] { 0, 0, 1, 1, 2, 2 }, new[] { 0, 0, 1, 1, 2, 2 })]
    [InlineData(new[] { 2, 2, 1, 1, 0, 0 }, new[] { 0, 0, 1, 1, 2, 2 })]
    // All identical
    [InlineData(new[] { 0, 0, 0 }, new[] { 0, 0, 0 })]
    [InlineData(new[] { 1, 1, 1 }, new[] { 1, 1, 1 })]
    [InlineData(new[] { 2, 2, 2 }, new[] { 2, 2, 2 })]
    // Only two colors present
    [InlineData(new[] { 1, 0, 1, 0 }, new[] { 0, 0, 1, 1 })]
    [InlineData(new[] { 2, 0, 2, 0 }, new[] { 0, 0, 2, 2 })]
    [InlineData(new[] { 2, 1, 2, 1 }, new[] { 1, 1, 2, 2 })]
    // Missing a color in the middle (0s and 2s only)
    [InlineData(new[] { 2, 2, 0, 0, 2, 0 }, new[] { 0, 0, 0, 2, 2, 2 })]
    // Position of each color
    [InlineData(new[] { 0, 2, 1 }, new[] { 0, 1, 2 })]
    [InlineData(new[] { 1, 2, 0 }, new[] { 0, 1, 2 })]
    [InlineData(new[] { 1, 0, 2 }, new[] { 0, 1, 2 })]
    // Traps for one-pass two-pointer solutions (swapping a 2 in from the end brings back a 0 or 1)
    [InlineData(new[] { 2, 0, 1 }, new[] { 0, 1, 2 })]
    [InlineData(new[] { 1, 2, 0, 0 }, new[] { 0, 0, 1, 2 })]
    [InlineData(new[] { 2, 2, 0, 1 }, new[] { 0, 1, 2, 2 })]
    [InlineData(new[] { 0, 1, 2, 0, 1, 2 }, new[] { 0, 0, 1, 1, 2, 2 })]
    public void SortColors_SortsInPlace(int[] nums, int[] expected)
    {
        var sol = new P75.Solution();

        sol.SortColors(nums);

        // The method returns void, so the input array itself must be sorted
        Assert.Equal(expected, nums);
    }

    [Fact]
    public void SortColors_MaxSize_Random()
    {
        var rng = new Random(12345);
        int[] nums = Enumerable.Range(0, 300).Select(_ => rng.Next(0, 3)).ToArray();
        int[] expected = nums.OrderBy(x => x).ToArray();

        var sol = new P75.Solution();

        sol.SortColors(nums);

        Assert.Equal(expected, nums);
    }

    [Fact]
    public void SortColors_MaxSize_ReverseSorted()
    {
        int[] nums = Enumerable.Repeat(2, 100)
            .Concat(Enumerable.Repeat(1, 100))
            .Concat(Enumerable.Repeat(0, 100))
            .ToArray();
        int[] expected = nums.OrderBy(x => x).ToArray();

        var sol = new P75.Solution();

        sol.SortColors(nums);

        Assert.Equal(expected, nums);
    }

    [Fact]
    public void SortColors_MaxSize_Alternating()
    {
        int[] nums = Enumerable.Range(0, 300).Select(i => i % 3).ToArray();
        int[] expected = nums.OrderBy(x => x).ToArray();

        var sol = new P75.Solution();

        sol.SortColors(nums);

        Assert.Equal(expected, nums);
    }

    [Fact]
    public void SortColors_MaxSize_AllIdentical()
    {
        int[] nums = Enumerable.Repeat(1, 300).ToArray();

        var sol = new P75.Solution();

        sol.SortColors(nums);

        Assert.All(nums, x => Assert.Equal(1, x));
    }

    [Fact]
    public void SortColors_KeepsCountsOfEachColor()
    {
        // Guards against solutions that lose or invent elements
        int[] nums = [2, 1, 0, 2, 1, 0, 2, 2, 0, 1];
        int[] original = (int[])nums.Clone();

        var sol = new P75.Solution();

        sol.SortColors(nums);

        Assert.Equal(original.Length, nums.Length);
        Assert.Equal(original.Count(x => x == 0), nums.Count(x => x == 0));
        Assert.Equal(original.Count(x => x == 1), nums.Count(x => x == 1));
        Assert.Equal(original.Count(x => x == 2), nums.Count(x => x == 2));
    }
}