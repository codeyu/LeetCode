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
            throw new Exception("input is not a valid List");
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

        public static void Sort(int[] arr)
        {
            SortMerge(arr, 0, arr.Length - 1);
        }
         static void MainMerge(int[] numbers, int left, int mid, int right)
        {
            int[] temp = new int[numbers.Length];
            int i, eol, num, pos;
 
            eol = (mid - 1);
            pos = left;
            num = (right - left + 1);
 
            while ((left <= eol) && (mid <= right))
            {
                if (numbers[left] <= numbers[mid])
                    temp[pos++] = numbers[left++];
                else
                    temp[pos++] = numbers[mid++];
            }
 
            while (left <= eol)
                temp[pos++] = numbers[left++];
 
            while (mid <= right)
                temp[pos++] = numbers[mid++];
 
            for (i = 0; i < num; i++)
            {
                numbers[right] = temp[right];
                right--;
            }
        }
 
        static void SortMerge(int[] numbers, int left, int right)
        {
            int mid;
 
            if (right > left)
            {
                mid = (right + left) / 2;
                SortMerge(numbers, left, mid);
                SortMerge(numbers, (mid + 1), right);
 
                MainMerge(numbers, left, (mid + 1), right);
            }
        }

        
        private static int Partition(int[] a, int low, int high)
        {
            int privotKey = a[low];
            while(low < high)
            {
                while(low < high && a[high] >= privotKey){ high--; }
                Swap(a[low], a[high]);
                while(low < high && a[low] <= privotKey){ low++; }
                Swap(a[low], a[high]);
            }
            return low;
        }
        static void Swap(int a, int b)
        {
            var tmp = a;
            a = b;
            b = tmp;
        }
    }
}