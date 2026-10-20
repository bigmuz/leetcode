namespace P49;

public class Solution {
    public IList<IList<string>> GroupAnagrams(string[] strs) {
        // TO-DO: counting solution
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