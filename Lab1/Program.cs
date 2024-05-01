using System;
using System.Diagnostics;

class SortingAlgorithms
{
    // Bubble Sort
    public static void BubbleSort<T>(T[] array) where T : IComparable<T>
    {
        long startMemory = GC.GetTotalMemory(true);

        int n = array.Length;
        long comparisons = 0;
        long swaps = 0;

        var stopWatch = Stopwatch.StartNew();

        for (int i = 0; i < n - 1; i++)
        {
            for (int j = 0; j < n - i - 1; j++)
            {
                comparisons++;
                if (array[j].CompareTo(array[j + 1]) > 0)
                {
                    swaps++;
                    T temp = array[j];
                    array[j] = array[j + 1];
                    array[j + 1] = temp;
                }
            }
        }

        stopWatch.Stop();
        long endMemory = GC.GetTotalMemory(true);

        Console.WriteLine($"Bubble Sort - Comparisons: {comparisons}, Swaps: {swaps}, Time (ms): {stopWatch.ElapsedMilliseconds}, Memory used: {(endMemory - startMemory) / 1024} KB");
    }

    // Insertion Sort
    public static void InsertionSort<T>(T[] array) where T : IComparable<T>
    {
        int n = array.Length;
        long comparisons = 0;
        long swaps = 0;

        long startMemory = GC.GetTotalMemory(true);
        var stopWatch = Stopwatch.StartNew();

        for (int i = 1; i < n; ++i)
        {
            T key = array[i];
            int j = i - 1;

            while (j >= 0 && array[j].CompareTo(key) > 0)
            {
                comparisons++;
                array[j + 1] = array[j];
                j = j - 1;
                swaps++;
            }
            array[j + 1] = key;
        }

        stopWatch.Stop();
        long endMemory = GC.GetTotalMemory(true);

        Console.WriteLine($"Insertion Sort - Comparisons: {comparisons}, Swaps: {swaps}, Time (ms): {stopWatch.ElapsedMilliseconds}, Memory used: {(endMemory - startMemory) / 1024} KB");
    }

    // Merge Sort
    public static void MergeSort<T>(T[] array) where T : IComparable<T>
    {
        long startMemory = GC.GetTotalMemory(true);
        var stopwatch = Stopwatch.StartNew();
        int comparisons = 0;
        int swaps = 0;

        T[] tempArray = new T[array.Length];
        MergeSort(array, tempArray, 0, array.Length - 1, ref comparisons, ref swaps);

        stopwatch.Stop();
        long endMemory = GC.GetTotalMemory(true);

        Console.WriteLine($"Merge Sort - Comparisons: {comparisons}, Swaps: {swaps}, Time (ms): {stopwatch.ElapsedMilliseconds}, Memory used: {(endMemory - startMemory) / 1024} KB");
    }

    private static void MergeSort<T>(T[] array, T[] tempArray, int left, int right, ref int comparisons, ref int swaps) where T : IComparable<T>
    {
        if (left < right)
        {
            int mid = (left + right) / 2;
            MergeSort(array, tempArray, left, mid, ref comparisons, ref swaps);
            MergeSort(array, tempArray, mid + 1, right, ref comparisons, ref swaps);
            Merge(array, tempArray, left, mid, right, ref comparisons, ref swaps);
        }
    }

    private static void Merge<T>(T[] array, T[] tempArray, int left, int mid, int right, ref int comparisons, ref int swaps) where T : IComparable<T>
    {
        int i = left, j = mid + 1, k = left;

        // Copy elements from array to tempArray
        for (int l = left; l <= right; l++)
        {
            tempArray[l] = array[l];
            swaps++;
        }

        while (i <= mid && j <= right)
        {
            comparisons++;
            if (tempArray[i].CompareTo(tempArray[j]) <= 0)
            {
                array[k] = tempArray[i];
                i++;
            }
            else
            {
                array[k] = tempArray[j];
                j++;
            }
            k++;
            swaps++;
        }

        while (i <= mid)
        {
            array[k] = tempArray[i];
            k++;
            i++;
            swaps++;
        }
    }

    // Quick Sort
    public static void QuickSort<T>(T[] array) where T : IComparable<T>
    {
        long startMemory = GC.GetTotalMemory(true);
        var stopwatch = Stopwatch.StartNew();
        long comparisons = 0;
        long swaps = 0;

        QuickSort(array, 0, array.Length - 1, ref comparisons, ref swaps);

        stopwatch.Stop();
        long endMemory = GC.GetTotalMemory(true);

        Console.WriteLine($"Quick Sort - Comparisons: {comparisons}, Swaps: {swaps}, Time (ms): {stopwatch.ElapsedMilliseconds}, Memory used: {(endMemory - startMemory) / 1024} KB");
    }

    private static void QuickSort<T>(T[] array, int low, int high, ref long comparisons, ref long swaps) where T : IComparable<T>
    {
        if (low < high)
        {
            int pi = Partition(array, low, high, ref comparisons, ref swaps);

            QuickSort(array, low, pi - 1, ref comparisons, ref swaps);
            QuickSort(array, pi + 1, high, ref comparisons, ref swaps);
        }
    }

    private static int Partition<T>(T[] array, int low, int high, ref long comparisons, ref long swaps) where T : IComparable<T>
    {
        T pivot = array[high];
        int i = low - 1;

        for (int j = low; j < high; j++)
        {
            comparisons++;
            if (array[j].CompareTo(pivot) <= 0)
            {
                i++;
                Swap(ref array[i], ref array[j]);
                swaps++;
            }
        }

        Swap(ref array[i + 1], ref array[high]);
        swaps++;

        return i + 1;
    }

    private static void Swap<T>(ref T a, ref T b)
    {
        T temp = a;
        a = b;
        b = temp;
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Generate descending integer array
        int[] descendingArray = GenerateDescendingArray(100000);

        // Generate ascendind integer array
        int[] ascendingArrar = GenerateAscendingArray(10000);

        // Generate random integer array
        int[] array = GenerateRandomArray(10000);

        // Sorting algorithms
        Console.WriteLine("Sorting Algorithms:");

        // Bubble Sort
        int[] bubbleArray = (int[])descendingArray.Clone();
        Console.WriteLine("\nBubble Sort:");
        SortingAlgorithms.BubbleSort(bubbleArray);

        // Insertion Sort
        int[] insertionArray = (int[])descendingArray.Clone();
        Console.WriteLine("\nInsertion Sort:");
        SortingAlgorithms.InsertionSort(insertionArray);

        // Merge Sort
        int[] mergeArray = (int[])descendingArray.Clone();
        Console.WriteLine("\nMerge Sort:");
        SortingAlgorithms.MergeSort(mergeArray);

        /*// Quick Sort
        int[] quickArray = (int[])descendingArray.Clone();
        Console.WriteLine("\nQuick Sort:");
        SortingAlgorithms.QuickSort(quickArray);*/
    }

    static int[] GenerateDescendingArray(int size)
    {
        int[] array = new int[size];
        for (int i = 0; i < size; i++)
        {
            array[i] = size - i;
        }
        return array;
    }

    static int[] GenerateAscendingArray(int size)
    {
        int[] array = new int[size];
        for (int i = 0; i < size; i++)
        {
            array[i] = i;
        }
        return array;
    }

    static int[] GenerateRandomArray(int size)
    {
        Random random = new Random();
        int[] array = new int[size];
        for (int i = 0; i < size; i++)
        {
            array[i] = random.Next();
        }
        return array;
    }

}

/*static void PrintArray<T>(T[] array)
{
    foreach (var item in array)
    {
        Console.Write(item + " ");
    }
    Console.WriteLine();
}
}*/