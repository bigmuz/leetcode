namespace LeetCode.Tests;

public class P271_EncodeAndDecodeStringsTests
{
    private static List<string> RoundTrip(List<string> strs)
    {
        var sol = new P271.Solution();

        string encoded = sol.Encode(strs);
        return sol.Decode(encoded);
    }

    public static IEnumerable<object[]> Cases()
    {
        // Examples from the problem
        yield return new object[] { new List<string> { "Hello", "World" } };
        yield return new object[] { new List<string> { "" } };

        // Empty list (strs.length can be 0)
        yield return new object[] { new List<string>() };

        // Single string
        yield return new object[] { new List<string> { "a" } };

        // Empty strings mixed in
        yield return new object[] { new List<string> { "", "" } };
        yield return new object[] { new List<string> { "", "a", "" } };
        yield return new object[] { new List<string> { "a", "", "b" } };
        yield return new object[] { new List<string> { "", "", "", "" } };

        // Duplicates
        yield return new object[] { new List<string> { "same", "same", "same" } };

        // Order must be preserved
        yield return new object[] { new List<string> { "c", "b", "a" } };

        // Whitespace
        yield return new object[] { new List<string> { " " } };
        yield return new object[] { new List<string> { "  ", " a ", "\t", "\n" } };
        yield return new object[] { new List<string> { "hello world", "foo bar" } };

        // Strings containing common delimiters (traps for naive join/split)
        yield return new object[] { new List<string> { "a,b", "c" } };
        yield return new object[] { new List<string> { ",", ",,", "," } };
        yield return new object[] { new List<string> { "a#b", "#", "##" } };
        yield return new object[] { new List<string> { "a|b|c", "|" } };
        yield return new object[] { new List<string> { ":;", "::", ";;" } };
        yield return new object[] { new List<string> { "\\", "\\n", "\\\\" } };

        // Strings containing digits (traps for length-prefix schemes)
        yield return new object[] { new List<string> { "5", "12", "3#abc" } };
        yield return new object[] { new List<string> { "4#abcd", "2#ab" } };
        yield return new object[] { new List<string> { "10#", "0#", "#0" } };
        yield return new object[] { new List<string> { "123", "456", "789" } };

        // A string that looks like an encoded string
        yield return new object[] { new List<string> { "3#abc3#def" } };
        yield return new object[] { new List<string> { "1#a", "1#b" } };

        // Punctuation and symbols
        yield return new object[] { new List<string> { "!@#$%^&*()", "<>?\"'`~" } };

        // Mixed lengths
        yield return new object[] { new List<string> { "a", "bb", "ccc", "dddd", "eeeee" } };
    }

    [Theory]
    [MemberData(nameof(Cases))]
    public void EncodeDecode_RoundTrip_ReturnsOriginalList(List<string> strs)
    {
        List<string> actual = RoundTrip(strs);

        Assert.Equal(strs, actual);
    }

    [Fact]
    public void Encode_ReturnsString()
    {
        var sol = new P271.Solution();

        string encoded = sol.Encode(new List<string> { "Hello", "World" });

        Assert.NotNull(encoded);
    }

    [Fact]
    public void Decode_ReturnsNewListEqualToInput_NotNull()
    {
        var sol = new P271.Solution();

        List<string> decoded = sol.Decode(sol.Encode(new List<string>()));

        Assert.NotNull(decoded);
        Assert.Empty(decoded);
    }

    [Fact]
    public void EncodeDecode_EmptyListAndSingleEmptyString_AreDistinct()
    {
        // [] and [""] must not encode to the same thing
        var sol = new P271.Solution();

        List<string> emptyList = sol.Decode(sol.Encode(new List<string>()));
        List<string> oneEmpty = sol.Decode(sol.Encode(new List<string> { "" }));

        Assert.Empty(emptyList);
        Assert.Single(oneEmpty);
        Assert.Equal("", oneEmpty[0]);
    }

    [Fact]
    public void EncodeDecode_TwoEmptyStrings_AreDistinctFromOne()
    {
        var sol = new P271.Solution();

        List<string> one = sol.Decode(sol.Encode(new List<string> { "" }));
        List<string> two = sol.Decode(sol.Encode(new List<string> { "", "" }));

        Assert.Single(one);
        Assert.Equal(2, two.Count);
    }

    [Fact]
    public void Decode_IsIndependentOfEncoderInstance()
    {
        // Machine 1 and Machine 2 are different machines: the decoder can't rely on
        // state saved by the encoder, only on the encoded string.
        var strs = new List<string> { "Hello", "World", "", "a#b" };

        string encoded = new P271.Solution().Encode(strs);
        List<string> decoded = new P271.Solution().Decode(encoded);

        Assert.Equal(strs, decoded);
    }

    [Fact]
    public void Encode_DifferentLists_ProduceDifferentStrings()
    {
        var sol = new P271.Solution();

        string a = sol.Encode(new List<string> { "ab", "c" });
        string b = sol.Encode(new List<string> { "a", "bc" });
        string c = sol.Encode(new List<string> { "abc" });

        Assert.NotEqual(a, b);
        Assert.NotEqual(a, c);
        Assert.NotEqual(b, c);
    }

    [Fact]
    public void Encode_DoesNotModifyInput()
    {
        var sol = new P271.Solution();
        var strs = new List<string> { "x", "", "y#z" };
        var copy = new List<string>(strs);

        sol.Encode(strs);

        Assert.Equal(copy, strs);
    }

    [Fact]
    public void EncodeDecode_AllPrintableAscii_RoundTrips()
    {
        // One string holding every printable ASCII character
        string all = string.Concat(Enumerable.Range(32, 95).Select(c => (char)c));
        var strs = new List<string> { all, all };

        Assert.Equal(strs, RoundTrip(strs));
    }

    [Fact]
    public void EncodeDecode_All256Characters_RoundTrips()
    {
        // Constraint: any of the 256 ASCII characters (0-255), including control characters
        string all = string.Concat(Enumerable.Range(0, 256).Select(c => (char)c));
        var strs = new List<string> { all, "x", all };

        Assert.Equal(strs, RoundTrip(strs));
    }

    [Fact]
    public void EncodeDecode_EachSingleCharacter_RoundTrips()
    {
        // 256 strings, each one character, covering every character value
        var strs = Enumerable.Range(0, 256).Select(c => ((char)c).ToString()).ToList();

        Assert.Equal(strs, RoundTrip(strs));
    }

    [Fact]
    public void EncodeDecode_MaxSize_RoundTrips()
    {
        // 99 strings (length < 100), each 199 characters (length < 200)
        var rng = new Random(12345);
        var strs = Enumerable.Range(0, 99)
            .Select(_ => new string(Enumerable.Range(0, 199).Select(__ => (char)rng.Next(0, 256)).ToArray()))
            .ToList();

        Assert.Equal(strs, RoundTrip(strs));
    }

    [Fact]
    public void EncodeDecode_MaxSize_AllEmptyStrings()
    {
        var strs = Enumerable.Repeat("", 99).ToList();

        Assert.Equal(strs, RoundTrip(strs));
    }

    [Fact]
    public void EncodeDecode_RandomLists_RoundTrip()
    {
        var rng = new Random(2024);

        for (int round = 0; round < 200; round++)
        {
            int count = rng.Next(0, 100);
            var strs = new List<string>();
            for (int i = 0; i < count; i++)
            {
                int len = rng.Next(0, 200);
                strs.Add(new string(Enumerable.Range(0, len).Select(_ => (char)rng.Next(0, 256)).ToArray()));
            }

            Assert.Equal(strs, RoundTrip(strs));
        }
    }

    [Fact]
    public void EncodeDecode_DelimiterHeavyRandomLists_RoundTrip()
    {
        // Small alphabet made only of likely delimiters and digits, to stress naive schemes
        char[] alphabet = { '#', ',', '|', ':', ';', '0', '1', '2', '3', '9', '\\', '/' };
        var rng = new Random(777);

        for (int round = 0; round < 200; round++)
        {
            int count = rng.Next(0, 30);
            var strs = new List<string>();
            for (int i = 0; i < count; i++)
            {
                int len = rng.Next(0, 12);
                strs.Add(new string(Enumerable.Range(0, len).Select(_ => alphabet[rng.Next(alphabet.Length)]).ToArray()));
            }

            Assert.Equal(strs, RoundTrip(strs));
        }
    }

    // Follow-up: any possible characters (full Unicode). Beyond the 256-character constraint.
    [Theory(Skip = "Follow-up only: outside the 256 ASCII character constraint")]
    [InlineData("café", "日本語")]
    [InlineData("😀", "a😀b")]
    [InlineData("\u0000", "\uFFFF")]
    public void EncodeDecode_UnicodeFollowUp_RoundTrips(string first, string second)
    {
        var strs = new List<string> { first, second };

        Assert.Equal(strs, RoundTrip(strs));
    }
}