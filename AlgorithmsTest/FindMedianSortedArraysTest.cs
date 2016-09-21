using Algorithms;
using Xunit;
namespace AlgorithmsTest
{
    public class FindMedianSortedArraysTest
    {
        [Theory]
        [InlineData(new []{1,2},new[]{3,4},2.5)]
        [InlineData(new int[]{},new[]{1},1)]
        [InlineData(new[]{2,3,5,6,10,12},new[]{2,3,9},5)]
        [InlineData(new[]{2, 7, 11, 15},new[]{0,1},4.5)]
        public void Test_FindMedianSortedArrays(int[] input1,int[] input2, double output)
        {
            Assert.Equal(output,Solution004.FindMedianSortedArrays(input1,input2));
        }
    }
}