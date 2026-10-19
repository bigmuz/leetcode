namespace LeetCode.Tests;

public class P14_LongestCommonPrefixTests
{
    [Theory]
    // Examples from the problem
    [InlineData(new[] { "flower", "flow", "flight" }, "fl")]
    [InlineData(new[] { "dog", "racecar", "car" }, "")]
    // Single string: the whole string is the prefix
    [InlineData(new[] { "alone" }, "alone")]
    // Empty strings
    [InlineData(new[] { "" }, "")]
    [InlineData(new[] { "", "" }, "")]
    [InlineData(new[] { "abc", "" }, "")]
    [InlineData(new[] { "", "abc" }, "")]
    // All identical
    [InlineData(new[] { "same", "same", "same" }, "same")]
    // One string is the prefix of the others
    [InlineData(new[] { "ab", "abc", "abcd" }, "ab")]
    [InlineData(new[] { "abcd", "abc", "ab" }, "ab")]
    // Shortest string isn't first or last
    [InlineData(new[] { "abcde", "ab", "abcd" }, "ab")]
    // Prefix breaks at different positions
    [InlineData(new[] { "a", "b" }, "")]
    [InlineData(new[] { "abc", "abd", "abe" }, "ab")]
    [InlineData(new[] { "abc", "abc", "abd" }, "ab")]       // mismatch only in the last string
    [InlineData(new[] { "cir", "car" }, "c")]
    // Common letters that are not a prefix
    [InlineData(new[] { "abc", "xabc" }, "")]
    // Single-letter strings
    [InlineData(new[] { "a", "a", "a" }, "a")]
    public void LongestCommonPrefix_ReturnsExpected(string[] strs, string expected)
    {
        var sol = new P14.Solution();

        string actual = sol.LongestCommonPrefix(strs);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void LongestCommonPrefix_MaxSizeAllIdentical_ReturnsFullString()
    {
        // 200 strings of 200 characters each
        string word = new string('a', 200);
        string[] strs = Enumerable.Repeat(word, 200).ToArray();

        var sol = new P14.Solution();

        Assert.Equal(word, sol.LongestCommonPrefix(strs));
    }

    [Fact]
    public void LongestCommonPrefix_MaxSizeMismatchAtEnd_ReturnsAllButLastChar()
    {
        // Every string shares 199 characters, the last one differs in the final character
        string common = new string('a', 199);
        string[] strs = Enumerable.Repeat(common + "a", 199).Append(common + "b").ToArray();

        var sol = new P14.Solution();

        Assert.Equal(common, sol.LongestCommonPrefix(strs));
    }
}