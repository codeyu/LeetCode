using Algorithms.Utils;

namespace Algorithms
{
    public class ListNode : ListNode<int>
    {
        public int val;
        public ListNode next;
        public ListNode(int x) : base(x)
        { 
            val = x; 
        }
    }
}