namespace LeetCode.Tests;

public class P560_SubarraySumEqualsKTests
{
    [Theory]
    // Examples from the problem
    [InlineData(new[] { 1, 1, 1 }, 2, 2)]
    [InlineData(new[] { 1, 2, 3 }, 3, 2)]
    // Single element
    [InlineData(new[] { 1 }, 1, 1)]
    [InlineData(new[] { 1 }, 0, 0)]
    [InlineData(new[] { 1 }, 2, 0)]
    [InlineData(new[] { 0 }, 0, 1)]
    // Zeros: n(n+1)/2 subarrays all sum to 0
    [InlineData(new[] { 0, 0 }, 0, 3)]
    [InlineData(new[] { 0, 0, 0 }, 0, 6)]
    // Whole array, middle pair, no match
    [InlineData(new[] { 1, 2, 3 }, 6, 1)]
    [InlineData(new[] { 1, 2, 3 }, 5, 1)]
    [InlineData(new[] { 1, 2, 3 }, 7, 0)]
    // Repeated values
    [InlineData(new[] { 1, 1, 1, 1 }, 1, 4)]
    [InlineData(new[] { 1, 1, 1, 1 }, 2, 3)]
    [InlineData(new[] { 1, 1, 1, 1 }, 4, 1)]
    [InlineData(new[] { 2, 2, 2 }, 2, 3)]
    [InlineData(new[] { 1, 2, 1, 2, 1 }, 3, 4)]
    // Negatives (rules out sliding window)
    [InlineData(new[] { 1, -1, 0 }, 0, 3)]
    [InlineData(new[] { -1, -1, 1 }, 0, 1)]
    [InlineData(new[] { -1, -1, 1 }, -1, 3)]
    [InlineData(new[] { 3, 4, 7, 2, -3, 1, 4, 2 }, 7, 4)]
    // Constraint extremes
    [InlineData(new[] { 1000 }, 1000, 1)]
    [InlineData(new[] { -1000 }, -1000, 1)]
    [InlineData(new[] { 1000, -1000 }, 0, 1)]
    [InlineData(new[] { 1, 2, 3 }, 10000000, 0)]
    [InlineData(new[] { 1, 2, 3 }, -10000000, 0)]
    public void SubarraySum_ReturnsExpected(int[] nums, int k, int expected)
    {
        var sol = new P560.Solution();

        Assert.Equal(expected, sol.SubarraySum(nums, k));
    }

    [Fact]
    public void SubarraySum_MaxSize_AllOnes_KOne()
    {
        int[] nums = Enumerable.Repeat(1, 20_000).ToArray();

        Assert.Equal(20_000, new P560.Solution().SubarraySum(nums, 1));
    }

    [Fact]
    public void SubarraySum_MaxSize_AllOnes_KEqualsLength()
    {
        int[] nums = Enumerable.Repeat(1, 20_000).ToArray();

        Assert.Equal(1, new P560.Solution().SubarraySum(nums, 20_000));
    }

    [Fact]
    public void SubarraySum_MaxSize_AllZeros()
    {
        // n(n+1)/2 = 20,000 * 20,001 / 2 = 200,010,000 (fits in int)
        int[] nums = new int[20_000];

        Assert.Equal(200_010_000, new P560.Solution().SubarraySum(nums, 0));
    }

    [Fact]
    public void SubarraySum_MaxSize_AlternatingPlusMinusOne_KZero()
    {
        // Every even-length subarray sums to 0, odd-length ones sum to +-1
        int[] nums = Enumerable.Range(0, 20_000).Select(i => i % 2 == 0 ? 1 : -1).ToArray();

        Assert.Equal(100_000_000, new P560.Solution().SubarraySum(nums, 0));
    }

    [Fact]
    public void SubarraySum_RandomArrays_MatchBruteForce()
    {
        var rng = new Random(12345);
        var sol = new P560.Solution();

        for (int round = 0; round < 500; round++)
        {
            int n = rng.Next(1, 40);
            int[] nums = Enumerable.Range(0, n).Select(_ => rng.Next(-5, 6)).ToArray();
            int k = rng.Next(-10, 11);

            int expected = 0;
            for (int i = 0; i < n; i++)
            {
                int sum = 0;
                for (int j = i; j < n; j++)
                {
                    sum += nums[j];
                    if (sum == k) expected++;
                }
            }

            Assert.Equal(expected, sol.SubarraySum((int[])nums.Clone(), k));
        }
    }
}