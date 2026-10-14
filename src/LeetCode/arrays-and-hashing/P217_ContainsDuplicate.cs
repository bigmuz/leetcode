namespace P217;

public class FirstSolution {
    public bool ContainsDuplicate(int[] nums) {
        List<int> numsList = [];

        foreach (var num in nums)
        {
            if (numsList.Contains(num))
            {
                return true;
            }

            numsList.Add(num);
        }

        return false;
    }
}

public class Solution {
    public bool ContainsDuplicate(int[] nums) {
        HashSet<int> numsHash = [];

        foreach (var num in nums)
        {
            if (!numsHash.Add(num)) // HashSet returns true if value was added, false if already there
            {
                return true;
            }
        }

        return false;
    }
}