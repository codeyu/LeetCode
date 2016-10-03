using Algorithms;
using Xunit;
namespace AlgorithmsTest
{
    public class ContainerWithMostWaterTest
    {
        [Theory]
        [InlineData()]
        public void TestMethod(int[] height, int output)
        {
            Assert.Equal(output, Solution011.MaxArea(height));
        }
    }
}

