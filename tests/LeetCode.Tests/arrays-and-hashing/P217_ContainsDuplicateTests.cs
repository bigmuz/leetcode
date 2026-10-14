namespace LeetCode.Tests;

public class P217_ContainsDuplicateTests
{
    [Theory]
    // Examples from the problem
    [InlineData(new[] { 1, 2, 3, 1 }, true)]
    [InlineData(new[] { 1, 2, 3, 4 }, false)]
    [InlineData(new[] { 1, 1, 1, 3, 3, 4, 3, 2, 4, 2 }, true)]
    // Smallest input (n = 1) can never contain a duplicate
    [InlineData(new[] { 7 }, false)]
    // n = 2
    [InlineData(new[] { 7, 7 }, true)]
    [InlineData(new[] { 7, 8 }, false)]
    // Position of the duplicate
    [InlineData(new[] { 5, 5, 1, 2, 3 }, true)]    // adjacent, at the start
    [InlineData(new[] { 1, 2, 3, 5, 5 }, true)]    // adjacent, at the end
    [InlineData(new[] { 5, 1, 2, 3, 5 }, true)]    // first and last element
    // Value edge cases
    [InlineData(new[] { 0, 0 }, true)]
    [InlineData(new[] { -1, 1 }, false)]           // same magnitude, different sign
    [InlineData(new[] { -3, -3 }, true)]           // negative duplicates
    [InlineData(new[] { -1000000000, 1000000000 }, false)] // constraint extremes, distinct
    [InlineData(new[] { 1000000000, 1000000000 }, true)]   // constraint extremes, duplicate
    public void ContainsDuplicate_ReturnsExpected(int[] nums, bool expected)
    {
        var sol = new P217.Solution();

        bool actual = sol.ContainsDuplicate(nums);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ContainsDuplicate_MaxSizeAllDistinct_ReturnsFalse()
    {
        // 100,000 distinct values: the worst case for "no duplicate found"
        int[] nums = Enumerable.Range(0, 100_000).ToArray();

        var sol = new P217.Solution();

        Assert.False(sol.ContainsDuplicate(nums));
    }

    [Fact]
    public void ContainsDuplicate_MaxSizeDuplicateAtEnd_ReturnsTrue()
    {
        // 100,000 elements where only the last one repeats the first
        int[] nums = Enumerable.Range(0, 100_000).ToArray();
        nums[^1] = nums[0];

        var sol = new P217.Solution();

        Assert.True(sol.ContainsDuplicate(nums));
    }
}