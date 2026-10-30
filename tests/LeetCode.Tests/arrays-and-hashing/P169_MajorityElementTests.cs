namespace LeetCode.Tests;

public class P169_MajorityElementTests
{
    [Theory]
    // Examples from the problem
    [InlineData(new[] { 3, 2, 3 }, 3)]
    [InlineData(new[] { 2, 2, 1, 1, 1, 2, 2 }, 2)]
    // Single element
    [InlineData(new[] { 1 }, 1)]
    // Two elements (majority needs more than floor(2/2) = 1 occurrence)
    [InlineData(new[] { 4, 4 }, 4)]
    // Smallest strict majority with a minority
    [InlineData(new[] { 1, 1, 2 }, 1)]
    [InlineData(new[] { 2, 1, 1 }, 1)]
    // All identical
    [InlineData(new[] { 7, 7, 7, 7 }, 7)]
    // Majority only just over half (n = 5 needs 3, n = 6 needs 4)
    [InlineData(new[] { 1, 2, 1, 2, 1 }, 1)]
    [InlineData(new[] { 1, 2, 1, 2, 1, 1 }, 1)]
    // Position of the majority
    [InlineData(new[] { 5, 5, 5, 1, 2 }, 5)]   // majority first
    [InlineData(new[] { 1, 2, 5, 5, 5 }, 5)]   // majority last
    [InlineData(new[] { 1, 5, 5, 5, 2 }, 5)]   // majority in the middle
    [InlineData(new[] { 5, 1, 5, 2, 5 }, 5)]   // interleaved
    // Majority appears late (trips up solutions that guess early)
    [InlineData(new[] { 1, 2, 3, 3, 3, 3, 3 }, 3)]
    [InlineData(new[] { 1, 1, 2, 2, 3, 3, 3, 3, 3 }, 3)]
    // Negatives and zero
    [InlineData(new[] { -1, -1, 2 }, -1)]
    [InlineData(new[] { 0, 0, 1 }, 0)]
    [InlineData(new[] { -1, 1, 1 }, 1)]
    // Constraint extremes
    [InlineData(new[] { -1000000000, -1000000000, 1000000000 }, -1000000000)]
    [InlineData(new[] { 1000000000, -1000000000, 1000000000 }, 1000000000)]
    public void MajorityElement_ReturnsExpected(int[] nums, int expected)
    {
        var sol = new P169.Solution();

        int actual = sol.MajorityElement(nums);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void MajorityElement_MaxSize_AllSame()
    {
        int[] nums = Enumerable.Repeat(9, 50_000).ToArray();

        var sol = new P169.Solution();

        Assert.Equal(9, sol.MajorityElement(nums));
    }

    [Fact]
    public void MajorityElement_MaxSize_MajorityJustOverHalf()
    {
        // 25,001 copies of 8 and 24,999 other values (n = 50,000 is even, so 25,001 > 25,000)
        var list = new List<int>();
        list.AddRange(Enumerable.Repeat(8, 25_001));
        list.AddRange(Enumerable.Range(100, 24_999));
        int[] nums = list.ToArray();

        var sol = new P169.Solution();

        Assert.Equal(8, sol.MajorityElement(nums));
    }

    [Fact]
    public void MajorityElement_MaxSize_MajorityAtEndAfterOtherValues()
    {
        // Other values first, majority value last
        var list = new List<int>();
        list.AddRange(Enumerable.Range(0, 24_999));
        list.AddRange(Enumerable.Repeat(-5, 25_001));
        int[] nums = list.ToArray();

        var sol = new P169.Solution();

        Assert.Equal(-5, sol.MajorityElement(nums));
    }

    [Fact]
    public void MajorityElement_MaxSize_AlternatingWithMajority()
    {
        // 30,000 copies of 1 interleaved with 20,000 copies of 2
        int[] nums = new int[50_000];
        for (int i = 0; i < nums.Length; i++)
        {
            nums[i] = i % 5 < 3 ? 1 : 2;
        }

        var sol = new P169.Solution();

        Assert.Equal(1, sol.MajorityElement(nums));
    }
}