
using Algorithms;
using Xunit;
namespace AlgorithmsTest
{
    public class ReverseIntegerTest
    {
        [Theory]
        [InlineData(2147483647, 0)]
        [InlineData(123, 321)]
        [InlineData(100, 1)]
        public void Test_Reverse(int x, int output) 
        {
            Assert.Equal(output,Solution007.Reverse(x));
        }
    }
}