using System;
using System.Collections.Generic;
using System.Linq;
namespace Algorithms.Utils
{
    public static class Helper
    {
        public static ListNode<int> ToListNode(this List<int> lst)
        {
            if (lst.Count > 0 && lst.Where(x => x >= 10).Count() == 0)
            {
                ListNode<int> head = new ListNode<int>(lst[0]);
                ListNode<int> current = head;
                for (var i = 1; i < lst.Count; i++)
                {
                    current.Next = new ListNode<int>(lst[i]);
                    current = current.Next;
                }
                return head;
            }
            throw new Exception("input is not a valid List");
        }
        public static TreeNode ToTreeNode(this int?[] arr)
        {
            return ToTreeNode(arr, 0, arr.Length);
        }
        private static TreeNode ToTreeNode(int?[] arr, int i, int n)
        {
            if (i >= n || arr[i] == null) return null;
            TreeNode tn = new TreeNode(arr[i].Value);
            tn.Left = ToTreeNode(arr, 2 * i + 1, n);
            tn.Right = ToTreeNode(arr, 2 * i + 2, n);
            return tn;
        }
        public static List<int> ToList(this ListNode<int> node)
        {
            var lst = new List<int>();
            while (node != null)
            {
                lst.Add(node.Val);
                node = node.Next;
            }
            return lst;
        }
        public static void BubbleSort(int[] nums)
        {
            for (int i = 0; i < nums.Length - 1; i++)
            {
                for (int j = 0; j < nums.Length - 1 - i; j++)
                {
                    if (nums[j] > nums[j + 1])
                    {
                        int tmp = nums[j];
                        nums[j] = nums[j + 1];
                        nums[j + 1] = tmp;
                    }
                }
            }
        }
        public static void BubbleSort2(int[] nums)
        {
            int i = nums.Length - 1;
            while (i > 0)
            {
                int pos = 0;
                for (int j = 0; j < i; j++)
                {
                    if (nums[j] > nums[j + 1])
                    {
                        pos = j;//记录交换的位置
                        int tmp = nums[j];
                        nums[j] = nums[j + 1];
                        nums[j + 1] = tmp;
                    }
                }
                i = pos;
            }
        }
        public static void BubbleSort3(int[] nums)
        {
            int low = 0;
            int high = nums.Length - 1;
            int tmp, j;
            while (low < high)
            {
                for (j = low; j < high; j++)
                {
                    if (nums[j] > nums[j + 1])
                    {
                        tmp = nums[j];
                        nums[j] = nums[j + 1];
                        nums[j + 1] = tmp;
                    }
                }
                high--;
                for (j = high; j > low; j--)
                {
                    if (nums[j] < nums[j - 1])
                    {
                        tmp = nums[j];
                        nums[j] = nums[j - 1];
                        nums[j - 1] = tmp;
                    }
                }
                low++;
            }
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

        public static void QuickSort(int[] nums)
        {
            QuickSort(nums, 0, nums.Length - 1);
        }
        private static void QuickSort(int[] nums, int low, int high)
        {
            int privotLoc = Partition(nums, low, high);
            QuickSort(nums, low, privotLoc - 1);
            QuickSort(nums, privotLoc + 1, high);
        }
        private static int Partition(int[] a, int low, int high)
        {
            int privotKey = a[low];
            while (low < high)
            {
                while (low < high && a[high] >= privotKey) { high--; }
                Swap(a[low], a[high]);
                while (low < high && a[low] <= privotKey) { low++; }
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