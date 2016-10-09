namespace Algorithms.Utils
{
    public class ListNode<T> 
    {
        public T Val {get;set;}
        public ListNode<T> Next{get;set;}
        public ListNode(T x) { Val = x; }
    }
}