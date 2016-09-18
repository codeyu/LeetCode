// Source : https://leetcode.com/problems/two-sum/
// Author : codeyu
// Date   : 2016-09-18

/********************************************************************************** 
* 
* Given an array of integers, return indices of the two numbers such that they add up to a specific target.
* 
* You may assume that each input would have exactly one solution.
* 
* Example:
* Given nums = [2, 7, 11, 15], target = 9,

* Because nums[0] + nums[1] = 2 + 7 = 9,
* return [0, 1].
* UPDATE (2016/2/13):
* The return format had been changed to zero-based indices. 
* Please read the above updated description carefully.
* 
*               
**********************************************************************************/
namespace Algorithms
{
    public class Solution001 
    {
        public static int[] TwoSum(int[] nums, int target) 
        {
            for(var i=0;i<nums.Length;i++){
                for(var j=nums.Length-1;j>i;j--){
                    if(i!=j){
                        if(nums[i]+nums[j]==target){
                            return new int[]{i,j};
                        }
                    }
                }
            }
            return new int[]{};
        }
    }
}
