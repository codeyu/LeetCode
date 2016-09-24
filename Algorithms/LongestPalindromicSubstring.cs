// Source : https://leetcode.com/problems/longest-palindromic-substring/
// Author : codeyu
// Date   : 2016-09-22

/*************************************************************************************** 
 *
 * Given a string S, find the longest palindromic substring in S. You may assume that 
 * the maximum length of S is 1000, and there exists one unique longest palindromic 
 * substring.
 ***************************************************************************************/
using System;
namespace Algorithms
{
    public class Solution005 
    {
        public static string LongestPalindromicWithSuffixTree(string s) 
        {
            throw new NotImplementedException("TODO");
        }

        public static string LongestPalindromicSubstring(string s) 
        {
            var n = s.Length;
            if(n == 0){ return ""; }
            var longest = s.Substring(0, 1);
            for(var i = 0; i < n - 1; i++)
            {
                var s1 = ExpandArouondCenter(s, i, i);
                if(s1.Length > longest.Length){ longest = s1; }
                var s2 = ExpandArouondCenter(s, i, i + 1);
                if(s2.Length > longest.Length) { longest = s2; }
            }
            return longest;
        }
        private static string ExpandArouondCenter(string s, int left, int right)
        {
            var L = left;
            var R = right;
            var N = s.Length;
            while(L >= 0 && R <= N - 1 && s[L] == s[R])
            {
                L--;
                R++;
            }
            return s.Substring(L + 1, R - L -1);
        }
    }
}
