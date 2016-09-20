// Source : https://leetcode.com/problems/two-sum/
// Author : codeyu
// Date   : 2016-09-18

/********************************************************************************** 
* 
* Given a string, find the length of the longest substring without repeating characters.
* 
* Examples:
* 
* Given "abcabcbb", the answer is "abc", which the length is 3.
* 
* Given "bbbbb", the answer is "b", with the length of 1.
* 
* Given "pwwkew", the answer is "wke", with the length of 3. 
* 
* Note that the answer must be a substring, "pwke" is a subsequence and not a substring.
*               
**********************************************************************************/
using System.Collections.Generic;
using System;
namespace Algorithms
{
    public class Solution003 
    {
        public static int LengthOfLongestSubstring(string s)
        {
            var start = 0;
            var tail = 0;
            var maxLength = 0;
            var charSet = new HashSet<char>();
            
            for(var i =0; i < s.Length; i++)
            {
                if(charSet.Add(s[i]))
                {
                    tail++;
                }
                else
                {
                    if(tail>maxLength)
                    {
                        maxLength = tail;        
                    }
                    tail = 0;
                    start++;
                    i = start - 1;
                    charSet.Clear();
                    continue;
                }
            }
            return maxLength;
        }
    }
}
