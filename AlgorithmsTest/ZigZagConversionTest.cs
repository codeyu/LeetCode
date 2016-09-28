using Algorithms;
using Xunit;
namespace AlgorithmsTest
{
    public class ZigZagConversionTest
    {
        [Theory]
        [InlineData("abcd",1,"abcd")]
        [InlineData("abcde",6,"abcde")]
        [InlineData("PAYPALISHIRING",3,"PAHNAPLSIIGYIR")]
        [InlineData("0123456789",4,"0615724839")]
        
        public void Test_Convert(string s, int numRows, string output) 
        {
            Assert.Equal(output, Solution006.Convert(s, numRows));
        }
    }
}