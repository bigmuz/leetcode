namespace P271;

public class Solution
{
    public string Encode(IList<string> strs)
    {
        string encoded = "";

        foreach (var word in strs)
        {
            encoded += word.Length + "#" + word; // e.g. 5#Hello5#World
        }

        return encoded;
    }

    public List<string> Decode(string s)
    {
        List<string> decoded = [];
        int i = 0;

        while (i < s.Length)
        {
            int j = i;

            while (s[j] != '#')
            {
                j++;
            }

            int wordLength = int.Parse(s[i..j]); // digits before '#' (.. excludes end index)

            decoded.Add(s[(j + 1) .. (j + 1 + wordLength)]); // decoded.Add(s.Substring(j + 1, wordLength));

            i = j + wordLength + 1; // jump to start of next segment
        }

        return decoded;
    }
}
