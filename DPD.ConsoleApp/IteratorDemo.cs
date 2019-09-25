using System;
using System.Collections.Generic;

namespace DPD.ConsoleApp
{
    internal static class IteratorDemo
    {
        public static void Run()
        {
            foreach (var number in InfiniteSequence())
            {
                Console.WriteLine(number);
            }
        }

        private static IEnumerable<long> InfiniteSequence(long start = 0)
        {
            while (true)
            {
                yield return start++;
            }
        }
    }
}