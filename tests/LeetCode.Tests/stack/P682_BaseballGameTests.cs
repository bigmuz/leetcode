namespace LeetCode.Tests;

public class P682_BaseballGameTests
{
    [Theory]
    // Examples from the problem
    [InlineData(new[] { "5", "2", "C", "D", "+" }, 30)]
    [InlineData(new[] { "5", "-2", "4", "C", "D", "9", "+", "+" }, 27)]
    [InlineData(new[] { "1", "C" }, 0)]
    // Single integer
    [InlineData(new[] { "5" }, 5)]
    [InlineData(new[] { "-5" }, -5)]
    [InlineData(new[] { "0" }, 0)]
    // D (double the previous score)
    [InlineData(new[] { "1", "D" }, 3)]            // [1, 2]
    [InlineData(new[] { "-1", "D" }, -3)]          // [-1, -2]
    [InlineData(new[] { "5", "D", "D" }, 35)]      // [5, 10, 20]
    [InlineData(new[] { "0", "D" }, 0)]            // [0, 0]
    // + (sum of the previous two)
    [InlineData(new[] { "2", "3", "+" }, 10)]               // [2, 3, 5]
    [InlineData(new[] { "-3", "-4", "+" }, -14)]            // [-3, -4, -7]
    [InlineData(new[] { "0", "0", "+" }, 0)]                // [0, 0, 0]
    [InlineData(new[] { "1", "1", "+", "+", "+" }, 12)]     // [1, 1, 2, 3, 5]
    // C (remove the previous score)
    [InlineData(new[] { "1", "2", "C", "C" }, 0)]           // []
    [InlineData(new[] { "1", "2", "3", "C", "C" }, 1)]      // [1]
    [InlineData(new[] { "1", "C", "2" }, 2)]                // [2]
    [InlineData(new[] { "3", "C", "4", "D" }, 12)]          // [4, 8]
    // Operations that depend on what C removed
    [InlineData(new[] { "1", "2", "3", "C", "+" }, 6)]      // [1, 2, 3]
    [InlineData(new[] { "1", "2", "D", "C", "+" }, 6)]      // [1, 2, 3]
    [InlineData(new[] { "1", "2", "+", "C" }, 3)]           // [1, 2]
    [InlineData(new[] { "1", "2", "+", "D" }, 12)]          // [1, 2, 3, 6]
    // Constraint extremes for values
    [InlineData(new[] { "30000" }, 30000)]
    [InlineData(new[] { "-30000" }, -30000)]
    [InlineData(new[] { "30000", "D" }, 90000)]             // [30000, 60000]
    [InlineData(new[] { "30000", "D", "+" }, 180000)]       // [30000, 60000, 90000]
    [InlineData(new[] { "-30000", "D", "+" }, -180000)]     // [-30000, -60000, -90000]
    public void CalPoints_ReturnsExpected(string[] operations, int expected)
    {
        var sol = new P682.Solution();

        Assert.Equal(expected, sol.CalPoints(operations));
    }

    [Fact]
    public void CalPoints_MaxSize_AllPositive()
    {
        string[] ops = Enumerable.Repeat("30000", 1000).ToArray();

        Assert.Equal(30_000_000, new P682.Solution().CalPoints(ops));
    }

    [Fact]
    public void CalPoints_MaxSize_AllNegative()
    {
        string[] ops = Enumerable.Repeat("-30000", 1000).ToArray();

        Assert.Equal(-30_000_000, new P682.Solution().CalPoints(ops));
    }

    [Fact]
    public void CalPoints_MaxSize_AddThenRemoveEverything()
    {
        // 500 scores then 500 C operations: record ends empty
        string[] ops = Enumerable.Repeat("7", 500)
            .Concat(Enumerable.Repeat("C", 500))
            .ToArray();

        Assert.Equal(0, new P682.Solution().CalPoints(ops));
    }

    [Fact]
    public void CalPoints_MaxSize_AddThenRemoveSome()
    {
        // 600 scores of 2, then 200 C operations: 400 twos remain
        string[] ops = Enumerable.Repeat("2", 600)
            .Concat(Enumerable.Repeat("C", 200))
            .ToArray();

        Assert.Equal(800, new P682.Solution().CalPoints(ops));
    }

    [Fact]
    public void CalPoints_DoesNotModifyInput()
    {
        string[] ops = ["5", "2", "C", "D", "+"];
        string[] copy = (string[])ops.Clone();

        new P682.Solution().CalPoints(ops);

        Assert.Equal(copy, ops);
    }

    [Fact]
    public void CalPoints_RandomValidOperations_MatchReference()
    {
        // Short sequences with small values keep every intermediate value inside int range.
        // Only valid operations are generated (+ needs 2 scores, D and C need 1).
        var rng = new Random(12345);
        var sol = new P682.Solution();

        for (int round = 0; round < 500; round++)
        {
            int n = rng.Next(1, 21);
            var ops = new List<string>();
            var record = new List<long>();

            for (int i = 0; i < n; i++)
            {
                int kind = rng.Next(4);

                if (kind == 1 && record.Count >= 2)
                {
                    ops.Add("+");
                    record.Add(record[^1] + record[^2]);
                }
                else if (kind == 2 && record.Count >= 1)
                {
                    ops.Add("D");
                    record.Add(record[^1] * 2);
                }
                else if (kind == 3 && record.Count >= 1)
                {
                    ops.Add("C");
                    record.RemoveAt(record.Count - 1);
                }
                else
                {
                    int x = rng.Next(-10, 11);
                    ops.Add(x.ToString());
                    record.Add(x);
                }
            }

            int expected = (int)record.Sum();

            Assert.Equal(expected, sol.CalPoints(ops.ToArray()));
        }
    }
}