namespace LeetCode.Tests;

public class P232_ImplementQueueUsingStacksTests
{
    [Fact]
    public void Example_FromProblem()
    {
        var q = new P232.MyQueue();

        q.Push(1);
        q.Push(2);

        Assert.Equal(1, q.Peek());
        Assert.Equal(1, q.Pop());
        Assert.False(q.Empty());
    }

    [Fact]
    public void NewQueue_IsEmpty()
    {
        Assert.True(new P232.MyQueue().Empty());
    }

    [Fact]
    public void Push_MakesQueueNonEmpty()
    {
        var q = new P232.MyQueue();

        q.Push(5);

        Assert.False(q.Empty());
    }

    [Fact]
    public void SingleElement_PushPeekPop()
    {
        var q = new P232.MyQueue();

        q.Push(7);

        Assert.Equal(7, q.Peek());
        Assert.Equal(7, q.Pop());
        Assert.True(q.Empty());
    }

    [Fact]
    public void Pop_ReturnsElementsInInsertionOrder()
    {
        var q = new P232.MyQueue();

        q.Push(1);
        q.Push(2);
        q.Push(3);

        Assert.Equal(1, q.Pop());
        Assert.Equal(2, q.Pop());
        Assert.Equal(3, q.Pop());
        Assert.True(q.Empty());
    }

    [Fact]
    public void Peek_DoesNotRemoveElement()
    {
        var q = new P232.MyQueue();

        q.Push(4);
        q.Push(9);

        Assert.Equal(4, q.Peek());
        Assert.Equal(4, q.Peek());
        Assert.Equal(4, q.Peek());
        Assert.Equal(4, q.Pop());
        Assert.Equal(9, q.Peek());
    }

    [Fact]
    public void Peek_StaysAtFront_WhenMorePushed()
    {
        var q = new P232.MyQueue();

        q.Push(1);
        Assert.Equal(1, q.Peek());

        q.Push(2);
        Assert.Equal(1, q.Peek());

        q.Push(3);
        Assert.Equal(1, q.Peek());
    }

    [Fact]
    public void Peek_ChangesAfterPop()
    {
        var q = new P232.MyQueue();

        q.Push(1);
        q.Push(2);
        q.Push(3);

        q.Pop();
        Assert.Equal(2, q.Peek());

        q.Pop();
        Assert.Equal(3, q.Peek());
    }

    [Fact]
    public void Empty_BecomesTrueOnlyAfterLastPop()
    {
        var q = new P232.MyQueue();

        q.Push(1);
        q.Push(2);

        q.Pop();
        Assert.False(q.Empty());

        q.Pop();
        Assert.True(q.Empty());
    }

    [Fact]
    public void PushAfterEmptying_WorksAgain()
    {
        var q = new P232.MyQueue();

        q.Push(1);
        q.Pop();
        Assert.True(q.Empty());

        q.Push(2);
        Assert.False(q.Empty());
        Assert.Equal(2, q.Peek());
        Assert.Equal(2, q.Pop());
    }

    // Traps two-stack solutions that move items between stacks at the wrong time
    [Fact]
    public void Interleaved_PushAndPop_KeepsFifoOrder()
    {
        var q = new P232.MyQueue();

        q.Push(1);
        q.Push(2);
        Assert.Equal(1, q.Pop());

        q.Push(3);
        q.Push(4);
        Assert.Equal(2, q.Pop());
        Assert.Equal(3, q.Pop());
        Assert.Equal(4, q.Pop());
        Assert.True(q.Empty());
    }

    [Fact]
    public void Push_AfterPeek_DoesNotJumpTheLine()
    {
        var q = new P232.MyQueue();

        q.Push(1);
        q.Push(2);
        Assert.Equal(1, q.Peek());   // may move items to the second stack

        q.Push(3);                    // must go behind 2, not in front

        Assert.Equal(1, q.Pop());
        Assert.Equal(2, q.Pop());
        Assert.Equal(3, q.Pop());
    }

    [Fact]
    public void Push_AfterPartialPop_GoesToBack()
    {
        var q = new P232.MyQueue();

        q.Push(1);
        q.Push(2);
        q.Push(3);
        Assert.Equal(1, q.Pop());     // 2 and 3 may now sit in the second stack

        q.Push(4);

        Assert.Equal(2, q.Pop());
        Assert.Equal(3, q.Pop());
        Assert.Equal(4, q.Pop());
    }

    [Fact]
    public void DuplicateValues_AreAllReturned()
    {
        var q = new P232.MyQueue();

        q.Push(5);
        q.Push(5);
        q.Push(5);

        Assert.Equal(5, q.Pop());
        Assert.Equal(5, q.Pop());
        Assert.Equal(5, q.Pop());
        Assert.True(q.Empty());
    }

    [Theory]
    [InlineData(1)]
    [InlineData(9)]
    public void ConstraintExtremes_SingleValue(int x)
    {
        var q = new P232.MyQueue();

        q.Push(x);

        Assert.Equal(x, q.Peek());
        Assert.Equal(x, q.Pop());
        Assert.True(q.Empty());
    }

    [Fact]
    public void MaxCalls_PushManyThenPopAll()
    {
        // 50 pushes then 50 pops = 100 calls
        var q = new P232.MyQueue();

        for (int i = 0; i < 50; i++)
        {
            q.Push(i % 9 + 1);
        }

        for (int i = 0; i < 50; i++)
        {
            Assert.Equal(i % 9 + 1, q.Pop());
        }

        Assert.True(q.Empty());
    }

    [Fact]
    public void RandomOperations_MatchBuiltInQueue()
    {
        // Compare against the built-in Queue. The built-in is only used in the test.
        var rng = new Random(12345);

        for (int round = 0; round < 200; round++)
        {
            var q = new P232.MyQueue();
            var reference = new Queue<int>();

            for (int i = 0; i < 100; i++)
            {
                int op = rng.Next(4);

                if (op == 0 || reference.Count == 0)
                {
                    int x = rng.Next(1, 10);
                    q.Push(x);
                    reference.Enqueue(x);
                }
                else if (op == 1)
                {
                    Assert.Equal(reference.Peek(), q.Peek());
                }
                else if (op == 2)
                {
                    Assert.Equal(reference.Dequeue(), q.Pop());
                }
                else
                {
                    Assert.Equal(reference.Count == 0, q.Empty());
                }
            }
        }
    }
}