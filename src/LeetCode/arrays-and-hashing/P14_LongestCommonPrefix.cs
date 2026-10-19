namespace P14;

public class Solution {
    public string LongestCommonPrefix(string[] strs) {
        string ans = "";

        for (int i = 0; i < strs[0].Length; i++) // prefix can't be longer than any word so can arbitarily say strs[0].Length
        {
            foreach (var word in strs)
            {
                if (i == word.Length || strs[0][i] != word[i]) // word.Length is checked first to make sure not out of range
                {
                    return ans; // could return strs[0][..i];  prefix is the first i characters
                }
            }

            ans += strs[0][i];
        }
        
        return ans;
    }
}