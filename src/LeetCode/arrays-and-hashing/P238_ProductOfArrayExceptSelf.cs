namespace P238;

public class Solution
{
    public int[] ProductExceptSelf(int[] nums)
    {
        int[] ans = new int[nums.Length];
        int left = 1;
        int right = 1;

        // pass one (left to right): each index holds product of everything to the left
        for (int i = 0; i < nums.Length; i++)
        {
            ans[i] = left; // product of everything before i
            left *= nums[i]; // include nums[i] for next index
        }

        // pass two (right to left): each index multiplies by product of everything to the right
        for (int i = nums.Length - 1; i >= 0; i--)
        {
            ans[i] *= right; // multiply product of everything after i
            right *= nums[i]; // include nums[i] for next index
        }

        return ans;
    }
}

public class FirstSolution
{
    public int[] ProductExceptSelf(int[] nums)
    {
        int[] prefix = new int[nums.Length + 1];
        int[] suffix = new int[nums.Length + 1];
        int[] ans = new int[nums.Length];

        // calculate prefix
        prefix[0] = 1;
        for (int i = 0; i < nums.Length; i++)
        {
            prefix[i + 1] = nums[i] * prefix[i];
        }

        // calculate suffix
        suffix[^1] = 1; // last element
        for (int i = nums.Length - 1; i >= 0; i--)
        {
            suffix[i] = nums[i] * suffix[i + 1];
        }

        // prefix * suffix
        for (int i = 0; i < nums.Length; i++)
        {
            ans[i] = prefix[i] * suffix[i + 1];
        }

        return ans;
    }
}