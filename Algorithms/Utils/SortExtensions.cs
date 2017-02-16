using System;
using System.Collections.Generic;
namespace Algorithms.Utils
{
    public static class SortExtensions
	{
		public static void Sort<T>(this IList<T> list)
        {
            if (list is List<T>)
            {
                ((List<T>)list).Sort();
            }
            else
            {
                List<T> copy = new List<T>(list);
                copy.Sort();
                Copy(copy, 0, list, 0, list.Count);
            }
        }

        public static void Sort<T>(this IList<T> list, Comparison<T> comparison)
        {
            if (list is List<T>)
            {
                ((List<T>)list).Sort(comparison);
            }
            else
            {
                List<T> copy = new List<T>(list);
                copy.Sort(comparison);
                Copy(copy, 0, list, 0, list.Count);
            }
        }

        public static void Sort<T>(this IList<T> list, IComparer<T> comparer)
        {
            if (list is List<T>)
            {
                ((List<T>)list).Sort(comparer);
            }
            else
            {
                List<T> copy = new List<T>(list);
                copy.Sort(comparer);
                Copy(copy, 0, list, 0, list.Count);
            }
        }

        public static void Sort<T>(this IList<T> list, int index, int count,
            IComparer<T> comparer)
        {
            if (list is List<T>)
            {
                ((List<T>)list).Sort(index, count, comparer);
            }
            else
            {
                List<T> range = new List<T>(count);
                for (int i = 0; i < count; i++)
                {
                    range.Add(list[index + i]);
                }
                range.Sort(comparer);
                Copy(range, 0, list, index, count);
            }
        }

        private static void Copy<T>(IList<T> sourceList, int sourceIndex,
            IList<T> destinationList, int destinationIndex, int count)
        {
            for (int i = 0; i < count; i++)
            {
                destinationList[destinationIndex + i] = sourceList[sourceIndex + i];
            }
        }
	}
}