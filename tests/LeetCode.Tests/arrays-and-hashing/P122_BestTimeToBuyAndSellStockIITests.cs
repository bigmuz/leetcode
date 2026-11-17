namespace LeetCode.Tests;

public class P122_BestTimeToBuyAndSellStockIITests
{
    [Theory]
    // Examples from the problem
    [InlineData(new[] { 7, 1, 5, 3, 6, 4 }, 7)]
    [InlineData(new[] { 1, 2, 3, 4, 5 }, 4)]
    [InlineData(new[] { 7, 6, 4, 3, 1 }, 0)]
    // Single day: nothing to trade
    [InlineData(new[] { 5 }, 0)]
    [InlineData(new[] { 0 }, 0)]
    // Two days
    [InlineData(new[] { 1, 5 }, 4)]
    [InlineData(new[] { 5, 1 }, 0)]
    [InlineData(new[] { 3, 3 }, 0)]
    // All identical
    [InlineData(new[] { 4, 4, 4, 4 }, 0)]
    // Strictly increasing and decreasing
    [InlineData(new[] { 1, 2, 3, 4, 5, 6 }, 5)]
    [InlineData(new[] { 6, 5, 4, 3, 2, 1 }, 0)]
    // Single peak and single valley
    [InlineData(new[] { 1, 5, 1 }, 4)]
    [InlineData(new[] { 5, 1, 5 }, 4)]
    // Multiple peaks: profits add up
    [InlineData(new[] { 1, 3, 1, 3, 1, 3 }, 6)]
    [InlineData(new[] { 3, 1, 3, 1, 3, 1 }, 4)]
    [InlineData(new[] { 2, 4, 1, 7 }, 8)]
    // Flat sections inside a rise
    [InlineData(new[] { 1, 2, 2, 3 }, 2)]
    [InlineData(new[] { 1, 1, 2, 2, 3, 3 }, 2)]
    // Zero prices
    [InlineData(new[] { 0, 0, 0 }, 0)]
    [InlineData(new[] { 0, 5, 0, 5 }, 10)]
    // Constraint extremes for values
    [InlineData(new[] { 0, 10000 }, 10000)]
    [InlineData(new[] { 10000, 0 }, 0)]
    [InlineData(new[] { 0, 10000, 0, 10000 }, 20000)]
    // Dip in the middle of a rise is worth trading around
    [InlineData(new[] { 1, 5, 3, 8 }, 9)]
    [InlineData(new[] { 6, 1, 3, 2, 4, 7 }, 7)]
    public void MaxProfit_ReturnsExpected(int[] prices, int expected)
    {
        var sol = new P122.Solution();

        Assert.Equal(expected, sol.MaxProfit(prices));
    }

    [Fact]
    public void MaxProfit_DoesNotRequireInputToStayUnmodified_ResultStillCorrect()
    {
        int[] prices = [7, 1, 5, 3, 6, 4];
        var sol = new P122.Solution();

        Assert.Equal(7, sol.MaxProfit(prices));
    }

    [Fact]
    public void MaxProfit_MaxSize_Increasing()
    {
        // 30,000 days, price rises by 1 each day up to 10,000 then stays flat
        int[] prices = Enumerable.Range(0, 30_000).Select(i => Math.Min(i, 10_000)).ToArray();
        var sol = new P122.Solution();

        Assert.Equal(10_000, sol.MaxProfit(prices));
    }

    [Fact]
    public void MaxProfit_MaxSize_Decreasing()
    {
        int[] prices = Enumerable.Range(0, 30_000).Select(i => Math.Max(10_000 - i, 0)).ToArray();
        var sol = new P122.Solution();

        Assert.Equal(0, sol.MaxProfit(prices));
    }

    [Fact]
    public void MaxProfit_MaxSize_AllIdentical()
    {
        int[] prices = Enumerable.Repeat(5000, 30_000).ToArray();
        var sol = new P122.Solution();

        Assert.Equal(0, sol.MaxProfit(prices));
    }

    [Fact]
    public void MaxProfit_MaxSize_Alternating()
    {
        // 0, 10000, 0, 10000, ...: 15,000 rises of 10,000 each
        int[] prices = Enumerable.Range(0, 30_000).Select(i => i % 2 == 0 ? 0 : 10_000).ToArray();
        var sol = new P122.Solution();

        Assert.Equal(150_000_000, sol.MaxProfit(prices));
    }

    [Fact]
    public void MaxProfit_RandomArrays_MatchBruteForceReference()
    {
        // Reference: the optimal total is the sum of every positive day-to-day rise.
        // A separate DP gives the same answer, so use that for an independent check.
        var rng = new Random(12345);
        var sol = new P122.Solution();

        for (int round = 0; round < 300; round++)
        {
            int n = rng.Next(1, 40);
            int[] prices = Enumerable.Range(0, n).Select(_ => rng.Next(0, 101)).ToArray();

            int hold = int.MinValue / 2, free = 0;
            foreach (int p in prices)
            {
                int newHold = Math.Max(hold, free - p);
                int newFree = Math.Max(free, hold + p);
                hold = newHold;
                free = newFree;
            }

            Assert.Equal(free, sol.MaxProfit((int[])prices.Clone()));
        }
    }
}