namespace P49;

public class Solution {
    public IList<IList<string>> GroupAnagrams(string[] strs) {
        Dictionary<string, IList<string>> res = [];

        foreach (var word in strs)
        {
            int[] charCount = new int[26]; // count each letter

            foreach (var letter in word)
            {
                charCount[letter - 'a']++; // each char has a number, lowercase letters are numbered alphabetically, -a gives letter distance from a
            }

            string key = string.Join(",", charCount); // create key from letter counts (, to separate numbers)

            if (res.TryGetValue(key, out var value))
            {
                value.Add(word);
            }
            else
            {
                res.Add(key, [word]);
            }
        }

        return [.. res.Values];
    }
}

public class FirstSolution {
    public IList<IList<string>> GroupAnagrams(string[] strs) {
        Dictionary<string, IList<string>> sortedWords = [];

        foreach (var word in strs)
        {
            // create key by converting word into char array for sorting
            char[] wordSorted = word.ToCharArray();
            Array.Sort(wordSorted);
            string key = new string(wordSorted);

            if (sortedWords.TryGetValue(key, out var value)) // sortedWords.ContainsKey(key)
            {
                value.Add(word); // sortedWords[key].Add(word) — using TryGetValue() stops looking key up twice
            }
            else
            {
                sortedWords.Add(key, [word]); // new List<string> { word }
            }
        }

        return [..sortedWords.Values]; // sortedWords.Values.ToList();
    }
}