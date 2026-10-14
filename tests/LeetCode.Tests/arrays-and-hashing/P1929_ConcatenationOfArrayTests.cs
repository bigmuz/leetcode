namespace LeetCode.Tests;

public class P1929_ConcatenationOfArrayTests
{
    [Theory]
    [InlineData(new[] { 1, 2, 1 }, new[] { 1, 2, 1, 1, 2, 1 })]           // example 1
    [InlineData(new[] { 1, 3, 2, 1 }, new[] { 1, 3, 2, 1, 1, 3, 2, 1 })] // example 2
    [InlineData(new[] { 5 }, new[] { 5, 5 })]                             // smallest input (n = 1)
    public void GetConcatenation_ReturnsNumsTwice(int[] nums, int[] expected)
    {
        var sol = new P1929.Solution();

        int[] actual = sol.GetConcatenation(nums);

        Assert.Equal(expected, actual);
    }
}