using Algorithms;
using Xunit;
namespace AlgorithmsTest
{
    public class AddTwoNumbersTest
    {
        [Theory]
        public void Test_AddTwoNumbers(ListNode l1, ListNode l2, ListNode output)
        {
            Assert.Equal(output,Solution002.AddTwoNumbers(l1, l2));
        }
    }
}