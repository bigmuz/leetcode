namespace LeetCode.Tests;

public class P225_ImplementStackUsingQueuesTests
{
    [Fact]
    public void Example_FromProblem()
    {
        var s = new P225.MyStack();

        s.Push(1);
        s.Push(2);

        Assert.Equal(2, s.Top());
        Assert.Equal(2, s.Pop());
        Assert.False(s.Empty());
    }

    [Fact]
    public void NewStack_IsEmpty()
    {
        Assert.True(new P225.MyStack().Empty());
    }

    [Fact]
    public void Push_MakesStackNonEmpty()
    {
        var s = new P225.MyStack();

        s.Push(5);

        Assert.False(s.Empty());
    }

    [Fact]
    public void SingleElement_PushTopPop()
    {
        var s = new P225.MyStack();

        s.Push(7);

        Assert.Equal(7, s.Top());
        Assert.Equal(7, s.Pop());
        Assert.True(s.Empty());
    }

    [Fact]
    public void Pop_ReturnsElementsInReverseOrder()
    {
        var s = new P225.MyStack();

        s.Push(1);
        s.Push(2);
        s.Push(3);

        Assert.Equal(3, s.Pop());
        Assert.Equal(2, s.Pop());
        Assert.Equal(1, s.Pop());
        Assert.True(s.Empty());
    }

    [Fact]
    public void Top_DoesNotRemoveElement()
    {
        var s = new P225.MyStack();

        s.Push(4);
        s.Push(9);

        Assert.Equal(9, s.Top());
        Assert.Equal(9, s.Top());
        Assert.Equal(9, s.Top());
        Assert.Equal(9, s.Pop());
        Assert.Equal(4, s.Top());
    }

    [Fact]
    public void Top_ChangesAfterPush()
    {
        var s = new P225.MyStack();

        s.Push(1);
        Assert.Equal(1, s.Top());

        s.Push(2);
        Assert.Equal(2, s.Top());

        s.Push(3);
        Assert.Equal(3, s.Top());
    }

    [Fact]
    public void Top_ChangesAfterPop()
    {
        var s = new P225.MyStack();

        s.Push(1);
        s.Push(2);
        s.Push(3);

        s.Pop();
        Assert.Equal(2, s.Top());

        s.Pop();
        Assert.Equal(1, s.Top());
    }

    [Fact]
    public void Empty_BecomesTrueOnlyAfterLastPop()
    {
        var s = new P225.MyStack();

        s.Push(1);
        s.Push(2);

        s.Pop();
        Assert.False(s.Empty());

        s.Pop();
        Assert.True(s.Empty());
    }

    [Fact]
    public void PushAfterEmptying_WorksAgain()
    {
        var s = new P225.MyStack();

        s.Push(1);
        s.Pop();
        Assert.True(s.Empty());

        s.Push(2);
        Assert.False(s.Empty());
        Assert.Equal(2, s.Top());
        Assert.Equal(2, s.Pop());
    }

    [Fact]
    public void Interleaved_PushAndPop_KeepsLifoOrder()
    {
        var s = new P225.MyStack();

        s.Push(1);
        s.Push(2);
        Assert.Equal(2, s.Pop());

        s.Push(3);
        s.Push(4);
        Assert.Equal(4, s.Pop());
        Assert.Equal(3, s.Pop());
        Assert.Equal(1, s.Pop());
        Assert.True(s.Empty());
    }

    [Fact]
    public void DuplicateValues_AreAllReturned()
    {
        var s = new P225.MyStack();

        s.Push(5);
        s.Push(5);
        s.Push(5);

        Assert.Equal(5, s.Pop());
        Assert.Equal(5, s.Pop());
        Assert.Equal(5, s.Pop());
        Assert.True(s.Empty());
    }

    [Theory]
    [InlineData(1)]
    [InlineData(9)]
    public void ConstraintExtremes_SingleValue(int x)
    {
        var s = new P225.MyStack();

        s.Push(x);

        Assert.Equal(x, s.Top());
        Assert.Equal(x, s.Pop());
        Assert.True(s.Empty());
    }

    [Fact]
    public void MaxCalls_PushManyThenPopAll()
    {
        // 50 pushes then 50 pops = 100 calls
        var s = new P225.MyStack();

        for (int i = 0; i < 50; i++)
        {
            s.Push(i % 9 + 1);
        }

        for (int i = 49; i >= 0; i--)
        {
            Assert.Equal(i % 9 + 1, s.Pop());
        }

        Assert.True(s.Empty());
    }

    [Fact]
    public void RandomOperations_MatchBuiltInStack()
    {
        // Compare against the built-in Stack. The built-in is only used in the test.
        var rng = new Random(12345);

        for (int round = 0; round < 200; round++)
        {
            var s = new P225.MyStack();
            var reference = new Stack<int>();

            for (int i = 0; i < 100; i++)
            {
                int op = rng.Next(4);

                if (op == 0 || reference.Count == 0)
                {
                    int x = rng.Next(1, 10);
                    s.Push(x);
                    reference.Push(x);
                }
                else if (op == 1)
                {
                    Assert.Equal(reference.Peek(), s.Top());
                }
                else if (op == 2)
                {
                    Assert.Equal(reference.Pop(), s.Pop());
                }
                else
                {
                    Assert.Equal(reference.Count == 0, s.Empty());
                }
            }
        }
    }
}