namespace P560;

// prefix sum
public class Solution
{
    public int SubarraySum(int[] nums, int k)
    {
        int count = 0;
        int sum = 0;
        Dictionary<int, int> prefixSums = [];

        prefixSums.Add(0, 1); // empty prefix: lets subarrays starting index 0 count

        for (int i = 0; i < nums.Length; i++)
        {
            sum += nums[i];
            int diff = sum - k;

            prefixSums.TryGetValue(diff, out int prefixSumCount); // return number of prefixes that are the difference between current sum and k
            count += prefixSumCount;

            // add current sum to prefix
            if (!prefixSums.TryAdd(sum, 1))
            {
                prefixSums[sum]++;
            }
        }
        
        return count;
    }
}

// brute force
public class FirstSolution
{
    public int SubarraySum(int[] nums, int k)
    {
        int count = 0;

        for (int i = 0; i < nums.Length; i++)
        {
            int sum = 0;

            for (int j = i; j < nums.Length; j++)
            {
                sum += nums[j];

                if (sum == k)
                {
                    count++;
                }
            }
        }

        return count;
    }
}