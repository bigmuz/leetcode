namespace LeetCode.Tests;

public class P20_ValidParenthesesTests
{
    [Theory]
    // Examples from the problem
    [InlineData("()", true)]
    [InlineData("()[]{}", true)]
    [InlineData("(]", false)]
    [InlineData("([])", true)]
    [InlineData("([)]", false)]
    // Single characters (always invalid)
    [InlineData("(", false)]
    [InlineData(")", false)]
    [InlineData("[", false)]
    [InlineData("]", false)]
    [InlineData("{", false)]
    [InlineData("}", false)]
    // Each pair type
    [InlineData("[]", true)]
    [InlineData("{}", true)]
    // Mismatched pairs
    [InlineData("[)", false)]
    [InlineData("{]", false)]
    [InlineData("(}", false)]
    // Wrong order (close before open)
    [InlineData(")(", false)]
    [InlineData("][", false)]
    [InlineData("}{", false)]
    // Unclosed openers left on the stack
    [InlineData("((", false)]
    [InlineData("(()", false)]
    [InlineData("([]", false)]
    [InlineData("{[(", false)]
    // Extra closers
    [InlineData("())", false)]
    [InlineData("()]", false)]
    [InlineData("(]]", false)]
    // Nesting
    [InlineData("((()))", true)]
    [InlineData("{[()]}", true)]
    [InlineData("{[(])}", false)]
    [InlineData("[({})]", true)]
    [InlineData("[({)}]", false)]
    // Sequences of valid groups
    [InlineData("()()()", true)]
    [InlineData("(){}[]", true)]
    [InlineData("({})[]", true)]
    [InlineData("(())[[]]{{}}", true)]
    // Nested plus sequential
    [InlineData("([]){}", true)]
    [InlineData("([]){]", false)]
    // Same-type nesting is fine, interleaving is not
    [InlineData("(())", true)]
    [InlineData("(()))", false)]
    [InlineData("((())", false)]
    // Odd length can never be valid
    [InlineData("()(", false)]
    [InlineData("(()", false)]
    public void IsValid_ReturnsExpected(string s, bool expected)
    {
        var sol = new P20.Solution();

        Assert.Equal(expected, sol.IsValid(s));
    }

    [Fact]
    public void IsValid_MaxSize_DeeplyNested_ReturnsTrue()
    {
        // 5,000 openers followed by their closers in reverse order
        string open = string.Concat(Enumerable.Range(0, 5_000).Select(i => "([{"[i % 3]));
        string close = string.Concat(open.Reverse().Select(c => c == '(' ? ')' : c == '[' ? ']' : '}'));

        Assert.True(new P20.Solution().IsValid(open + close));
    }

    [Fact]
    public void IsValid_MaxSize_AllOpeners_ReturnsFalse()
    {
        Assert.False(new P20.Solution().IsValid(new string('(', 10_000)));
    }

    [Fact]
    public void IsValid_MaxSize_AllClosers_ReturnsFalse()
    {
        Assert.False(new P20.Solution().IsValid(new string(')', 10_000)));
    }

    [Fact]
    public void IsValid_MaxSize_ManySimplePairs_ReturnsTrue()
    {
        Assert.True(new P20.Solution().IsValid(string.Concat(Enumerable.Repeat("()", 5_000))));
    }

    [Fact]
    public void IsValid_MaxSize_MismatchAtVeryEnd_ReturnsFalse()
    {
        // 4,999 valid pairs, then a mismatched pair
        string s = string.Concat(Enumerable.Repeat("()", 4_999)) + "(]";

        Assert.False(new P20.Solution().IsValid(s));
    }

    [Fact]
    public void IsValid_RandomStrings_MatchReference()
    {
        // Reference uses a different method: repeatedly delete adjacent matched pairs
        var rng = new Random(12345);
        var sol = new P20.Solution();
        const string chars = "()[]{}";

        for (int round = 0; round < 1000; round++)
        {
            int n = rng.Next(1, 17);
            string s = new string(Enumerable.Range(0, n).Select(_ => chars[rng.Next(chars.Length)]).ToArray());

            string reduced = s;
            string previous;
            do
            {
                previous = reduced;
                reduced = reduced.Replace("()", "").Replace("[]", "").Replace("{}", "");
            }
            while (reduced != previous);

            Assert.Equal(reduced.Length == 0, sol.IsValid(s));
        }
    }

    [Fact]
    public void IsValid_RandomValidStrings_ReturnTrue()
    {
        // Build valid strings by construction: insert matched pairs at random positions
        var rng = new Random(2024);
        var sol = new P20.Solution();
        string[] pairs = { "()", "[]", "{}" };

        for (int round = 0; round < 500; round++)
        {
            string s = "";
            int count = rng.Next(1, 10);
            for (int i = 0; i < count; i++)
            {
                string pair = pairs[rng.Next(3)];
                int pos = rng.Next(0, s.Length + 1);
                s = s.Insert(pos, pair);
            }

            Assert.True(sol.IsValid(s));
        }
    }
}