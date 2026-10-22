namespace P347;

public class Solution {
    public int[] TopKFrequent(int[] nums, int k) {
        Dictionary<int, int> numberCount = [];
        List<int>[] buckets = new List<int>[nums.Length + 1]; // array using index as count & List<int> to hold numbers of that count
        List<int> ans = [];

        foreach (var num in nums)
        {
            if (numberCount.ContainsKey(num))
            {
                numberCount[num] ++;
            }
            else
            {
                numberCount.Add(num, 1);
            }
        }

        foreach (var num in numberCount)
        {
            // create empty list to avoid null ex
            if (buckets[num.Value] == null)
            {
                buckets[num.Value] = [];
            }

            buckets[num.Value].Add(num.Key);
        }

        for (int i = nums.Length; i >= 1; i--)
        {
            if (buckets[i] == null)
            {
                continue;
            }
            
            ans.AddRange(buckets[i]); // AddRange() adds every item from collection

            if (ans.Count >= k)
            {
                return [.. ans]; // ans.ToArray()
            }
        }

        return [.. ans];
    }
}

public class FirstSolution {
    public int[] TopKFrequent(int[] nums, int k) {
        Dictionary<int, int> numberCount = [];

        foreach (var num in nums)
        {
            if (numberCount.ContainsKey(num))
            {
                numberCount[num] ++;
            }
            else
            {
                numberCount.Add(num, 1);
            }
        }

        return [.. numberCount
            .OrderByDescending(pair => pair.Value) // sort pairs by value
            .Take(k) // only first k items
            .Select(pair => pair.Key)]; // returns each pair's key
    }
}