using Algorithms;
using Xunit;
namespace AlgorithmsTest
{
    public class StringtoIntegerTest
    {
        [Theory]
        [InlineData("   -10522545459", -2147483648)]
        [InlineData("-2147483647", -2147483647)]
        [InlineData("234899987633333", 2147483647)]
        [InlineData("", 0)]
        [InlineData(" +123", 123)]
        public void Test_MyAtoi(string str, int output) 
        {
            Assert.Equal(output,Solution008.MyAtoi(str));
        }
    }
}