using System;
using System.Diagnostics;

namespace Rozumatic_ASD_Lab2
{
    class Program
    {
        // 1. Пошук мінімуму, максимуму та їх індексів
        static void FindExtremes(int[] arr, out int minVal, out int minIdx, out int maxVal, out int maxIdx)
        {
            minVal = arr[0]; minIdx = 0;
            maxVal = arr[0]; maxIdx = 0;

            for (int i = 1; i < arr.Length; i++)
            {
                if (arr[i] < minVal) { minVal = arr[i]; minIdx = i; }
                if (arr[i] > maxVal) { maxVal = arr[i]; maxIdx = i; }
            }
        }

        // 2. In-place реверс масиву двома вказівниками
        static void ReverseInPlace(int[] arr)
        {
            int left = 0;
            int right = arr.Length - 1;
            while (left < right)
            {
                int temp = arr[left];
                arr[left] = arr[right];
                arr[right] = temp;
                left++;
                right--;
            }
        }

        // 3. Фільтрація елементів: вибір лише додатних парних
        static int[] FilterPositiveEven(int[] arr)
        {
            int count = 0;
            for (int i = 0; i < arr.Length; i++)
                if (arr[i] > 0 && arr[i] % 2 == 0) count++;

            int[] result = new int[count];
            int idx = 0;
            for (int i = 0; i < arr.Length; i++)
                if (arr[i] > 0 && arr[i] % 2 == 0) result[idx++] = arr[i];

            return result;
        }

        // Варіант 1
        static double CalculateAverageBetweenExtremes(int[] arr, int minIdx, int maxIdx)
        {
            int start = Math.Min(minIdx, maxIdx) + 1;
            int end = Math.Max(minIdx, maxIdx);
            if (start >= end) return 0;

            double sum = 0;
            for (int i = start; i < end; i++) sum += arr[i];
            return sum / (end - start);
        }

        // Варіант 2
        static void ShiftRight(int[] arr, int k)
        {
            if (arr.Length == 0) return;
            k = k % arr.Length;
            if (k < 0) k += arr.Length;
            
            ReverseRange(arr, 0, arr.Length - 1);
            ReverseRange(arr, 0, k - 1);
            ReverseRange(arr, k, arr.Length - 1);
        }
        
        static void ReverseRange(int[] arr, int start, int end)
        {
            while (start < end)
            {
                int temp = arr[start];
                arr[start] = arr[end];
                arr[end] = temp;
                start++; end--;
            }
        }

        // Варіант 3
        static int RemoveDuplicatesInPlace(int[] arr)
        {
            if (arr.Length == 0) return 0;
            int uniqueIdx = 0;
            for (int i = 1; i < arr.Length; i++)
            {
                if (arr[i] != arr[uniqueIdx])
                {
                    uniqueIdx++;
                    arr[uniqueIdx] = arr[i];
                }
            }
            return uniqueIdx + 1;
        }

        static void Main(string[] args)
        {
            Console.WriteLine("======================================");
            Console.WriteLine("   РОЗУМАТИК // ЛАБОРАТОРНА РОБОТА №2");
            Console.WriteLine("======================================\n");

            int[] numbers = { 42, -7, 18, 99, 0, -15, 8, 24, -3, 55 };
            Console.WriteLine("Вихідний масив: [ " + string.Join(", ", numbers) + " ]\n");

            // Прогрів JIT
            int[] warmupArr = (int[])numbers.Clone();
            FindExtremes(warmupArr, out _, out _, out _, out _);
            FilterPositiveEven(warmupArr);
            ReverseInPlace(warmupArr);

            Stopwatch sw = Stopwatch.StartNew();

            // 1. Екстремуми
            FindExtremes(numbers, out int minVal, out int minIdx, out int maxVal, out int maxIdx);
            Console.WriteLine("Мінімум: " + minVal + " (індекс " + minIdx + ")");
          // 2. Фільтрація
            int[] filtered = FilterPositiveEven(numbers);
            Console.WriteLine("\nДодатні парні числа: [ " + string.Join(", ", filtered) + " ]");

            // 3. In-place реверс
            ReverseInPlace(numbers);
            Console.WriteLine("\nМасив після реверсу: [ " + string.Join(", ", numbers) + " ]");

            sw.Stop();
            Console.WriteLine("\nЗагальний час виконання: " + sw.Elapsed.TotalMilliseconds + " мс");

            // Демонстрація варіантів
            Console.WriteLine("\n======================================");
            Console.WriteLine("   ДЕМОНСТРАЦІЯ ВАРІАНТІВ");
            Console.WriteLine("======================================\n");

            int[] var1Arr = { 10, 5, 20, 4, 30, 2, 10 }; 
            FindExtremes(var1Arr, out int v1Min, out int v1MinIdx, out int v1Max, out int v1MaxIdx);
            double avg = CalculateAverageBetweenExtremes(var1Arr, v1MinIdx, v1MaxIdx);
            Console.WriteLine("[Варіант 1] Середнє між мін та макс: " + avg.ToString("F2") + "\n");

            int[] var2Arr = { 1, 2, 3, 4, 5 };
            Console.WriteLine("[Варіант 2] Оригінал: [" + string.Join(", ", var2Arr) + "]");
            ShiftRight(var2Arr, 2);
            Console.WriteLine("Зсув праворуч на 2: [" + string.Join(", ", var2Arr) + "]\n");

            int[] var3Arr = { 1, 1, 2, 2, 2, 3, 4, 4, 5 };
            Console.WriteLine("[Варіант 3] Оригінал: [" + string.Join(", ", var3Arr) + "]");
            int newLength = RemoveDuplicatesInPlace(var3Arr);
            int[] uniqueResult = new int[newLength];
            Array.Copy(var3Arr, uniqueResult, newLength);
            Console.WriteLine("Без дублікатів: [" + string.Join(", ", uniqueResult) + "]\n");

            Console.WriteLine("Тестування завершено.");
        }
    }
}
