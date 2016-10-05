// Source : https://leetcode.com/problems/container-with-most-water/ 
// Author : codeyu 
// Date : 10/3/16 

/***************************************************************************************
*
* Given n non-negative integers a1, a2, ..., an, where each represents a point at coordinate (i, ai). 
* n vertical lines are drawn such that the two rightpoints of line i is at (i, ai) and (i, 0). 
* Find two lines, which together with x-axis forms a container, such that the container curArea the most water.
* 
* Note: You may not slant the container.
*
**********************************************************************************/

using System;
namespace Algorithms
{
    public class Solution011 
    {
        public static int MaxArea(int[] height)
        {
            int left = 0;
            int right = height.Length - 1;
            int maxArea = 0;

            while (left < right) 
            {
                int curArea = Math.Min(height[right], height[left]) * (right - left);
                maxArea = Math.Max(maxArea, curArea);

                if (height[left] < height[right]) 
                {
                    left ++;
                } 
                else 
                {
                    right --;
                }
            }
            return maxArea;
        }
    }
}

