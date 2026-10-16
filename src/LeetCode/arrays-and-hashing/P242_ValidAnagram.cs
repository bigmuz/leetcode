namespace P242;

public class Solution {
    public bool IsAnagram(string s, string t) {

        // can quickly determine not anagram if lengths are different
        if (s.Length != t.Length)
        {
            return false;
        }

        Dictionary<char, int> lettersCount = [];

        foreach (var letter in s)
        {
            if (lettersCount.ContainsKey(letter))
            {
                lettersCount[letter] ++;
            }
            else
            {
                lettersCount.Add(letter, 1);
            }
        }

        foreach (var letter in t)
        {
            if (lettersCount.ContainsKey(letter))
            {
                lettersCount[letter] --;
            }
            else
            {
                lettersCount.Add(letter, -1);
            }
        }

        foreach (var pair in lettersCount)
        {
            if (pair.Value != 0)
            {
                return false;
            }
        }

        return true;
    }
}

public class SolutionExtra {
    public bool IsAnagram(string s, string t) {

        // can quickly determine not anagram if lengths are different
        if (s.Length != t.Length)
        {
            return false;
        }

        string sLowercase = s.ToLowerInvariant();
        string tLowercase = t.ToLowerInvariant();

        Dictionary<char, int> lettersCount = [];

        foreach (var letter in sLowercase)
        {
            if (lettersCount.ContainsKey(letter))
            {
                lettersCount[letter] ++;
            }
            else
            {
                lettersCount.Add(letter, 1);
            }
        }

        foreach (var letter in tLowercase)
        {
            if (lettersCount.ContainsKey(letter))
            {
                lettersCount[letter] --;
            }
            else
            {
                lettersCount.Add(letter, -1);
            }
        }

        foreach (var pair in lettersCount)
        {
            if (pair.Value != 0)
            {
                return false;
            }
        }

        return true;
    }
    
}