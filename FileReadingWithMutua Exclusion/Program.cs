using System;
using System.IO;
using System.Threading.Tasks;
using System.Threading;


// Multi-threading
class Program
{

    static void Main1(string[] args)
    {
        int[] numbers = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

        Parallel.ForEach(numbers, i =>
        {
            Console.WriteLine($"Processing number {i} on thrad {Task.CurrentId}");

        });

        Console.WriteLine("All processing complete.");
    }
}