using System;
using System.IO;
using System.Threading.Tasks;
using System.Threading;
using System.Collections;
using System.Text;
using FileReadingWithMutua_Exclusion;
using FileReadingWithMutual_Exclusion;

// Multi-threading
class Program11
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

    static void Main3(string[] args)
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


class Program4
{
    private static Mutex processMutex = new Mutex(false, "FileWritingMutex"); // Mutex for process synchronization

    static void Main4(string[] args)
    {
        string filePath = "C:\\Users\\tshahbaz\\source\\repos\\FileReadingWithMutualExclusion\\FileReadingWithMutualExclusion\\TestFile3.txt";
        try
        {
            // Logger.Log("I_01");
            processMutex.WaitOne(2000); // Wait for ownership of the mutex
            // Logger.Log(Convert.ToString(Thread.CurrentThread.ManagedThreadId));

            using (StreamWriter writer = File.AppendText(filePath))
            {
                Console.WriteLine("Add Some data to write on file.");
            }
        }
        catch (Exception ex)
        {
            // Logger.Log("I_02");
            //// Handle the exception here
            // Logger.Log(ex.ToString());
        }
        finally
        {
            // Logger.Log("I_03");
            processMutex.ReleaseMutex(); // Release the mutex
        }
    }
}


// private static readonly object fileOperationLock = new Object();
// try
//                {
//                    lock (fileOperationLock)
//                    {
//                        File.WriteAllBytes(_RequestInfo.FileLocalPath, _ResponseInfo.FileContent);
//                        File.SetLastWriteTimeUtc(_RequestInfo.FileLocalPath, _ResponseInfo.ModifiedDate);
//                    }
 //               }
//                catch (Exception ex)
//                {
 //                   Logger.Log(ex.ToString());
 //               }
//
    class PracticeIEnumerator
    {
        static void Main6(string[] args)
        {
            int[] number = { 1, 2, 3, 4,90, 5, 6, 7, 8 };
            IEnumerator enumerator = number.GetEnumerator();
            while (enumerator.MoveNext())
            { 
                Console.WriteLine(enumerator.Current);
            }

            // Reset the enumerator to the beginning
            enumerator.Reset();

        }
   
    }

class PracticeFileStream
{
    static void Main7(string[] args)
    {
        string filePath = @"C:\Users\tshahbaz\source\repos\FileReadingWithMutualExclusion\FileReadingWithMutua Exclusion\fileStream.txt";
        //FileStream file = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None);
        //string text = "This is first line";
        //byte[] byteArray = Encoding.UTF8.GetBytes(text);
        //file.Write(byteArray);
        ////file.Write(byteArray,0,4);
        //file.Close();

        // ReadFile
        using (FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.None))
        using (StreamReader reader = new StreamReader(fileStream))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                Console.WriteLine(line);
            }
        }
        //file.Close();

    }
}
class PracticeDateTime
{
    static void Main8(string[] args)
    {
        DateTime dateTime = DateTime.Now;
        Console.WriteLine(dateTime);
        Console.WriteLine(dateTime.ToString("yyyy-MM-dd HH:mm:ss"));
        Console.WriteLine(dateTime.ToString("dddd, MMMM dd,yyyy HH:mm:ss"));
        Console.WriteLine(dateTime.ToString("dddd, MMMM dd,yyyy HH:mm:ss.fff zzz"));
        Console.WriteLine(dateTime.ToString("dddd, MMMM dd,yyyy h:mm:ss tt"));

        DateTime dateTime2 = DateTime.Today.AddDays(-1);
        Console.WriteLine(dateTime2);
        Console.WriteLine(dateTime > dateTime2);

        DateOnly dateOnly = new DateOnly();
        Console.WriteLine(dateOnly);
        Console.WriteLine(DateOnly.FromDateTime(DateTime.Now));

        TimeOnly timeOnly = new TimeOnly();
        Console.WriteLine(timeOnly);
        Console.WriteLine(TimeOnly.FromDateTime(DateTime.Now));


    }

}

    class PracticeCustomFilter
    { 
        static void Main10(string[] args)
        {
            List<Employee> employeeList = EmployeeData.GetEmployees();
            List<Employee> filteredEmployees = employeeList.MyWhere(e => e.Department == "Admin" );


            foreach (var employee in filteredEmployees)
            {

                Console.WriteLine($"Employee Id : {employee.Id}");
                Console.WriteLine($"Epmloyee Name :{employee.FirstName}");
                Console.WriteLine($"Epmloyee Department: {employee.Department} ");
                Console.WriteLine($"Employee isManager :{employee.isMananger } \n");
            }


            List<int> filteredEmployees2 = employeeList.MySelect(e => e.Id);
            Console.WriteLine(filteredEmployees2);
            foreach (var item in filteredEmployees2)
            {

                Console.WriteLine($"Employee Id : {item}");
             
            }
        }

    }


//class EODPriceTask
//{
//    static public void Main12(string[] args)
//    {

//        string EODPriceFilePath = @"C:\SourceTree\TrafixMaster\Servers\ReportServer\x64\Debug\Config\20240111_Trafix_EOD_Prices_modified.CSV";
//            if (File.Exists(EODPriceFilePath))
//        {
//            try
//            {
//                Workbook my_workbook = new Workbook();
//                my_workbook.LoadDocument(EODPriceFilePath);

//                Worksheet my_worksheet = my_workbook.Worksheets[0];
//                CellRange usedRange = my_worksheet.GetUsedRange();

//                int rowCount = usedRange.RowCount;
//                int colCount = usedRange.ColumnCount;

//                for (int row = 0; row < rowCount; row++)
//                {
//                    for (int col = 0; col < colCount; col++)
//                    {

//                        var cellValue = usedRange[row, col].Value;


//                    }

//                }
//            }
//            catch (Exception ex)
//            {
//                //Logger.Log(ex.ToString());
//            }
            

//    }

//    }
//}



class Program
{
    static void Main(string[] args)
    {
        string filePath = "C:\\Users\\\\tshahbaz\\\\source\\\\repos\\\\FileReadingWithMutualExclusion\\\\FileReadingWithMutua Exclusion\\His-CAT-3.csv";
        int lineNumberToInsert = 3; // Change this to your desired line number
        string dataToInsert = "new,data,to,insert"; // Change this to your desired data

        InsertDataIntoCSV(filePath, lineNumberToInsert, dataToInsert);
    }

    static void InsertDataIntoCSV(string filePath, int lineNumber, string data)
    {
        // Create a temporary file to write the modified data
        string tempFilePath = Path.GetTempFileName();

        using (StreamWriter writer = new StreamWriter(tempFilePath))
        using (StreamReader reader = new StreamReader(filePath))
        {
            // Write data to temporary file up to the line where you want to insert
            for (int i = 1; i < lineNumber; i++)
            {
                string line = reader.ReadLine();
                if (line == null)
                {
                    // Handle case where lineNumber is greater than number of lines in file
                    //throw new InvalidOperationException("Specified line number exceeds total lines in file.");
                }
                writer.WriteLine(line);
            }

            // Write the new data at the desired line number
            writer.WriteLine(data);

            // Write remaining data from original file
            string remainingContent = reader.ReadToEnd();
            writer.Write(remainingContent);
        }

        // Replace the original file with the modified temporary file
        File.Delete(filePath);
        File.Move(tempFilePath, filePath);
    }
}