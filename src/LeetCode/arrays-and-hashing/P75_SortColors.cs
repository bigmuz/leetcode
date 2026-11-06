namespace P75;

// dutch national flag - one pass solution
public class Solution {
    public void SortColors(int[] nums) {
        int lowPtr = 0;
        int index = 0;
        int highPtr = nums.Length - 1;

        while (index <= highPtr)
        {
            if (nums[index] == 0)
            {
                (nums[index], nums[lowPtr]) = (nums[lowPtr], nums[index]); // swap index vals
                lowPtr++;
                index++;
            }
            else if (nums[index] == 1)
            {
                index++;
                // 1 stays in middle so no need to swap vals or change high/low ptrs
            }
            else
            {
                (nums[index], nums[highPtr]) = (nums[highPtr], nums[index]); // swap index vals
                highPtr--; 
                // no index++ as new index val has not been sorted
            }
        }
    }
}

// counting sort
public class CountingSortSolution {
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