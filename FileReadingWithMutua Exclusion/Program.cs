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


class Program2
{
    static object fileLock = new object();

    static void Main(string[] args)
    {
        string filePath = "C:\\Users\\tshahbaz\\source\\repos\\FileReadingWithMutualExclusion\\FileReadingWithMutua Exclusion\\TestFile.txt";
        int numberOfThreads = 4;

        lock (fileLock)
        {

            using (StreamWriter writer = File.AppendText(filePath))
            {
                writer.WriteLine($"Main Thread  writing to the file at {DateTime.Now}");
            }

            Console.WriteLine($" Main Thread  completed writing.");
        }

        Parallel.For(0, numberOfThreads, i =>
        {
           
            lock (fileLock) 
            {
               
                using (StreamWriter writer = File.AppendText(filePath))
                {
                    writer.WriteLine($"Thread {i + 1} writing to the file at {DateTime.Now}");
                }

                Console.WriteLine($"Thread {i + 1} completed writing.");
            }

          
            Thread.Sleep(1000);
        });

        Console.WriteLine("All processes finished executing.");
    }
}
