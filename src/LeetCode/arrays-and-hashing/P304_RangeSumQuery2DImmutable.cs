namespace P304;

public class NumMatrix
{
    private readonly int[][] prefix; // prefix sum - precomputes running totals, each cell holds sum for (0,0) to itself

    public NumMatrix(int[][] matrix)
    {
        int rows = matrix.Length;
        int cols = matrix[0].Length;

        // init prefix - rows/cols + 1 for border of zeros
        prefix = new int[rows + 1][];
        for (int r = 0; r < prefix.Length; r++)
        {
            prefix[r] = new int[cols + 1];
        }

        // fill prefix
        for (int r = 0; r < rows; r++)
        {
            for (int c = 0; c < cols; c++)
            {
                // (r/c + 1) is current cell so r/c will be above/left cell of current (1 less index)
                prefix[r + 1][c + 1] = matrix[r][c] // matrix val
                                     + prefix[r][c + 1] // plus above cell
                                     + prefix[r + 1][c] // plus left cell
                                     - prefix[r][c]; // minus above left cell that was added twice
            }
        }
    }

    public int SumRegion(int row1, int col1, int row2, int col2)
    {
        return prefix[row2 + 1][col2 + 1] // rectangle from (0,0)
             - prefix[row1][col2 + 1] // minus above cell (the rows above desired rectangle)
             - prefix[row2 + 1][col1] // minus left cell (the columns left of desired rectangle)
             + prefix[row1][col1]; // plus above left cell that was removed twice
    }
}

/**
 * Your NumMatrix object will be instantiated and called as such:
 * NumMatrix obj = new NumMatrix(matrix);
 * int param_1 = obj.SumRegion(row1,col1,row2,col2);
 */