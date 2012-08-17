using System;

namespace SharpStructures.Sorting
{
    public static class SortHelper
    {
        public static void Swap<T>(T[] array, int i, int j)
        {
            T t = array[i];
            array[i] = array[j];
            array[j] = t;
        }

        public static void SelectionSort<T>(T[] array) where T : IComparable<T>
        {
            int n = array.Length;
            for (int i = 0; i < n - 1; i++)
            {
                int minPos = i;
                for (int j = 0; j < n; j++)
                    if (array[j].CompareTo(array[minPos]) < 0)
                        minPos = j;
                Swap(array, minPos, i);
            }
        }

        public static void InsertionSort<T>(T[] array) where T : IComparable<T>
        {
            int n = array.Length;
            for (int i = 1; i < n; i++)
                for (int j = i - 1; j >= 0 && array[j + 1].CompareTo(array[j]) < 0; j--)
                    Swap(array, j + 1, j);
        }

        public static void ShellSort<T>(T[] array) where T : IComparable<T>
        {
            int n = array.Length;
            int h = 1;
            while (h < n/3)
                h = 3*h + 1;
            while (h > 1)
            {
                for (int i = h; i < n; i++)
                    for (int j = i - h; j >= 0 && array[j + h].CompareTo(array[j]) < 0; j--)
                        Swap(array, j + h, j);
                h /= 3;
            }
        }

        public static void MergeSort<T>(T[] array) where T : IComparable<T>
        {
            int n = array.Length;
            var aux = new T[n];
            MergeSort(array, 0, n - 1, aux);
        }

        private static void MergeSort<T>(T[] array, int lo, int hi, T[] aux)
            where T : IComparable<T>
        {
            if (lo >= hi) return;
            int mid = lo + (hi - lo)/2;
            MergeSort(array, lo, mid, aux);
            MergeSort(array, mid + 1, hi, aux);
            Merge(array, lo, mid, hi, aux);
        }

        private static void Merge<T>(T[] array, int lo, int mid, int hi, T[] aux)
            where T : IComparable<T>
        {
            for (int it = lo; it <= hi; it++)
                aux[it] = array[it];

            int i = lo, j = mid + 1;
            int k = lo;
            while (k <= hi)
            {
                if (i > mid) array[k++] = aux[j++];
                else if (j > hi) array[k++] = aux[i++];
                else if (aux[i].CompareTo(aux[j]) <= 0) array[k++] = aux[i++];
                else array[k++] = aux[j++];
            }
        }

        public static void QuickSort<T>(T[] array)
            where T : IComparable<T>
        {
            Shuffle(array);
            QuickSort(array, 0, array.Length - 1);
        }

        public static void CountSort(int[] array, int R)
        {
            var count = new int[R];
            for (int i = 0; i < array.Length; i++)
                count[array[i]]++;
            int k = 0;
            for (int i = 0; i < R; i++)
                for (int j = 0; j < count[i]; j++)
                    array[k++] = i;
        }

        private static void QuickSort<T>(T[] array, int lo, int hi)
            where T : IComparable<T>
        {
            if (lo < hi)
            {
                int p = Partition(array, lo, hi);
                QuickSort(array, lo, p - 1);
                QuickSort(array, p + 1, hi);
            }
        }

        public static int Partition<T>(T[] array, int lo, int hi)
            where T : IComparable<T>
        {
            T piv = array[lo];
            int i = lo, j = lo + 1;
            while (j <= hi)
            {
                if (array[j].CompareTo(piv) < 0)
                    Swap(array, ++i, j);
                j++;
            }
            Swap(array, lo, i);
            return i;
        }

        public static int Partition2<T>(T[] array, int lo, int hi)
            where T : IComparable<T>
        {
            T piv = array[lo];
            int i = lo, j = hi;
            //loop invariant:
            //[lo, i] <= piv
            //[i + 1, j] - untested
            //[j + 1, hi] >= piv

            while (i < j)
            {
                while (i < j && array[j].CompareTo(piv) >= 0)
                    j--;
                while (i < j && array[i].CompareTo(piv) <= 0)
                    i++;
                if (i < j) Swap(array, i, j);
            }
            if (i > lo) Swap(array, lo, i);
            return i;
        }

        public static void Shuffle<T>(T[] array)
        {
            int n = array.Length;
            var r = new Random();
            for (int i = 0; i < n - 1; i++)
                Swap(array, i, r.Next(i + 1, n));
        }

        public static int[] GenerateArray(int length)
        {
            var r = new Random();
            var ret = new int[length];
            for (int i = 0; i < length; i++)
                ret[i] = r.Next();
            return ret;
        }

        public static int[] GenerateArrayInRange(int lo, int hi, int length)
        {
            var r = new Random();
            var ret = new int[length];
            for (int i = 0; i < length; i++)
                ret[i] = r.Next(lo, hi);
            return ret;
        }

        public static bool AreEqual<T>(T[] a, T[] b)
        {
            if (a == null && b == null) return true;
            if (a == null || b == null || a.Length != b.Length) return false;
            for (int i = 0; i < a.Length; i++)
                if (!a[i].Equals(b[i])) return false;
            return true;
        }
    }
}