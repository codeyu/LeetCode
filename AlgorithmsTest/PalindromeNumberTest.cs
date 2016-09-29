using Algorithms;
using Xunit;
namespace AlgorithmsTest
{
    public class PalindromeNumberTest
    {
        [Theory]
        [InlineData(1, true)]
        public void TestMethod(int x, bool output)
        {
            Assert.Equal(output, Solution009.IsPalindrome(x));
        }
    }
}