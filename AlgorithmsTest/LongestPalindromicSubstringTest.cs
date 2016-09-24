using Algorithms;
using Xunit;
namespace AlgorithmsTest
{
    public class LongestPalindromicSubstringTest
    {
        [Theory]
        [InlineData("XMADAMYX", "MADAM")]
        [InlineData("bbba", "bbb")]
        [InlineData("googlelevel", "level")]
        public void Test_LongestPalindromicSubstring(string input, string output)
        {
            Assert.Equal(output,Solution005.LongestPalindromicSubstring(input));
        }
        [Theory]
        [InlineData("XMADAMYX", "MADAM")]
        [InlineData("bbba", "bbb")]
        [InlineData("googlelevel", "level")]
        public void Test_LongestPalindromicWithSuffixTree(string input, string output)
        {
            Assert.Equal(output,Solution005.LongestPalindromicWithSuffixTree(input));
        }
    }
}