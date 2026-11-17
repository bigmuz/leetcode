namespace P122;

public class Solution
{
    public int MaxProfit(int[] prices)
    {
        int profit = 0;

        for (int i = 1; i < prices.Length; i++)
        {
            if (prices[i] > prices[i - 1]) // check stock price has gone up
            {
                profit += prices[i] - prices[i - 1]; // add difference to profit
            }
        }

        return profit;
    }
}