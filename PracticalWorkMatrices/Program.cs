using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticalWorkMatrices
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const int a = 10000;
            const int b = 10000;

            var mtr = new int[a, b];
            var random = new Random();
            for (int i = 0; i < a; i++)
            {
                for (int j = 0; j < b; j++)
                {
                    mtr[i, j] = random.Next(-1000000, 1000000);

                }
            }

            int maxRow = -1;
            long maxSum = long.MinValue;

            var stopwatch = Stopwatch.StartNew();
            for (int i = 0; i < a; i++)
            {
                long rowSum = 0;
                for (int j = 0; j < b; j++)
                {
                    rowSum += mtr[i, j];
                }
                if (rowSum > maxSum)
                {
                    maxSum = rowSum;
                    maxRow = i;
                }
            }
            stopwatch.Stop();
            Console.WriteLine("Без параллельной обработки:");
            Console.WriteLine($"Максимальная сумма: {maxSum}, строка: {maxRow}, время: {stopwatch.Elapsed}");

            maxRow = -1;
            maxSum = long.MinValue;
            stopwatch.Restart();
            Parallel.For(0, a, i =>
            {
                long rowSum = 0;
                for (int j = 0; j < b; j++)
                {
                    rowSum += mtr[i, j];
                }
                lock (mtr)
                {
                    if (rowSum > maxSum)
                    {
                        maxSum = rowSum;
                        maxRow = i;
                    }
                }
            });
            stopwatch.Stop();
            Console.WriteLine("С параллельной обработкой:");
            Console.WriteLine($"Максимальная сумма: {maxSum}, строка: {maxRow}, время: {stopwatch.Elapsed}");

            Console.ReadKey();
        }
    }
}
