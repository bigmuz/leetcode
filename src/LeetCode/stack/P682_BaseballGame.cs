namespace P682;

public class Solution
{
    public int CalPoints(string[] operations)
    {
        Stack<int> record = [];

        foreach (var op in operations)
        {
            if (int.TryParse(op, out int newScore))
            {
                record.Push(newScore);
            }
            else if (op == "+")
            {
                int top = record.Pop(); // pop top to peek next number too
                int second = record.Peek();

                record.Push(top);
                record.Push(top + second);
            }
            else if (op == "D")
            {
                record.Push(record.Peek() * 2);
            }
            else if (op == "C")
            {
                record.Pop();
            }
        }

        return record.Sum();
    }
}