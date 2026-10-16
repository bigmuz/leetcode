namespace P1;

public class Solution {
    public int[] TwoSum(int[] nums, int target) {

        Dictionary<int, int> numsDict = [];

        for (int i = 0; i < nums.Length; i++)
        {
            if (numsDict.TryGetValue(target - nums[i], out int value))
            {
                return [value, i];
            }

            numsDict.TryAdd(nums[i], i); // TryAdd() adds only if missing, doesn't throw ex for dupe key
        }

        return [];
    }
}

public class FirstSolution {
    public int[] TwoSum(int[] nums, int target) {
        for (int i = 0; i < nums.Length; i++)
        {
            for (int j = i+1; j < nums.Length; j++)
            {
                if (nums[i] + nums[j] == target)
                {
                    int[] ans = [i, j];
                    return ans;
                } 
            }
        }

        return [];
    }
}