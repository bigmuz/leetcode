namespace P229;

// O(1) space: Boyer-Moore generalised to 2 candidates
public class Solution
{
    public IList<int> MajorityElement(int[] nums)
    {
        Dictionary<int, int> count = [];
        List<int> ans = [];

        foreach (var num in nums)
        {
            // add a vote
            if (!count.TryAdd(num, 1))
            {
                count[num]++;
            }

            // <= 2 candidates: do nothing
            if (count.Count <= 2)
            {
                continue;
            }

            // 3 candidates: remove 1 vote from each (can't all be > n / 3)
            Dictionary<int, int> newCount = [];

            foreach (var pair in count)
            {
                if (pair.Value > 1) // drop candidates whose count would hit 0
                {
                    newCount.Add(pair.Key, pair.Value - 1);
                }
            }

            count = newCount;
        }

        // verify remaining candidates have count > n / 3
        foreach (var pair in count)
        {
            if (nums.Count(x => x == pair.Key) > nums.Length / 3)
            {
                ans.Add(pair.Key);
            }
        }
        
        return ans;
    }
}

public class FirstSolution
{
    public IList<int> MajorityElement(int[] nums)
    {
        Dictionary<int, int> count = [];
        List<int> ans = [];

        foreach (var num in nums)
        {
            if (!count.TryAdd(num, 1))
            {
                count[num]++;
            }
        }

        foreach (var pair in count)
        {
            if (pair.Value > nums.Length / 3)
            {
                ans.Add(pair.Key);
            }
        }

        return ans;
    }
}