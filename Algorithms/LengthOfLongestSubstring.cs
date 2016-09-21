// Source : https://leetcode.com/problems/longest-substring-without-repeating-characters/
// Author : codeyu
// Date   : 2016-09-20

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
            var n = s.Length;
            var charSet = new HashSet<char>();
            int maxLength = 0, i = 0, j = 0;
            while (i < n && j < n) 
            {
                if (charSet.Add(s[j]))
                {
                    j++;
                    maxLength = Math.Max(maxLength, j - i);
                }
                else 
                {
                    charSet.Remove(s[i]);
                    i++;
                }
            }
            return maxLength;
        }
    }
}
