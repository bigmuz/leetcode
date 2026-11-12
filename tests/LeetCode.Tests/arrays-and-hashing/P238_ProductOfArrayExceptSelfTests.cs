namespace LeetCode.Tests;

public class P238_ProductOfArrayExceptSelfTests
{
    [Theory]
    // Examples from the problem
    [InlineData(new[] { 1, 2, 3, 4 }, new[] { 24, 12, 8, 6 })]
    [InlineData(new[] { -1, 1, 0, -3, 3 }, new[] { 0, 0, 9, 0, 0 })]
    // Smallest input (n = 2)
    [InlineData(new[] { 2, 3 }, new[] { 3, 2 })]
    [InlineData(new[] { -2, 3 }, new[] { 3, -2 })]
    [InlineData(new[] { -1, -1 }, new[] { -1, -1 })]
    // All ones, all identical
    [InlineData(new[] { 1, 1, 1, 1 }, new[] { 1, 1, 1, 1 })]
    [InlineData(new[] { 2, 2, 2, 2 }, new[] { 8, 8, 8, 8 })]
    // Increasing values
    [InlineData(new[] { 1, 2, 3, 4, 5 }, new[] { 120, 60, 40, 30, 24 })]
    [InlineData(new[] { 5, 1, 1, 2 }, new[] { 2, 10, 10, 5 })]
    // Negatives
    [InlineData(new[] { -1, -2, -3 }, new[] { 6, 3, 2 })]
    [InlineData(new[] { -1, 1, -1, 1 }, new[] { -1, 1, -1, 1 })]
    // One zero (only the zero's own slot is non-zero)
    [InlineData(new[] { 0, 5 }, new[] { 5, 0 })]
    [InlineData(new[] { 5, 0 }, new[] { 0, 5 })]
    [InlineData(new[] { 1, 0, 3 }, new[] { 0, 3, 0 })]
    [InlineData(new[] { 0, 2, 3, 4 }, new[] { 24, 0, 0, 0 })]   // zero first
    [InlineData(new[] { 2, 3, 0, 4 }, new[] { 0, 0, 24, 0 })]   // zero in the middle
    // Two or more zeros (everything becomes 0)
    [InlineData(new[] { 0, 0 }, new[] { 0, 0 })]
    [InlineData(new[] { 0, 0, 3 }, new[] { 0, 0, 0 })]
    [InlineData(new[] { 0, 0, 0 }, new[] { 0, 0, 0 })]
    // Constraint extremes for values
    [InlineData(new[] { 30, 30 }, new[] { 30, 30 })]
    [InlineData(new[] { 30, -30, 1 }, new[] { -30, 30, -900 })]
    public void ProductExceptSelf_ReturnsExpected(int[] nums, int[] expected)
    {
        var sol = new P238.Solution();

        int[] actual = sol.ProductExceptSelf(nums);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ProductExceptSelf_DoesNotModifyInput()
    {
        int[] nums = [1, 2, 3, 4];
        var sol = new P238.Solution();

        sol.ProductExceptSelf(nums);

        Assert.Equal(new[] { 1, 2, 3, 4 }, nums);
    }

    [Fact]
    public void ProductExceptSelf_ReturnsSameLengthAsInput()
    {
        int[] nums = [3, 1, 4, 1, 5, 9, 2];
        var sol = new P238.Solution();

        Assert.Equal(nums.Length, sol.ProductExceptSelf(nums).Length);
    }

    [Fact]
    public void ProductExceptSelf_MaxSize_AllOnes()
    {
        int[] nums = Enumerable.Repeat(1, 100_000).ToArray();
        var sol = new P238.Solution();

        int[] actual = sol.ProductExceptSelf(nums);

        Assert.Equal(100_000, actual.Length);
        Assert.All(actual, x => Assert.Equal(1, x));
    }

    [Fact]
    public void ProductExceptSelf_MaxSize_TwoNonOneValues()
    {
        // 2 at the start, 3 at the end, ones elsewhere
        int[] nums = Enumerable.Repeat(1, 100_000).ToArray();
        nums[0] = 2;
        nums[^1] = 3;
        var sol = new P238.Solution();

        int[] actual = sol.ProductExceptSelf(nums);

        Assert.Equal(3, actual[0]);
        Assert.Equal(2, actual[^1]);
        Assert.All(actual[1..^1], x => Assert.Equal(6, x));
    }

    [Fact]
    public void ProductExceptSelf_MaxSize_SingleZero()
    {
        int[] nums = Enumerable.Repeat(1, 100_000).ToArray();
        nums[70_000] = 0;
        var sol = new P238.Solution();

        int[] actual = sol.ProductExceptSelf(nums);

        Assert.Equal(1, actual[70_000]);
        // every other slot excludes a one, so still includes the zero
        Assert.Equal(99_999, actual.Count(x => x == 0));
    }

    [Fact]
    public void ProductExceptSelf_MaxSize_TwoZeros()
    {
        int[] nums = Enumerable.Repeat(1, 100_000).ToArray();
        nums[10] = 0;
        nums[90_000] = 0;
        var sol = new P238.Solution();

        int[] actual = sol.ProductExceptSelf(nums);

        Assert.All(actual, x => Assert.Equal(0, x));
    }

    [Fact]
    public void ProductExceptSelf_RandomSmallArrays_MatchBruteForce()
    {
        // Values -3..3 and n <= 12 keep every product inside int range
        var rng = new Random(12345);
        var sol = new P238.Solution();

        for (int round = 0; round < 500; round++)
        {
            int n = rng.Next(2, 13);
            int[] nums = Enumerable.Range(0, n).Select(_ => rng.Next(-3, 4)).ToArray();

            int[] expected = new int[n];
            for (int i = 0; i < n; i++)
            {
                long product = 1;
                for (int j = 0; j < n; j++)
                {
                    if (j != i) product *= nums[j];
                }
                expected[i] = (int)product;
            }

            Assert.Equal(expected, sol.ProductExceptSelf((int[])nums.Clone()));
        }
    }
}