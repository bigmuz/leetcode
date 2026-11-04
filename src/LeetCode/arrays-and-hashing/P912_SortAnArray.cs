namespace P912;

// merge sort
public class Solution
{
    public int[] SortArray(int[] nums)
    {
        int[] temp = new int[nums.Length];
        MergeSort(nums, temp, 0, nums.Length - 1);
        return nums;
    }

    private void Merge(int[] nums, int[] temp, int left, int mid, int right)
    {
        int leftPointer = left; // left half pointer
        int rightPointer = mid + 1; // right half pointer
        int tempPointer = left; // where to write in temp[]

        // compare left & right sides until one side runs out
        while (leftPointer <= mid && rightPointer <= right)
        {
            if (nums[leftPointer] <= nums[rightPointer])
            {
                temp[tempPointer] = nums[leftPointer];
                leftPointer++;
            }
            else
            {
                temp[tempPointer] = nums[rightPointer];
                rightPointer++;
            }

            tempPointer++;
        }

        // add leftover nums
        while (leftPointer <= mid)
        {
            temp[tempPointer] = nums[leftPointer];
            leftPointer++;
            tempPointer++;
        }

        while (rightPointer <= right)
        {
            temp[tempPointer] = nums[rightPointer];
            rightPointer++;
            tempPointer++;
        }

        // copy temp[] back to nums[]
        for (int i = left; i <= right; i++)
        {
            nums[i] = temp[i];
        }
    }

    private void MergeSort(int[] nums, int[] temp, int left, int right)
    {
        // recursively splits until 1 or 0 elements
        if (left >= right)
        {
            return;
        }

        int mid = left + (right - left) / 2;

        MergeSort(nums, temp, left, mid); // sort left side
        MergeSort(nums, temp, mid + 1, right); // sort right side
        Merge(nums, temp, left, mid, right); // merge both sorted sides
    }
}