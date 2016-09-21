// Source : https://leetcode.com/problems/median-of-two-sorted-arrays/
// Author : codeyu
// Date   : 2016-09-21

/********************************************************************************** 
* 
* There are two sorted arrays nums1 and nums2 of size m and n respectively.
* 
* Find the median of the two sorted arrays.
* The overall run time complexity should be O(log (m+n)).
* 
* Example 1:
* nums1 = [1, 3]
* nums2 = [2]
* 
* The median is 2.0
* 
* Example 2:
* nums1 = [1, 2]
* nums2 = [3, 4]
* 
* The median is (2 + 3)/2 = 2.5
*               
**********************************************************************************/

namespace Algorithms
{
    public class Solution004
    {
        public static double FindMedianSortedArrays(int[] nums1, int[] nums2)
        {
            int[] arr = new int[nums1.Length + nums2.Length];
            int i = 0,j = 0, k = 0;
            while(i < nums1.Length && j < nums2.Length)
            {
                if(nums1[i] < nums2[j])
                {
                    arr[k] = nums1[i];
                    i++;
                }
                else
                {
                    arr[k] = nums2[j];
                    j++;
                }
                k++;
            }
            while(i<nums1.Length)
            {
                arr[k] = nums1[i];
                k++;
                i++;
            }
            while(j<nums2.Length)
            {
                arr[k] = nums2[j];
                k++;
                j++;
            }
            return GetArrayMedian(arr);
        }
        private static double GetArrayMedian(int[] arr)
        {
            int len = arr.Length;
            int mid = len/2;
            if(len%2 == 0)
            {
                return (double)(arr[mid]+arr[mid-1])/2;
            }
            else
            {
                return arr[mid];
            }
        }
    }
}
