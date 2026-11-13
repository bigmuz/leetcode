namespace LeetCode.Tests;

public class P36_ValidSudokuTests
{
    // Builds a board from 9 strings of 9 characters each ('.' = empty)
    private static char[][] Board(params string[] rows) =>
        rows.Select(r => r.ToCharArray()).ToArray();

    private static char[][] Empty() => Board(
        ".........", ".........", ".........",
        ".........", ".........", ".........",
        ".........", ".........", ".........");

    private static char[][] ValidBoard() => Board(
        "53..7....",
        "6..195...",
        ".98....6.",
        "8...6...3",
        "4..8.3..1",
        "7...2...6",
        ".6....28.",
        "...419..5",
        "....8..79");

    private static bool Run(char[][] board) => new P36.Solution().IsValidSudoku(board);

    [Fact]
    public void Example1_ValidBoard_ReturnsTrue()
    {
        Assert.True(Run(ValidBoard()));
    }

    [Fact]
    public void Example2_DuplicateInBox_ReturnsFalse()
    {
        var board = ValidBoard();
        board[0][0] = '8';   // two 8s in the top-left box (and in column 0)

        Assert.False(Run(board));
    }

    [Fact]
    public void EmptyBoard_ReturnsTrue()
    {
        Assert.True(Run(Empty()));
    }

    [Fact]
    public void SingleFilledCell_ReturnsTrue()
    {
        var board = Empty();
        board[4][4] = '5';

        Assert.True(Run(board));
    }

    [Fact]
    public void CompletedValidSolution_ReturnsTrue()
    {
        var board = Board(
            "534678912",
            "672195348",
            "198342567",
            "859761423",
            "426853791",
            "713924856",
            "961537284",
            "287419635",
            "345286179");

        Assert.True(Run(board));
    }

    // Row-only duplicates: different columns and different boxes, so only the row rule catches it
    [Theory]
    [InlineData(0, 0, 4)]   // columns 0 and 4, boxes 0 and 1
    [InlineData(4, 1, 7)]   // columns 1 and 7, boxes 3 and 5
    [InlineData(8, 2, 8)]   // last row
    [InlineData(0, 0, 8)]   // first and last column of a row
    public void DuplicateInRowOnly_ReturnsFalse(int row, int col1, int col2)
    {
        var board = Empty();
        board[row][col1] = '7';
        board[row][col2] = '7';

        Assert.False(Run(board));
    }

    // Column-only duplicates: different rows and different boxes, so only the column rule catches it
    [Theory]
    [InlineData(0, 0, 4)]   // rows 0 and 4, boxes 0 and 3
    [InlineData(4, 1, 7)]
    [InlineData(8, 2, 8)]   // last column
    [InlineData(0, 0, 8)]   // first and last row of a column
    public void DuplicateInColumnOnly_ReturnsFalse(int col, int row1, int row2)
    {
        var board = Empty();
        board[row1][col] = '3';
        board[row2][col] = '3';

        Assert.False(Run(board));
    }

    // Box-only duplicates: different rows and different columns, so only the box rule catches it
    [Theory]
    [InlineData(0, 0, 1, 1)]   // top-left box
    [InlineData(0, 2, 2, 0)]   // top-left box, anti-diagonal
    [InlineData(0, 3, 1, 5)]   // top-middle box
    [InlineData(1, 6, 2, 8)]   // top-right box
    [InlineData(3, 0, 5, 2)]   // middle-left box
    [InlineData(4, 4, 3, 3)]   // center box
    [InlineData(3, 6, 5, 8)]   // middle-right box
    [InlineData(6, 0, 8, 2)]   // bottom-left box
    [InlineData(7, 4, 6, 3)]   // bottom-middle box
    [InlineData(6, 6, 8, 8)]   // bottom-right box
    public void DuplicateInBoxOnly_ReturnsFalse(int r1, int c1, int r2, int c2)
    {
        var board = Empty();
        board[r1][c1] = '9';
        board[r2][c2] = '9';

        Assert.False(Run(board));
    }

    // Same digit in different rows, columns and boxes is fine
    [Fact]
    public void SameDigitInDifferentRowsColsAndBoxes_ReturnsTrue()
    {
        var board = Empty();
        board[0][0] = '1';
        board[1][3] = '1';
        board[2][6] = '1';
        board[3][1] = '1';
        board[4][4] = '1';
        board[5][7] = '1';
        board[6][2] = '1';
        board[7][5] = '1';
        board[8][8] = '1';

        Assert.True(Run(board));
    }

    // Same digit in the same box-row of boxes but different boxes is fine
    [Fact]
    public void SameDigitInNeighbouringBoxes_ReturnsTrue()
    {
        var board = Empty();
        board[0][2] = '5';   // box 0
        board[1][3] = '5';   // box 1 (different row and column)

        Assert.True(Run(board));
    }

    // Each digit 1-9 can trigger a duplicate
    [Theory]
    [InlineData('1')]
    [InlineData('2')]
    [InlineData('3')]
    [InlineData('4')]
    [InlineData('5')]
    [InlineData('6')]
    [InlineData('7')]
    [InlineData('8')]
    [InlineData('9')]
    public void EveryDigit_DuplicateInRow_ReturnsFalse(char digit)
    {
        var board = Empty();
        board[2][1] = digit;
        board[2][5] = digit;

        Assert.False(Run(board));
    }

    // Boxes that look invalid by row/column mixing but are valid
    [Fact]
    public void FullRowOfDistinctDigits_ReturnsTrue()
    {
        var board = Empty();
        for (int c = 0; c < 9; c++)
        {
            board[0][c] = (char)('1' + c);
        }

        Assert.True(Run(board));
    }

    [Fact]
    public void FullColumnOfDistinctDigits_ReturnsTrue()
    {
        var board = Empty();
        for (int r = 0; r < 9; r++)
        {
            board[r][0] = (char)('1' + r);
        }

        Assert.True(Run(board));
    }

    [Fact]
    public void FullBoxOfDistinctDigits_ReturnsTrue()
    {
        var board = Empty();
        for (int i = 0; i < 9; i++)
        {
            board[i / 3][i % 3] = (char)('1' + i);
        }

        Assert.True(Run(board));
    }

    // Valid by the rules but not solvable: only the three rules matter
    [Fact]
    public void ValidButUnsolvable_ReturnsTrue()
    {
        // Row 0 holds 1-8 in the first 8 cells, and column 8 already contains a 9
        // below it, so the last cell of row 0 has no legal digit. The board is
        // unsolvable but breaks no rule.
        var board = Empty();
        for (int c = 0; c < 8; c++)
        {
            board[0][c] = (char)('1' + c);
        }
        board[1][8] = '9';

        Assert.True(Run(board));
    }

    // A duplicate placed late in the scan (last cells checked)
    [Fact]
    public void DuplicateAtEndOfBoard_ReturnsFalse()
    {
        var board = ValidBoard();
        board[8][8] = '5';   // 5 already in column 8 (row 7) and in the bottom-right box

        Assert.False(Run(board));
    }

    // Duplicate involving the very first cell
    [Fact]
    public void DuplicateWithFirstCell_ReturnsFalse()
    {
        var board = ValidBoard();
        board[0][1] = '5';   // 5 at [0][0] and [0][1]

        Assert.False(Run(board));
    }

    [Fact]
    public void DoesNotModifyBoard()
    {
        var board = ValidBoard();
        var copy = board.Select(r => (char[])r.Clone()).ToArray();

        Run(board);

        for (int r = 0; r < 9; r++)
        {
            Assert.Equal(copy[r], board[r]);
        }
    }

    [Fact]
    public void RepeatedCalls_GiveSameResult()
    {
        var sol = new P36.Solution();
        var good = ValidBoard();
        var bad = ValidBoard();
        bad[0][0] = '8';

        Assert.True(sol.IsValidSudoku(good));
        Assert.False(sol.IsValidSudoku(bad));
        Assert.True(sol.IsValidSudoku(good));
    }

    [Fact]
    public void ManyRandomValidBoards_ReturnTrue()
    {
        // Shuffle a known valid solved grid with row/column swaps inside bands/stacks,
        // which always preserves validity, then blank out random cells.
        var rng = new Random(12345);
        string[] solved =
        {
            "534678912", "672195348", "198342567",
            "859761423", "426853791", "713924856",
            "961537284", "287419635", "345286179"
        };

        for (int round = 0; round < 100; round++)
        {
            var rows = Enumerable.Range(0, 9).ToArray();
            var cols = Enumerable.Range(0, 9).ToArray();
            for (int band = 0; band < 3; band++)
            {
                Shuffle(rows, band * 3, rng);
                Shuffle(cols, band * 3, rng);
            }

            var board = Empty();
            for (int r = 0; r < 9; r++)
            {
                for (int c = 0; c < 9; c++)
                {
                    if (rng.Next(2) == 0)
                    {
                        board[r][c] = solved[rows[r]][cols[c]];
                    }
                }
            }

            Assert.True(Run(board));
        }
    }

    private static void Shuffle(int[] arr, int start, Random rng)
    {
        for (int i = start + 2; i > start; i--)
        {
            int j = start + rng.Next(i - start + 1);
            (arr[i], arr[j]) = (arr[j], arr[i]);
        }
    }
}