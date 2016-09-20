// Source : https://leetcode.com/problems/add-two-numbers/
// Author : codeyu
// Date   : 2016-09-19

/********************************************************************************** 
* 
* You are given two linked lists representing two non-negative numbers. 
* The digits are stored in reverse order and each of their nodes contain a single digit. 
* Add the two numbers and return it as a linked list.
* 
* Input: (2 -> 4 -> 3) + (5 -> 6 -> 4)
* Output: 7 -> 0 -> 8
*               
**********************************************************************************/

namespace Algorithms
{
    public class Solution002 
    {
        public static ListNode<int> AddTwoNumbers(ListNode<int> l1, ListNode<int> l2)
        {
            ListNode<int> head = new ListNode<int>(0);
            ListNode<int> current = head;
            var carry = 0;
            while (l1 != null || l2 != null)
            {
                var x = l1 != null ? l1.Val : 0;
                var y = l2 != null ? l2.Val : 0;
                var digit = carry + x + y;
                carry = digit /10;
                current.Next = new ListNode<int>(digit%10);
                current = current.Next;
                if(l1 != null) { l1 = l1.Next;}
                if(l2 != null) { l2 = l2.Next;}
            }
            if(carry > 0){ current.Next = new ListNode<int>(carry); }
            return head.Next;
        }
         
    }

    public class ListNode<T> 
    {
        public T Val {get;set;}
        public ListNode<T> Next{get;set;}
        public ListNode(T x) { Val = x; }
    }
}