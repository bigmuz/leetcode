namespace LeetCode.Tests;

public class P27_RemoveElementTests
{
    [Theory]
    // Examples from the problem
    [InlineData(new[] { 3, 2, 2, 3 }, 3, new[] { 2, 2 })]
    [InlineData(new[] { 0, 1, 2, 2, 3, 0, 4, 2 }, 2, new[] { 0, 0, 1, 3, 4 })]
    // Empty array
    [InlineData(new int[] { }, 0, new int[] { })]
    // Single element
    [InlineData(new[] { 5 }, 5, new int[] { })]
    [InlineData(new[] { 5 }, 6, new[] { 5 })]
    // Nothing to remove
    [InlineData(new[] { 1, 2, 3 }, 4, new[] { 1, 2, 3 })]
    // Everything removed
    [InlineData(new[] { 2, 2, 2 }, 2, new int[] { })]
    // Position of val
    [InlineData(new[] { 3, 1, 2 }, 3, new[] { 1, 2 })]        // at the start
    [InlineData(new[] { 1, 2, 3 }, 3, new[] { 1, 2 })]        // at the end
    [InlineData(new[] { 1, 3, 2 }, 3, new[] { 1, 2 })]        // in the middle
    [InlineData(new[] { 3, 3, 1, 2 }, 3, new[] { 1, 2 })]     // consecutive at the start
    [InlineData(new[] { 1, 2, 3, 3 }, 3, new[] { 1, 2 })]     // consecutive at the end
    [InlineData(new[] { 3, 1, 3, 2, 3 }, 3, new[] { 1, 2 })]  // alternating
    // Duplicates of other values are kept
    [InlineData(new[] { 1, 1, 2, 2, 3 }, 2, new[] { 1, 1, 3 })]
    // Zero as val and as an element
    [InlineData(new[] { 0, 1, 0, 2 }, 0, new[] { 1, 2 })]
    // Constraint extremes
    [InlineData(new[] { 50, 0, 50 }, 100, new[] { 0, 50, 50 })]
    [InlineData(new[] { 50, 0, 50 }, 50, new[] { 0 })]
    public void RemoveElement_ReturnsKAndKeepsRemainingElements(int[] nums, int val, int[] expected)
    {
        var sol = new P27.Solution();

        int k = sol.RemoveElement(nums, val);

        Assert.Equal(expected.Length, k);
        // Order may change, so sort the first k elements before comparing (same as the judge)
        Assert.Equal(expected.OrderBy(x => x), nums.Take(k).OrderBy(x => x));
        Assert.DoesNotContain(val, nums.Take(k));
    }

    [Fact]
    public void RemoveElement_MaxSize_AllEqualVal()
    {
        int[] nums = Enumerable.Repeat(7, 100).ToArray();

        var sol = new P27.Solution();

        Assert.Equal(0, sol.RemoveElement(nums, 7));
    }

    [Fact]
    public void RemoveElement_MaxSize_NoneEqualVal()
    {
        int[] nums = Enumerable.Range(0, 100).Select(i => i % 51).ToArray();
        int[] original = (int[])nums.Clone();

        var sol = new P27.Solution();

        int k = sol.RemoveElement(nums, 100);

        Assert.Equal(100, k);
        Assert.Equal(original.OrderBy(x => x), nums.Take(k).OrderBy(x => x));
    }

    [Fact]
    public void RemoveElement_MaxSize_HalfRemoved()
    {
        // 100 elements, alternating 3 and 4, remove 3
        int[] nums = Enumerable.Range(0, 100).Select(i => i % 2 == 0 ? 3 : 4).ToArray();

        var sol = new P27.Solution();

        int k = sol.RemoveElement(nums, 3);

        Assert.Equal(50, k);
        Assert.All(nums.Take(k), x => Assert.Equal(4, x));
    }
}