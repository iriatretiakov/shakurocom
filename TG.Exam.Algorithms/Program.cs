using System;

namespace TG.Exam.Algorithms
{
    class Program
    {
        static int Foo(int a, int b, int c)
        {
            //change to just math formula w\o 
            //if (1 < c)
            //    return Foo(b, b + a, c - 1);
            //else
            for (int i=1; i < c; i++)
            {
                b += a;
                a = b - a;
            }

            return a;
        }

        static int[] Bar(int[] arr)
        {
            // just bubble sorting O(n^2)
            //for (int i = 0; i < arr.Length; i++)
            //for (int j = 0; j < arr.Length - 1; j++)
            //    if (arr[j] > arr[j + 1])
            //    {
            //        int t = arr[j];
            //        arr[j] = arr[j + 1];
            //        arr[j + 1] = t;
            //    }

            // quick sort - O(n*log(n)), but O(n^2) - when array already sorted
            QuickSort(arr, 0, arr.Length - 1);
            return arr;
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Foo result: {0}", Foo(7, 2, 8));
            Console.WriteLine("Bar result: {0}", string.Join(", ", Bar(new[] { 7, 2, 8 })));

            Console.ReadKey();
        }

        private static void QuickSort(int[] arr, int left, int right)
        {
            if (left < right)
            {
                int pivot = Partition(arr, left, right);

                if (pivot > 1)
                {
                    QuickSort(arr, left, pivot - 1);
                }
                if (pivot + 1 < right)
                {
                    QuickSort(arr, pivot + 1, right);
                }
            }

        }

        private static int Partition(int[] arr, int left, int right)
        {
            int pivot = arr[left];
            while (true)
            {

                while (arr[left] < pivot)
                {
                    left++;
                }

                while (arr[right] > pivot)
                {
                    right--;
                }

                if (left < right)
                {
                    if (arr[left] == arr[right]) return right;

                    int temp = arr[left];
                    arr[left] = arr[right];
                    arr[right] = temp;


                }
                else
                {
                    return right;
                }
            }
        }
    }
}
