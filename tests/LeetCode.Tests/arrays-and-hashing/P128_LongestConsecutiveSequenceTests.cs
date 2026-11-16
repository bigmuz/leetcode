namespace LeetCode.Tests;

public class P128_LongestConsecutiveSequenceTests
{
    [Theory]
    // Examples from the problem
    [InlineData(new[] { 100, 4, 200, 1, 3, 2 }, 4)]
    [InlineData(new[] { 0, 3, 7, 2, 5, 8, 4, 6, 0, 1 }, 9)]
    [InlineData(new[] { 1, 0, 1, 2 }, 3)]
    // Empty and single element
    [InlineData(new int[] { }, 0)]
    [InlineData(new[] { 5 }, 1)]
    // Two elements
    [InlineData(new[] { 1, 2 }, 2)]
    [InlineData(new[] { 2, 1 }, 2)]
    [InlineData(new[] { 1, 3 }, 1)]
    [InlineData(new[] { 4, 4 }, 1)]
    // All identical
    [InlineData(new[] { 7, 7, 7, 7 }, 1)]
    // Already sorted, reverse sorted
    [InlineData(new[] { 1, 2, 3, 4, 5 }, 5)]
    [InlineData(new[] { 5, 4, 3, 2, 1 }, 5)]
    // No consecutive pairs
    [InlineData(new[] { 10, 20, 30, 40 }, 1)]
    // Gap in the middle
    [InlineData(new[] { 1, 2, 3, 10, 11 }, 3)]
    [InlineData(new[] { 1, 2, 10, 11, 12, 13 }, 4)]
    // Duplicates inside a sequence
    [InlineData(new[] { 1, 2, 2, 3, 3, 4 }, 4)]
    [InlineData(new[] { 1, 1, 1, 2, 2, 2, 3, 3, 3 }, 3)]
    // Sequence in the middle of unordered input
    [InlineData(new[] { 9, 1, 4, 7, 3, 2, 8, 5, 6 }, 9)]
    [InlineData(new[] { 50, 3, 2, 1, 99, 4 }, 4)]
    // Negatives and zero
    [InlineData(new[] { -1, -2, -3 }, 3)]
    [InlineData(new[] { -1, 0, 1 }, 3)]
    [InlineData(new[] { -5, -3, -4, 0, 10 }, 3)]
    [InlineData(new[] { 0, -1 }, 2)]
    // Two sequences of different lengths: longest wins, whichever comes first
    [InlineData(new[] { 1, 2, 3, 10, 11, 12, 13 }, 4)]
    [InlineData(new[] { 10, 11, 12, 13, 1, 2, 3 }, 4)]
    // Two sequences of the same length
    [InlineData(new[] { 1, 2, 3, 7, 8, 9 }, 3)]
    // Constraint extremes (watch for int overflow on +1 / -1)
    [InlineData(new[] { 1000000000, 999999999 }, 2)]
    [InlineData(new[] { -1000000000, -999999999 }, 2)]
    [InlineData(new[] { -1000000000, 1000000000 }, 1)]
    [InlineData(new[] { 1000000000 }, 1)]
    [InlineData(new[] { -1000000000 }, 1)]
    public void LongestConsecutive_ReturnsExpected(int[] nums, int expected)
    {
        var sol = new P128.Solution();

        Assert.Equal(expected, sol.LongestConsecutive(nums));
    }

    [Fact]
    public void LongestConsecutive_DoesNotRequireSortedInput_ButMayModify_ResultStillCorrect()
    {
        int[] nums = [3, 1, 2, 10, 4];
        var sol = new P128.Solution();

        Assert.Equal(4, sol.LongestConsecutive(nums));
    }

    [Fact]
    public void LongestConsecutive_MaxSize_OneLongSequence_Shuffled()
    {
        // 100,000 consecutive values in random order
        var rng = new Random(12345);
        int[] nums = Enumerable.Range(-50_000, 100_000).OrderBy(_ => rng.Next()).ToArray();
        var sol = new P128.Solution();

        Assert.Equal(100_000, sol.LongestConsecutive(nums));
    }

    [Fact]
    public void LongestConsecutive_MaxSize_OneLongSequence_Sorted()
    {
        // Worst case for a solution that restarts counting from every element (O(n^2))
        int[] nums = Enumerable.Range(0, 100_000).ToArray();
        var sol = new P128.Solution();

        Assert.Equal(100_000, sol.LongestConsecutive(nums));
    }

    [Fact]
    public void LongestConsecutive_MaxSize_OneLongSequence_Reversed()
    {
        int[] nums = Enumerable.Range(0, 100_000).Reverse().ToArray();
        var sol = new P128.Solution();

        Assert.Equal(100_000, sol.LongestConsecutive(nums));
    }

    [Fact]
    public void LongestConsecutive_MaxSize_AllDistinctNoneConsecutive()
    {
        int[] nums = Enumerable.Range(0, 100_000).Select(i => i * 3).ToArray();
        var sol = new P128.Solution();

        Assert.Equal(1, sol.LongestConsecutive(nums));
    }

    [Fact]
    public void LongestConsecutive_MaxSize_AllIdentical()
    {
        int[] nums = Enumerable.Repeat(42, 100_000).ToArray();
        var sol = new P128.Solution();

        Assert.Equal(1, sol.LongestConsecutive(nums));
    }

    [Fact]
    public void LongestConsecutive_MaxSize_ManyDuplicatesOfShortSequence()
    {
        // Values 0..9 repeated 10,000 times each: still a single run of 10
        int[] nums = Enumerable.Range(0, 100_000).Select(i => i % 10).ToArray();
        var sol = new P128.Solution();

        Assert.Equal(10, sol.LongestConsecutive(nums));
    }

    [Fact]
    public void LongestConsecutive_RandomArrays_MatchSortBasedReference()
    {
        var rng = new Random(2024);
        var sol = new P128.Solution();

        for (int round = 0; round < 300; round++)
        {
            int n = rng.Next(0, 60);
            int[] nums = Enumerable.Range(0, n).Select(_ => rng.Next(-30, 31)).ToArray();

            // Reference: sort, dedupe, count the longest run
            int[] distinct = nums.Distinct().OrderBy(x => x).ToArray();
            int expected = 0, run = 0;
            for (int i = 0; i < distinct.Length; i++)
            {
                run = i > 0 && distinct[i] == distinct[i - 1] + 1 ? run + 1 : 1;
                expected = Math.Max(expected, run);
            }

            Assert.Equal(expected, sol.LongestConsecutive((int[])nums.Clone()));
        }
    }
}