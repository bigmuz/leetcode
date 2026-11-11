namespace LeetCode.Tests;

public class P304_RangeSumQuery2DImmutableTests
{
    private static readonly int[][] Example =
    [
        [3, 0, 1, 4, 2],
        [5, 6, 3, 2, 1],
        [1, 2, 0, 1, 5],
        [4, 1, 0, 1, 7],
        [1, 0, 3, 0, 5]
    ];

    [Theory]
    // Examples from the problem
    [InlineData(2, 1, 4, 3, 8)]
    [InlineData(1, 1, 2, 2, 11)]
    [InlineData(1, 2, 2, 4, 12)]
    // Single cell
    [InlineData(0, 0, 0, 0, 3)]
    [InlineData(4, 4, 4, 4, 5)]
    [InlineData(2, 2, 2, 2, 0)]
    // Whole matrix
    [InlineData(0, 0, 4, 4, 58)]
    // Single row, single column
    [InlineData(0, 0, 0, 4, 10)]
    [InlineData(3, 1, 3, 3, 2)]
    [InlineData(0, 2, 4, 2, 7)]
    // Touching each edge and corner
    [InlineData(0, 0, 1, 1, 14)]     // top-left corner
    [InlineData(0, 3, 1, 4, 9)]      // top-right corner
    [InlineData(3, 0, 4, 1, 6)]      // bottom-left corner
    [InlineData(3, 3, 4, 4, 13)]     // bottom-right corner
    [InlineData(0, 1, 4, 3, 24)]     // full height, inner columns
    [InlineData(1, 0, 3, 4, 39)]     // full width, inner rows
    public void SumRegion_ReturnsExpected(int r1, int c1, int r2, int c2, int expected)
    {
        var m = new P304.NumMatrix(Example);

        Assert.Equal(expected, m.SumRegion(r1, c1, r2, c2));
    }

    [Fact]
    public void SumRegion_SameQueryTwice_GivesSameResult()
    {
        var m = new P304.NumMatrix(Example);

        Assert.Equal(m.SumRegion(2, 1, 4, 3), m.SumRegion(2, 1, 4, 3));
    }

    [Fact]
    public void SumRegion_DoesNotModifyInputMatrix()
    {
        int[][] input = [[1, 2], [3, 4]];
        var m = new P304.NumMatrix(input);

        m.SumRegion(0, 0, 1, 1);

        Assert.Equal(1, input[0][0]);
        Assert.Equal(2, input[0][1]);
        Assert.Equal(3, input[1][0]);
        Assert.Equal(4, input[1][1]);
    }

    [Fact]
    public void SingleCellMatrix()
    {
        var m = new P304.NumMatrix([[7]]);

        Assert.Equal(7, m.SumRegion(0, 0, 0, 0));
    }

    [Fact]
    public void SingleRowMatrix()
    {
        var m = new P304.NumMatrix([[1, 2, 3, 4]]);

        Assert.Equal(10, m.SumRegion(0, 0, 0, 3));
        Assert.Equal(5, m.SumRegion(0, 1, 0, 2));
        Assert.Equal(4, m.SumRegion(0, 3, 0, 3));
    }

    [Fact]
    public void SingleColumnMatrix()
    {
        var m = new P304.NumMatrix([[1], [2], [3], [4]]);

        Assert.Equal(10, m.SumRegion(0, 0, 3, 0));
        Assert.Equal(5, m.SumRegion(1, 0, 2, 0));
        Assert.Equal(4, m.SumRegion(3, 0, 3, 0));
    }

    [Fact]
    public void NegativeValues()
    {
        var m = new P304.NumMatrix([[-1, -2, 3], [4, -5, 6], [-7, 8, -9]]);

        Assert.Equal(-1, m.SumRegion(0, 0, 0, 0));
        Assert.Equal(-4, m.SumRegion(0, 0, 1, 1));   // -1 -2 +4 -5
        Assert.Equal(0, m.SumRegion(1, 1, 2, 2));    // -5 +6 +8 -9
        Assert.Equal(-3, m.SumRegion(0, 0, 2, 2));
    }

    [Fact]
    public void ConstraintExtremes_NoOverflow()
    {
        // 200 x 200 of 10,000: whole-matrix sum is 400,000,000 (fits in int)
        int[][] big = Enumerable.Range(0, 200)
            .Select(_ => Enumerable.Repeat(10_000, 200).ToArray())
            .ToArray();
        var m = new P304.NumMatrix(big);

        Assert.Equal(400_000_000, m.SumRegion(0, 0, 199, 199));
        Assert.Equal(10_000, m.SumRegion(100, 100, 100, 100));
        Assert.Equal(20_000 * 3, m.SumRegion(0, 0, 2, 1));
    }

    [Fact]
    public void ConstraintExtremes_AllMinimum()
    {
        int[][] big = Enumerable.Range(0, 200)
            .Select(_ => Enumerable.Repeat(-10_000, 200).ToArray())
            .ToArray();
        var m = new P304.NumMatrix(big);

        Assert.Equal(-400_000_000, m.SumRegion(0, 0, 199, 199));
    }

    [Fact]
    public void ManyQueries_MatchBruteForce()
    {
        var rng = new Random(12345);
        int rows = 200, cols = 200;
        int[][] matrix = Enumerable.Range(0, rows)
            .Select(_ => Enumerable.Range(0, cols).Select(__ => rng.Next(-10_000, 10_001)).ToArray())
            .ToArray();
        var m = new P304.NumMatrix(matrix);

        for (int q = 0; q < 10_000; q++)
        {
            int r1 = rng.Next(0, rows), r2 = rng.Next(r1, rows);
            int c1 = rng.Next(0, cols), c2 = rng.Next(c1, cols);

            long expected = 0;
            for (int r = r1; r <= r2; r++)
                for (int c = c1; c <= c2; c++)
                    expected += matrix[r][c];

            Assert.Equal(expected, m.SumRegion(r1, c1, r2, c2));
        }
    }
}