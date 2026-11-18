namespace LeetCode.Tests;

public class P229_MajorityElementIITests
{
    [Theory]
    // Examples from the problem
    [InlineData(new[] { 3, 2, 3 }, new[] { 3 })]
    [InlineData(new[] { 1 }, new[] { 1 })]
    [InlineData(new[] { 1, 2 }, new[] { 1, 2 })]
    // Nothing qualifies
    [InlineData(new[] { 1, 2, 3 }, new int[] { })]
    [InlineData(new[] { 1, 2, 3, 4, 5, 6 }, new int[] { })]
    // Threshold boundary: needs strictly more than floor(n / 3)
    [InlineData(new[] { 1, 1, 2, 3 }, new[] { 1 })]               // n = 4, floor = 1, 1 appears twice
    [InlineData(new[] { 1, 2, 3, 4 }, new int[] { })]             // n = 4, each appears once, not > 1
    [InlineData(new[] { 1, 1, 2, 2, 3, 4 }, new int[] { })]       // n = 6, floor = 2, twice is not > 2
    [InlineData(new[] { 1, 1, 1, 2, 2, 2 }, new[] { 1, 2 })]      // n = 6, both appear 3 times
    [InlineData(new[] { 1, 1, 1, 2, 2, 3 }, new[] { 1 })]         // n = 6, 1 appears 3 times, 2 only twice
    // All identical
    [InlineData(new[] { 4, 4, 4 }, new[] { 4 })]
    [InlineData(new[] { 7, 7, 7, 7, 7 }, new[] { 7 })]
    // Two qualifying elements
    [InlineData(new[] { 1, 1, 1, 2, 2, 2, 3 }, new[] { 1, 2 })]
    [InlineData(new[] { 1, 2, 1, 2, 3 }, new[] { 1, 2 })]         // n = 5, floor = 1
    [InlineData(new[] { 3, 1, 2, 1, 2, 4, 1, 2 }, new[] { 1, 2 })] // n = 8, floor = 2, both appear 3 times
    // One qualifying element among many others
    [InlineData(new[] { 5, 1, 5, 2, 5, 3, 5, 4 }, new[] { 5 })]
    // Qualifying element appears only at the end
    [InlineData(new[] { 1, 2, 3, 4, 5, 5, 5, 5 }, new[] { 5 })]
    // Negatives and zero
    [InlineData(new[] { -1, -1, -1, 2 }, new[] { -1 })]
    [InlineData(new[] { 0, 0, 1 }, new[] { 0 })]
    [InlineData(new[] { -1, 0, -1, 0, 2 }, new[] { -1, 0 })]
    // Constraint extremes for values
    [InlineData(new[] { 1000000000, 1000000000, -1000000000 }, new[] { 1000000000 })]
    [InlineData(new[] { -1000000000, 1000000000 }, new[] { -1000000000, 1000000000 })]
    // Traps for a Boyer-Moore solution that skips the verification pass
    [InlineData(new[] { 1, 2, 3, 4, 1, 2, 5 }, new int[] { })]    // n = 7, floor = 2, twice is not > 2
    [InlineData(new[] { 1, 2, 3, 4, 5, 6, 7 }, new int[] { })]
    [InlineData(new[] { 2, 2, 1, 3 }, new[] { 2 })]
    public void MajorityElement_ReturnsExpected(int[] nums, int[] expected)
    {
        var sol = new P229.Solution();

        IList<int> actual = sol.MajorityElement(nums);

        // Any order is allowed, so compare sorted
        Assert.Equal(expected.OrderBy(x => x), actual.OrderBy(x => x));
    }

    [Fact]
    public void MajorityElement_ReturnsNoDuplicates()
    {
        int[] nums = [1, 1, 1, 1, 2, 2, 2, 2, 3];
        var sol = new P229.Solution();

        IList<int> actual = sol.MajorityElement(nums);

        Assert.Equal(actual.Count, actual.Distinct().Count());
    }

    [Fact]
    public void MajorityElement_MaxSize_AllSame()
    {
        int[] nums = Enumerable.Repeat(9, 50_000).ToArray();
        var sol = new P229.Solution();

        Assert.Equal(new[] { 9 }, sol.MajorityElement(nums));
    }

    [Fact]
    public void MajorityElement_MaxSize_TwoQualify()
    {
        // 20,000 of 1, 20,000 of 2, 10,000 distinct others: threshold is 16,666
        var list = new List<int>();
        list.AddRange(Enumerable.Repeat(1, 20_000));
        list.AddRange(Enumerable.Repeat(2, 20_000));
        list.AddRange(Enumerable.Range(100, 10_000));
        int[] nums = list.OrderBy(x => x % 7).ToArray();   // scramble the order
        var sol = new P229.Solution();

        Assert.Equal(new[] { 1, 2 }, sol.MajorityElement(nums).OrderBy(x => x));
    }

    [Fact]
    public void MajorityElement_MaxSize_NoneQualify()
    {
        int[] nums = Enumerable.Range(0, 50_000).ToArray();
        var sol = new P229.Solution();

        Assert.Empty(sol.MajorityElement(nums));
    }

    [Fact]
    public void MajorityElement_MaxSize_JustAboveThreshold()
    {
        // n = 50,000, floor(n / 3) = 16,666, so 16,667 occurrences qualify
        var list = new List<int>();
        list.AddRange(Enumerable.Repeat(8, 16_667));
        list.AddRange(Enumerable.Range(100, 33_333));
        int[] nums = list.OrderBy(x => x % 11).ToArray();
        var sol = new P229.Solution();

        Assert.Equal(new[] { 8 }, sol.MajorityElement(nums));
    }

    [Fact]
    public void MajorityElement_MaxSize_JustBelowThreshold()
    {
        // 16,666 occurrences is exactly floor(n / 3), which does not qualify
        var list = new List<int>();
        list.AddRange(Enumerable.Repeat(8, 16_666));
        list.AddRange(Enumerable.Range(100, 33_334));
        int[] nums = list.OrderBy(x => x % 11).ToArray();
        var sol = new P229.Solution();

        Assert.Empty(sol.MajorityElement(nums));
    }

    [Fact]
    public void MajorityElement_RandomArrays_MatchCountingReference()
    {
        var rng = new Random(12345);
        var sol = new P229.Solution();

        for (int round = 0; round < 500; round++)
        {
            int n = rng.Next(1, 40);
            int[] nums = Enumerable.Range(0, n).Select(_ => rng.Next(-4, 5)).ToArray();

            int[] expected = nums
                .GroupBy(x => x)
                .Where(g => g.Count() > n / 3)
                .Select(g => g.Key)
                .OrderBy(x => x)
                .ToArray();

            Assert.Equal(expected, sol.MajorityElement((int[])nums.Clone()).OrderBy(x => x));
        }
    }
}