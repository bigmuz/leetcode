namespace P347;

public class Solution {
    public int[] TopKFrequent(int[] nums, int k) {
        // TO-DO: make time complexity better than O(n log n)
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