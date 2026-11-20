namespace P41;

public class Solution
{
    public int FirstMissingPositive(int[] nums)
    {
        // make all negative numbers = 0
        for (int i = 0; i < nums.Length; i++)
        {
            if (nums[i] < 0)
            {
                nums[i] = 0;
            }
        }

        // mark each value as seen by making negative nums[value - 1] (the values position in arr)
        for (int i = 0; i < nums.Length; i++)
        {
            int value = Math.Abs(nums[i]); // strip any negative signs to get original value

            // solution must be between 1..n+1 so ignore others
            if (value >= 1 && value <= nums.Length)
            {
                if (nums[value - 1] > 0)
                {
                    nums[value - 1] *= -1;
                }
                else if (nums[value - 1] == 0)
                {
                    nums[value - 1] = -1 * (nums.Length + 1); // out of range marker (0 can't be made negative)
                }
            }
        }

        // if array position isn't negative we know that number doesn't exist in nums
        for (int i = 1; i < nums.Length + 1; i++)
        {
            if (nums[i - 1] >= 0)
            {
                return i;
            }
        }

        return nums.Length + 1; // 1..n must be present so solution is n + 1
    }
}