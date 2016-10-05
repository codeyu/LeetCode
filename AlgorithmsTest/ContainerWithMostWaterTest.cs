using Algorithms;
using Xunit;
namespace AlgorithmsTest
{
    public class ContainerWithMostWaterTest
    {
        [Theory]
        [InlineData(new []{3,6,10,40,2,36},72)]
        public void TestMethod(int[] height, int output)
        {
            Assert.Equal(output, Solution011.MaxArea(height));
        }
    }
}

