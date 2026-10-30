namespace P169;

public class Solution {
    public int MajorityElement(int[] nums)
    {
        // Boyer-Moore voting algorithm
        int candidate = 0;
        int count = 0;

        for (int i = 0; i < nums.Length; i++)
        {
            if (count == 0)
            {
                candidate = nums[i];
            }

            if (nums[i] == candidate)
            {
                count++;
            }
            else
            {
                count--;
            }
        }

        return candidate;
    }
}

public class FirstSolution {
    public int MajorityElement(int[] nums) 
    {
        Dictionary<int, int> counts = [];

        for (int i = 0; i < nums.Length; i++)
        {
            if (!counts.TryAdd(nums[i], 1))
            {
                counts[nums[i]]++;
            }
        }

        foreach (var pair in counts)
        {
            if (pair.Value > nums.Length / 2)
            {
                return pair.Key;
            }
        }

        return 0; // satisfy compiler
    }
}