using Algorithms;
using Xunit;
namespace AlgorithmsTest
{
    public class LengthOfLongestSubstringTest
    {
        [Theory]
        [InlineData("abcabcbb", 3)]
        [InlineData("bbbbb", 1)]
        [InlineData("pwwkew", 3)]
        public void Test_LengthOfLongestSubstring(string input, int output)
        {
            Assert.Equal(output,Solution003.LengthOfLongestSubstring(input));
        }
    }
}