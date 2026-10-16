namespace LeetCode.Tests;

public class P242_ValidAnagramTests
{
    [Theory]
    // Examples from the problem
    [InlineData("anagram", "nagaram", true)]
    [InlineData("rat", "car", false)]
    // Examples from the earlier version of the problem
    [InlineData("racecar", "carrace", true)]
    [InlineData("jar", "jam", false)]
    // Smallest inputs (length 1)
    [InlineData("x", "x", true)]
    [InlineData("x", "y", false)]
    // Length mismatch (both directions)
    [InlineData("ab", "abc", false)]
    [InlineData("abc", "ab", false)]
    // Same letters, different counts
    [InlineData("aab", "abb", false)]
    [InlineData("aabb", "abbb", false)]
    // Repeated letters, different order
    [InlineData("aabb", "bbaa", true)]
    [InlineData("abab", "baba", true)]
    // Identical strings, and reversed strings
    [InlineData("hello", "hello", true)]
    [InlineData("abcdef", "fedcba", true)]
    // Same length, completely different letters
    [InlineData("abc", "xyz", false)]
    // Boundary letters of the alphabet
    [InlineData("az", "za", true)]
    [InlineData("az", "zz", false)]
    public void IsAnagram_ReturnsExpected(string s, string t, bool expected)
    {
        var sol = new P242.Solution();

        bool actual = sol.IsAnagram(s, t);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void IsAnagram_MaxSizeAnagram_ReturnsTrue()
    {
        // 50,000 characters each, same letters in different order
        string s = new string('a', 25_000) + new string('b', 25_000);
        string t = new string('b', 25_000) + new string('a', 25_000);

        var sol = new P242.Solution();

        Assert.True(sol.IsAnagram(s, t));
    }

    [Fact]
    public void IsAnagram_MaxSizeOneLetterDiffers_ReturnsFalse()
    {
        // Same length, differs by a single character at the very end
        string s = new string('a', 49_999) + "b";
        string t = new string('a', 50_000);

        var sol = new P242.Solution();

        Assert.False(sol.IsAnagram(s, t));
    }
}

// Extra tests: these go BEYOND LeetCode's constraints (lowercase a-z only).
// Remove the comment on Skip to turn them off so the main suite stays focused.
// A 26-slot int[] solution using `c - 'a'` will fail these;
// sorting and Dictionary<char, int> solutions should pass the Unicode ones.
// The case-insensitive ones also need case normalized (e.g. ToLowerInvariant()).
public class P242_ValidAnagramExtraTests
{
    [Theory] // (Skip = "Follow-up only: outside the lowercase-letters constraint")
    [InlineData("café", "éfac", true)]
    [InlineData("日本語", "語日本", true)]
    [InlineData("résumé", "resume", false)]
    public void IsAnagram_UnicodeFollowUp_ReturnsExpected(string s, string t, bool expected)
    {
        var sol = new P242.SolutionExtra();

        bool actual = sol.IsAnagram(s, t);

        Assert.Equal(expected, actual);
    }

    // Case does not matter: the same letters in any casing are anagrams.
    [Theory] // (Skip = "Follow-up only: outside the lowercase-letters constraint")
    [InlineData("Listen", "Silent", true)]    // same letters, same case
    [InlineData("Listen", "silent", true)]    // uppercase L vs lowercase l still matches
    [InlineData("AaBb", "abAB", true)]        // mixed case on both sides
    [InlineData("ABC", "cba", true)]          // all uppercase vs all lowercase
    [InlineData("Listen", "Silenz", false)]   // different letters, so still false
    [InlineData("Aab", "abb", false)]         // 'A' folds to 'a' (2 a's) vs 1 a, so counts differ
    public void IsAnagram_CaseInsensitive_ReturnsExpected(string s, string t, bool expected)
    {
        var sol = new P242.SolutionExtra();

        bool actual = sol.IsAnagram(s, t);

        Assert.Equal(expected, actual);
    }
}