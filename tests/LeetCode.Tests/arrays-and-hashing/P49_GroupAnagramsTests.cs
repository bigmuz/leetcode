namespace LeetCode.Tests;

public class P49_GroupAnagramsTests
{
    public static IEnumerable<object[]> Cases()
    {
        // Examples from the problem
        yield return new object[]
        {
            new[] { "eat", "tea", "tan", "ate", "nat", "bat" },
            new[] { new[] { "bat" }, new[] { "nat", "tan" }, new[] { "ate", "eat", "tea" } }
        };
        yield return new object[] { new[] { "" }, new[] { new[] { "" } } };
        yield return new object[] { new[] { "a" }, new[] { new[] { "a" } } };

        // Multiple empty strings group together
        yield return new object[] { new[] { "", "" }, new[] { new[] { "", "" } } };
        yield return new object[] { new[] { "", "a", "" }, new[] { new[] { "", "" }, new[] { "a" } } };

        // No anagrams at all
        yield return new object[]
        {
            new[] { "abc", "def", "ghi" },
            new[] { new[] { "abc" }, new[] { "def" }, new[] { "ghi" } }
        };

        // Everything is one group
        yield return new object[]
        {
            new[] { "abc", "bca", "cab", "acb", "bac", "cba" },
            new[] { new[] { "abc", "bca", "cab", "acb", "bac", "cba" } }
        };

        // Identical strings
        yield return new object[] { new[] { "aa", "aa", "aa" }, new[] { new[] { "aa", "aa", "aa" } } };

        // Same letters, different counts must not group
        yield return new object[]
        {
            new[] { "aab", "abb", "aba", "bab" },
            new[] { new[] { "aab", "aba" }, new[] { "abb", "bab" } }
        };

        // Different lengths must not group
        yield return new object[]
        {
            new[] { "a", "aa", "aaa" },
            new[] { new[] { "a" }, new[] { "aa" }, new[] { "aaa" } }
        };

        // Boundary letters
        yield return new object[]
        {
            new[] { "az", "za", "zz", "aa" },
            new[] { new[] { "az", "za" }, new[] { "zz" }, new[] { "aa" } }
        };

        // Anagram groups interleaved in the input
        yield return new object[]
        {
            new[] { "listen", "google", "silent", "gogole", "enlist" },
            new[] { new[] { "listen", "silent", "enlist" }, new[] { "google", "gogole" } }
        };
    }

    [Theory]
    [MemberData(nameof(Cases))]
    public void GroupAnagrams_ReturnsExpectedGroups(string[] strs, string[][] expected)
    {
        var sol = new P49.Solution();

        var actual = sol.GroupAnagrams(strs);

        // Any order is allowed, for both the groups and the strings inside each group
        Assert.Equal(Canonical(expected), Canonical(actual));
    }

    [Fact]
    public void GroupAnagrams_MaxSizeSingleGroup_ReturnsOneGroup()
    {
        // 10,000 strings of length 100, each a rotation of the same letters
        string baseWord = string.Concat(Enumerable.Range(0, 100).Select(i => (char)('a' + i % 26)));
        string[] strs = Enumerable.Range(0, 10_000)
            .Select(i => baseWord[(i % 100)..] + baseWord[..(i % 100)])
            .ToArray();

        var sol = new P49.Solution();

        var actual = sol.GroupAnagrams(strs);

        Assert.Single(actual);
        Assert.Equal(10_000, actual[0].Count);
    }

    [Fact]
    public void GroupAnagrams_ManyDistinctGroups_ReturnsOneGroupPerLetterCount()
    {
        // Strings of i 'a's followed by j 'b's, for every i + j <= 100.
        // Each (i, j) is a different letter count, so each string is its own group.
        var strs = new List<string>();
        for (int i = 0; i <= 100; i++)
        {
            for (int j = 0; i + j <= 100; j++)
            {
                strs.Add(new string('a', i) + new string('b', j));
            }
        }

        var sol = new P49.Solution();

        var actual = sol.GroupAnagrams(strs.ToArray());

        Assert.Equal(strs.Count, actual.Count);
        Assert.All(actual, group => Assert.Single(group));
    }

    [Fact]
    public void GroupAnagrams_EveryInputStringAppearsExactlyOnce()
    {
        string[] strs = ["eat", "tea", "tan", "ate", "nat", "bat", "", "", "tab"];

        var sol = new P49.Solution();

        var actual = sol.GroupAnagrams(strs);

        var flattened = actual.SelectMany(g => g).OrderBy(s => s, StringComparer.Ordinal);
        Assert.Equal(strs.OrderBy(s => s, StringComparer.Ordinal), flattened);
    }

    // Sort inside each group, then sort the groups, so the order doesn't matter
    private static List<string> Canonical(IEnumerable<IEnumerable<string>> groups) =>
        groups
            .Select(g => string.Join(",", g.OrderBy(s => s, StringComparer.Ordinal)))
            .OrderBy(s => s, StringComparer.Ordinal)
            .ToList();
}