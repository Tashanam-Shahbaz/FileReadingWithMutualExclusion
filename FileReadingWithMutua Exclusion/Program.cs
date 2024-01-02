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

    static void Main2(string[] args)
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


class Program3
{
    static object fileLock = new object(); // Lock object for file access
    static int deadlockTimeout = 1; // 5 seconds timeout for deadlock detection

    static void Main(string[] args)
    {
        string filePath = "C:\\Users\\tshahbaz\\source\\repos\\FileReadingWithMutualExclusion\\FileReadingWithMutua Exclusion\\TestFile3.txt";
        int numberOfThreads = 4; // Set the number of threads

        // Parallel processing using Task.Run to simulate threads
        Task[] tasks = new Task[numberOfThreads];

        for (int i = 0; i < numberOfThreads; i++)
        {
            int threadIndex = i;
            tasks[i] = Task.Run(() =>
            {
                bool lockTaken = false;

                try
                {
                    // Attempt to acquire the lock within the specified timeout
                    if (Monitor.TryEnter(fileLock, deadlockTimeout))
                    {
                        lockTaken = true;
                        // Perform operations on the locked file
                        using (StreamWriter writer = File.AppendText(filePath))
                        {
                            writer.WriteLine($"Thread {threadIndex + 1} writing to the file at {DateTime.Now}");
                        }

                        Console.WriteLine($"Thread {threadIndex + 1} completed writing.");
                    }
                    else
                    {
                        Console.WriteLine($"Thread {threadIndex + 1} failed to acquire lock within the timeout.");
                        // case when the lock couldn't be acquired within the timeout
                    }
                }
                finally
                {
                    if (lockTaken)
                    {
                        Monitor.Exit(fileLock);
                    }
                }
            });
        }

        Task.WaitAll(tasks); // Wait for all threads to complete

        Console.WriteLine("All processes finished executing.");
    }
}