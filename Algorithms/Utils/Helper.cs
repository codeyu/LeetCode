using System;
using System.Collections.Generic;
using System.Linq;
namespace Algorithms.Utils
{
    public static class Helper
    {
        public static ListNode<int> ToListNode(this List<int> lst)
        {
            if(lst.Count>0 && lst.Where(x=>x>=10).Count()==0)
            {
                ListNode<int> head = new ListNode<int>(lst[0]);
                ListNode<int> current = head;
                for (var i=1;i<lst.Count;i++)
                {
                    current.Next = new ListNode<int>(lst[i]);
                    current = current.Next;
                }
                return head;
            }
            throw new Exception("input is not a valid array");
        }
        public static List<int> ToList(this ListNode<int> node)
        {
            var lst = new List<int>();
            while(node != null)
            {
                lst.Add(node.Val);
                node = node.Next;
            }
            return lst;
        }
    }
}