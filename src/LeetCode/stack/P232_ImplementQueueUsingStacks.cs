namespace P232;

// amortized O(1)
public class MyQueue
{
    private readonly Stack<int> stack1 = [];
    private readonly Stack<int> stack2 = [];

    public void Push(int x)
    {
        stack1.Push(x);
    }

    public int Pop()
    {
        TransferElementsIfNeeded();
        return stack2.Pop();
    }

    public int Peek()
    {
        TransferElementsIfNeeded();
        return stack2.Peek();
    }

    public bool Empty()
    {
        return stack1.Count + stack2.Count == 0;
    }

    private void TransferElementsIfNeeded()
    {
        // transfer elements to stack2, reversing order so the oldest is on top
        if (stack2.Count == 0)
        {
            while (stack1.Count > 0)
            {
                stack2.Push(stack1.Pop());
            }
        }
    }
}

public class FirstMyQueue
{
    private readonly Stack<int> stack1 = [];
    private readonly Stack<int> stack2 = [];

    public void Push(int x)
    {
        // transfer elements to stack2, reversing their order
        while (stack1.Count > 0)
        {
            stack2.Push(stack1.Pop());
        }

        stack1.Push(x); // push new element to bottom

        // transfer elements back, restoring original order above x
        while (stack2.Count > 0)
        {
            stack1.Push(stack2.Pop());
        }
    }

    public int Pop()
    {
        return stack1.Pop();
    }

    public int Peek()
    {
        return stack1.Peek();
    }

    public bool Empty()
    {
        return stack1.Count == 0;
    }
}

/**
 * Your MyQueue object will be instantiated and called as such:
 * MyQueue obj = new MyQueue();
 * obj.Push(x);
 * int param_2 = obj.Pop();
 * int param_3 = obj.Peek();
 * bool param_4 = obj.Empty();
 */