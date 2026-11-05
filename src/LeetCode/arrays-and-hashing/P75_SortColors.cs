namespace P75;

// counting sort
public class Solution {
    public void SortColors(int[] nums) {
        int[] buckets = new int[3]; // nums[i] is either 0, 1, or 2 (3 buckets)
        int numsPointer = 0;

        foreach (var num in nums)
        {
            buckets[num]++;
        }

        for (int i = 0; i < buckets.Length; i++)
        {
            for (int j = 0; j < buckets[i]; j++)
            {
                nums[numsPointer] = i;
                numsPointer++;
            }
        }
    }
}