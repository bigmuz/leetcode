namespace LeetCode.Tests;

public class P1_TwoSumTests
{
    [Theory]
    // Examples from the problem
    [InlineData(new[] { 2, 7, 11, 15 }, 9, new[] { 0, 1 })]
    [InlineData(new[] { 3, 2, 4 }, 6, new[] { 1, 2 })]
    [InlineData(new[] { 3, 3 }, 6, new[] { 0, 1 })]
    // Same element can't be used twice (5 + 5 = 10, but there is only one 5)
    [InlineData(new[] { 6, 2, 4, 5 }, 10, new[] { 0, 2 })]
    // Position of the pair
    [InlineData(new[] { 4, 1, 2, 3, 6 }, 10, new[] { 0, 4 })]
    [InlineData(new[] { 1, 5, 9, 13, 17 }, 30, new[] { 3, 4 })]
    // Negatives and zero
    [InlineData(new[] { -1, -2, -3, -4, -5 }, -8, new[] { 2, 4 })]
    [InlineData(new[] { -3, 4, 3, 90 }, 0, new[] { 0, 2 })]
    [InlineData(new[] { 0, 4, 3, 0 }, 0, new[] { 0, 3 })]
    // Constraint extremes
    [InlineData(new[] { -1000000000, 1000000000, 5 }, 0, new[] { 0, 1 })]
    [InlineData(new[] { 500000000, 1, 500000000 }, 1000000000, new[] { 0, 2 })]
    public void TwoSum_ReturnsExpectedIndices(int[] nums, int target, int[] expected)
    {
        var sol = new P1.Solution();

        int[] actual = sol.TwoSum(nums, target);

        Assert.NotNull(actual);
        Assert.Equal(2, actual.Length);
        // Any order is allowed, so compare sorted
        Assert.Equal(expected, actual.OrderBy(i => i).ToArray());
    }

    [Fact]
    public void TwoSum_MaxSize_PairAtEnd()
    {
        // 10,000 elements, only the last two sum to the target
        int[] nums = Enumerable.Repeat(1, 10_000).ToArray();
        nums[9_998] = 500_000_000;
        nums[9_999] = 499_999_999;

        var sol = new P1.Solution();

        int[] actual = sol.TwoSum(nums, 999_999_999);

        Assert.Equal(new[] { 9_998, 9_999 }, actual.OrderBy(i => i).ToArray());
    }
}