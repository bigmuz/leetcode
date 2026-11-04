namespace LeetCode.Tests;

public class P912_SortAnArrayTests
{
    [Theory]
    // Examples from the problem
    [InlineData(new[] { 5, 2, 3, 1 }, new[] { 1, 2, 3, 5 })]
    [InlineData(new[] { 5, 1, 1, 2, 0, 0 }, new[] { 0, 0, 1, 1, 2, 5 })]
    // Single element
    [InlineData(new[] { 1 }, new[] { 1 })]
    // Two elements
    [InlineData(new[] { 2, 1 }, new[] { 1, 2 })]
    [InlineData(new[] { 1, 2 }, new[] { 1, 2 })]
    [InlineData(new[] { 3, 3 }, new[] { 3, 3 })]
    // Already sorted, reverse sorted
    [InlineData(new[] { 1, 2, 3, 4, 5 }, new[] { 1, 2, 3, 4, 5 })]
    [InlineData(new[] { 5, 4, 3, 2, 1 }, new[] { 1, 2, 3, 4, 5 })]
    // All identical
    [InlineData(new[] { 4, 4, 4, 4 }, new[] { 4, 4, 4, 4 })]
    // Many duplicates
    [InlineData(new[] { 3, 1, 2, 3, 1, 2, 3, 1, 2 }, new[] { 1, 1, 1, 2, 2, 2, 3, 3, 3 })]
    // Negatives, zero, mixed
    [InlineData(new[] { -1, -3, -2 }, new[] { -3, -2, -1 })]
    [InlineData(new[] { 0, -1, 1 }, new[] { -1, 0, 1 })]
    [InlineData(new[] { 3, -2, 0, -5, 4, 0 }, new[] { -5, -2, 0, 0, 3, 4 })]
    // Constraint extremes
    [InlineData(new[] { 50000, -50000 }, new[] { -50000, 50000 })]
    [InlineData(new[] { 50000, 0, -50000, 50000, -50000 }, new[] { -50000, -50000, 0, 50000, 50000 })]
    // Odd and even lengths (matters for merge/split midpoints)
    [InlineData(new[] { 3, 1, 2 }, new[] { 1, 2, 3 })]
    [InlineData(new[] { 4, 3, 2, 1 }, new[] { 1, 2, 3, 4 })]
    [InlineData(new[] { 2, 5, 1, 4, 3, 6, 0 }, new[] { 0, 1, 2, 3, 4, 5, 6 })]
    // Pivot-unfriendly patterns for quicksort
    [InlineData(new[] { 1, 3, 2, 3, 1, 2 }, new[] { 1, 1, 2, 2, 3, 3 })]
    [InlineData(new[] { 2, 1, 2, 1, 2, 1 }, new[] { 1, 1, 1, 2, 2, 2 })]
    public void SortArray_ReturnsSorted(int[] nums, int[] expected)
    {
        var sol = new P912.Solution();

        int[] actual = sol.SortArray(nums);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void SortArray_MaxSize_Random()
    {
        var rng = new Random(12345);
        int[] nums = Enumerable.Range(0, 50_000).Select(_ => rng.Next(-50_000, 50_001)).ToArray();
        int[] expected = nums.OrderBy(x => x).ToArray();

        var sol = new P912.Solution();

        Assert.Equal(expected, sol.SortArray(nums));
    }

    [Fact]
    public void SortArray_MaxSize_AlreadySorted()
    {
        // Worst case for a naive first-element-pivot quicksort (O(n^2), deep recursion)
        int[] nums = Enumerable.Range(-25_000, 50_000).ToArray();
        int[] expected = (int[])nums.Clone();

        var sol = new P912.Solution();

        Assert.Equal(expected, sol.SortArray(nums));
    }

    [Fact]
    public void SortArray_MaxSize_ReverseSorted()
    {
        int[] nums = Enumerable.Range(-25_000, 50_000).Reverse().ToArray();
        int[] expected = nums.OrderBy(x => x).ToArray();

        var sol = new P912.Solution();

        Assert.Equal(expected, sol.SortArray(nums));
    }

    [Fact]
    public void SortArray_MaxSize_AllIdentical()
    {
        // Worst case for a quicksort that doesn't handle duplicates well
        int[] nums = Enumerable.Repeat(7, 50_000).ToArray();

        var sol = new P912.Solution();

        Assert.Equal(nums, sol.SortArray(nums));
    }

    [Fact]
    public void SortArray_MaxSize_FewDistinctValues()
    {
        int[] nums = Enumerable.Range(0, 50_000).Select(i => i % 3 - 1).ToArray();
        int[] expected = nums.OrderBy(x => x).ToArray();

        var sol = new P912.Solution();

        Assert.Equal(expected, sol.SortArray(nums));
    }

    [Fact]
    public void SortArray_ResultIsPermutationOfInput()
    {
        // Guards against solutions that lose or invent elements
        int[] nums = [9, -4, 9, 0, 3, -4, 7, 7, 1];
        int[] original = (int[])nums.Clone();

        var sol = new P912.Solution();

        int[] actual = sol.SortArray(nums);

        Assert.Equal(original.Length, actual.Length);
        Assert.Equal(original.OrderBy(x => x), actual.OrderBy(x => x));
        Assert.Equal(actual.OrderBy(x => x), actual);
    }
}