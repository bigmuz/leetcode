namespace P20;

public class Solution
{
    public bool IsValid(string s)
    {
        Stack<char> stack = [];
        Dictionary<char, char> map = new Dictionary<char, char>()
        {
            {')', '('},
            {']', '['},
            {'}', '{'}
        };

        foreach (var bracket in s)
        {
            if (map.TryGetValue(bracket, out char openBracket))
            {
                if (stack.Count == 0 || stack.Peek() != openBracket)
                {
                    return false;
                }

                stack.Pop();
            }
            else
            {
                stack.Push(bracket);
            }
        }

        return stack.Count == 0;
    }
}