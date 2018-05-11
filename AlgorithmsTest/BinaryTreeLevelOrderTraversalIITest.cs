
using System;
using System.Collections.Generic;
using Algorithms;
using Algorithms.Utils;
using Xunit;
namespace AlgorithmsTest
{
    public class BinaryTreeLevelOrderTraversalIITest
    {
        [Theory]
        [InlineData()]
        public void TestMethod(TreeNode root,IList<IList<int>> output)
        {
            Assert.Equal(output, Solution107.LevelOrderBottom(root));
        }
    }
}