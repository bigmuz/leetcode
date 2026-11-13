namespace P36;

public class Solution {
    public bool IsValidSudoku(char[][] board) {
        Dictionary<int, HashSet<char>> rows = [];
        Dictionary<int, HashSet<char>> cols = [];
        Dictionary<(int, int), HashSet<char>> squares = []; // key = (row / 3, col / 3): int division maps cell to 3x3 square)

        // initialise dicts with empty vals
        for (int i = 0; i < 9; i++)
        {
            rows[i] = [];
            cols[i] = [];
        }

        for (int r = 0; r < 3; r++)
        {
            for (int c = 0; c < 3; c++)
            {
                squares[(r, c)] = [];
            }
        }

        for (int r = 0; r < 9; r++) // each row
        {
            for (int c = 0; c < 9; c++) // each col
            {
                char val = board[r][c];

                if (val == '.')
                {
                    continue;
                }

                // adding to HashSet returns bool (false if val already present)
                if (!rows[r].Add(val) || !cols[c].Add(val) || !squares[(r / 3, c / 3)].Add(val))
                {
                    return false;
                }
            }
        }

        return true;
    }
}