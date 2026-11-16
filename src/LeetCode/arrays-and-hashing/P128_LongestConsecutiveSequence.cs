namespace P128;

public class Solution
{
    public int LongestConsecutive(int[] nums)
    {
        HashSet<int> numsSet = [..nums];
        int longestSequence = 0;

        foreach (var num in numsSet) // looping over set instead of array skips dupes
        {
            if (!numsSet.Contains(num - 1)) // num is start of a seq (no left neighbour)
            {
                int length = 1;

                while (numsSet.Contains(num + length)) // check next seq val exists
                {
                    length++;
                }

                longestSequence = Math.Max(longestSequence, length); // replace longest seq
            }
        }

        return longestSequence;
    }
}
