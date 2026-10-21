namespace LeetCode.Tests;

public class P347_TopKFrequentElementsTests
{
    [Theory]
    // Examples from the problem
    [InlineData(new[] { 1, 1, 1, 2, 2, 3 }, 2, new[] { 1, 2 })]
    [InlineData(new[] { 1 }, 1, new[] { 1 })]
    [InlineData(new[] { 1, 2, 1, 2, 1, 2, 3, 1, 3, 2 }, 2, new[] { 1, 2 })]
    // k = 1: only the single most frequent
    [InlineData(new[] { 4, 4, 4, 5, 5, 6 }, 1, new[] { 4 })]
    // k = number of unique elements: everything is returned
    [InlineData(new[] { 1, 2, 3 }, 3, new[] { 1, 2, 3 })]
    [InlineData(new[] { 1, 1, 2, 2, 3 }, 3, new[] { 1, 2, 3 })]
    // Most frequent element is not first or last
    [InlineData(new[] { 1, 2, 2, 2, 3 }, 1, new[] { 2 })]
    [InlineData(new[] { 3, 1, 1, 1, 2, 2 }, 2, new[] { 1, 2 })]
    // Frequent element appears only at the end
    [InlineData(new[] { 1, 2, 3, 3, 3, 3 }, 1, new[] { 3 })]
    // Negatives and zero
    [InlineData(new[] { -1, -1, -2 }, 1, new[] { -1 })]
    [InlineData(new[] { 0, 0, 0, -1, -1, 5 }, 2, new[] { -1, 0 })]
    [InlineData(new[] { -1, 1, 1 }, 1, new[] { 1 })]
    // Constraint extremes for values
    [InlineData(new[] { -10000, -10000, 10000 }, 1, new[] { -10000 })]
    [InlineData(new[] { 10000, 10000, 10000, -10000, -10000, 0 }, 2, new[] { -10000, 10000 })]
    // Frequencies spread out (1, 2, 3, 4 occurrences)
    [InlineData(new[] { 1, 2, 2, 3, 3, 3, 4, 4, 4, 4 }, 2, new[] { 3, 4 })]
    [InlineData(new[] { 1, 2, 2, 3, 3, 3, 4, 4, 4, 4 }, 3, new[] { 2, 3, 4 })]
    public void TopKFrequent_ReturnsExpected(int[] nums, int k, int[] expected)
    {
        var sol = new P347.Solution();

        int[] actual = sol.TopKFrequent(nums, k);

        Assert.NotNull(actual);
        Assert.Equal(k, actual.Length);
        // Any order is allowed, so compare sorted
        Assert.Equal(expected.OrderBy(x => x), actual.OrderBy(x => x));
    }

    [Fact]
    public void TopKFrequent_MaxSize_AllSameElement()
    {
        int[] nums = Enumerable.Repeat(7, 100_000).ToArray();

        var sol = new P347.Solution();

        Assert.Equal(new[] { 7 }, sol.TopKFrequent(nums, 1));
    }

    [Fact]
    public void TopKFrequent_MaxSize_ManyUniqueValues()
    {
        // Values 0..9999 each appear 10 times (100,000 elements).
        // Then value 5 gets 5 extra occurrences, replacing some of value 9999's,
        // so 5 is the unique most frequent and 9999 the unique least frequent.
        var list = new List<int>();
        for (int v = 0; v < 10_000; v++)
        {
            int count = v == 5 ? 15 : v == 9_999 ? 5 : 10;
            for (int c = 0; c < count; c++)
            {
                list.Add(v);
            }
        }
        int[] nums = list.ToArray();

        var sol = new P347.Solution();

        Assert.Equal(new[] { 5 }, sol.TopKFrequent(nums, 1));
    }

    [Fact]
    public void TopKFrequent_MaxSize_KEqualsUniqueCount()
    {
        // 20,001 unique values, each once: -10000..10000, k = all of them
        int[] nums = Enumerable.Range(-10_000, 20_001).ToArray();

        var sol = new P347.Solution();

        int[] actual = sol.TopKFrequent(nums, 20_001);

        Assert.Equal(nums.OrderBy(x => x), actual.OrderBy(x => x));
    }
}