namespace LeetCode.Tests;

public class P41_FirstMissingPositiveTests
{
    [Theory]
    // Examples from the problem
    [InlineData(new[] { 1, 2, 0 }, 3)]
    [InlineData(new[] { 3, 4, -1, 1 }, 2)]
    [InlineData(new[] { 7, 8, 9, 11, 12 }, 1)]
    // Single element
    [InlineData(new[] { 1 }, 2)]
    [InlineData(new[] { 2 }, 1)]
    [InlineData(new[] { 0 }, 1)]
    [InlineData(new[] { -1 }, 1)]
    [InlineData(new[] { int.MaxValue }, 1)]
    [InlineData(new[] { int.MinValue }, 1)]
    // Complete run starting at 1 (answer is n + 1)
    [InlineData(new[] { 1, 2, 3 }, 4)]
    [InlineData(new[] { 3, 2, 1 }, 4)]
    [InlineData(new[] { 2, 1 }, 3)]
    [InlineData(new[] { 4, 3, 2, 1 }, 5)]
    [InlineData(new[] { 5, 4, 3, 2, 1 }, 6)]
    [InlineData(new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }, 11)]
    [InlineData(new[] { 3, 4, 5, 6, 1, 2 }, 7)]
    [InlineData(new[] { 0, 1, 2 }, 3)]
    // Gap in the run
    [InlineData(new[] { 1, 3 }, 2)]
    [InlineData(new[] { 1, 2, 4 }, 3)]
    [InlineData(new[] { 2, 3, 1, 5 }, 4)]
    [InlineData(new[] { -1, 4, 2, 1, 9, 10 }, 3)]
    [InlineData(new[] { 1, 1000 }, 2)]
    // 1 is missing
    [InlineData(new[] { 2, 3, 4 }, 1)]
    [InlineData(new[] { 2, 2 }, 1)]
    // Only non-positive values
    [InlineData(new[] { 0, -1, -2 }, 1)]
    [InlineData(new[] { -5, -3, -1 }, 1)]
    // Duplicates (trap for in-place swap solutions that loop forever)
    [InlineData(new[] { 1, 1 }, 2)]
    [InlineData(new[] { 1, 1, 1, 1 }, 2)]
    [InlineData(new[] { 1, 2, 2, 3 }, 4)]
    [InlineData(new[] { 1, 1, 2, 2, 3, 3 }, 4)]
    [InlineData(new[] { 0, 2, 2, 1, 1 }, 3)]
    // Mixed negatives, zero and large values
    [InlineData(new[] { 1, 2, 0, -1, 5, 4 }, 3)]
    [InlineData(new[] { int.MaxValue, 1 }, 2)]
    [InlineData(new[] { int.MinValue, 1, 2 }, 3)]
    [InlineData(new[] { int.MaxValue, int.MinValue }, 1)]
    public void FirstMissingPositive_ReturnsExpected(int[] nums, int expected)
    {
        var sol = new P41.Solution();

        Assert.Equal(expected, sol.FirstMissingPositive(nums));
    }

    [Fact]
    public void MaxSize_PermutationOfOneToN_Shuffled_ReturnsNPlusOne()
    {
        var rng = new Random(12345);
        int[] nums = Enumerable.Range(1, 100_000).OrderBy(_ => rng.Next()).ToArray();

        Assert.Equal(100_001, new P41.Solution().FirstMissingPositive(nums));
    }

    [Fact]
    public void MaxSize_Sorted_ReturnsNPlusOne()
    {
        int[] nums = Enumerable.Range(1, 100_000).ToArray();

        Assert.Equal(100_001, new P41.Solution().FirstMissingPositive(nums));
    }

    [Fact]
    public void MaxSize_Reversed_ReturnsNPlusOne()
    {
        int[] nums = Enumerable.Range(1, 100_000).Reverse().ToArray();

        Assert.Equal(100_001, new P41.Solution().FirstMissingPositive(nums));
    }

    [Fact]
    public void MaxSize_OneValueRemovedFromRun()
    {
        // 1..100000 without 50000
        var rng = new Random(2024);
        int[] nums = Enumerable.Range(1, 100_000).Where(x => x != 50_000).OrderBy(_ => rng.Next()).ToArray();

        Assert.Equal(50_000, new P41.Solution().FirstMissingPositive(nums));
    }

    [Fact]
    public void MaxSize_RunStartsAtTwo_ReturnsOne()
    {
        int[] nums = Enumerable.Range(2, 100_000).ToArray();

        Assert.Equal(1, new P41.Solution().FirstMissingPositive(nums));
    }

    [Fact]
    public void MaxSize_AllHugeValues_ReturnsOne()
    {
        int[] nums = Enumerable.Repeat(int.MaxValue, 100_000).ToArray();

        Assert.Equal(1, new P41.Solution().FirstMissingPositive(nums));
    }

    [Fact]
    public void MaxSize_ManyDuplicatesOfOneToThousand_Returns1001()
    {
        // values cycle 1..1000, so all of 1..1000 are present and 1001 is not
        int[] nums = Enumerable.Range(0, 100_000).Select(i => i % 1000 + 1).ToArray();

        Assert.Equal(1001, new P41.Solution().FirstMissingPositive(nums));
    }

    [Fact]
    public void RandomArrays_MatchHashSetReference()
    {
        var rng = new Random(12345);
        var sol = new P41.Solution();

        for (int round = 0; round < 500; round++)
        {
            int n = rng.Next(1, 21);
            int[] nums = Enumerable.Range(0, n).Select(_ => rng.Next(-5, 16)).ToArray();

            var set = new HashSet<int>(nums);
            int expected = 1;
            while (set.Contains(expected))
            {
                expected++;
            }

            Assert.Equal(expected, sol.FirstMissingPositive((int[])nums.Clone()));
        }
    }
}