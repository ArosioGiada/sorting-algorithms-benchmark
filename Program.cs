using System;
using System.IO;//to use File.ReadAllLines and Path.GetFileName
using System.Diagnostics;//to use Stopwatch

class Program
{
    // Main method. Used to manage the program: asks the user for a file path, reads and validates the file, sorts the numbers using both Selection Sort and Quick Sort, measures their execution times, and displays the results. 
    // To do so:
    // 1. Used While loop to start an infinite loop: allow the user to test multiple files until they choose to exit.
    // 2. Asks the user to enter a file path or ONLY press enter to exit. 
    // 3. Reads the input and trims, for example, eventualy whitespace.

    // 4. If statement n°1: check if the input is empty (user ONLY pressed enter), breaks the loop and ends the program.
    // 5. If statement n°2: check if the file exists at the given path. If not, prints an error message and restarts the While.

    // 6. Calls the ReadFile method to read and validate the file. Store the result in an int array called fileArray.

    // 7. If statement n°3: check if ReadFile returns null (file invalid or error), if so restarts the While.

    // 8. Get the file name from the path using Path.GetFileName and store it in a string called 'fileName' for display purposes.
    // 9. Created a Stopwatch object to measure execution times of the sorting algorithms.

    // 10. For Selection sort:
    //      - Made a clone of fileArray and called it 'SelectionCopy' (in order to do not modify the original file array, so it can be use again later in the code)
    //      - Restart Stopwatch (could have used Start instand of Restart because is used here the first time, so the Stopwatch is already at 0 but I decide to used Restart in case of future code updates that adds other methods before this)
    //      - Sorts the array 'SelectionCopy' calling SelectionSort method
    //      - Stop Stopwatch
    //      - Store in a double called time1 the time taken by the method SelectionSort to sort the array. It is measured by Stopwatch. (Used Elapsed.TotalMilliseconds to have more precise results)

    // 11. For Quick sort:
    //      - Made a new clone of fileArray and called it 'quickCopy' (I could have use the original here because is the last method but I decide to make another clone of the original in case of future code updates that adds other methods that need to use the same original array)
    //      - Restart Stopwatch - bring Stopwatchback to 0
    //      - Sorts the array 'quickCopy' calling quickSort method. pass to the method: the array, '0' that will indicate 'startIndex' and the length of the array -1 that will indicate 'endIndex'
    //      - Stop Stopwatch
    //      - Store in a double called time2 the time taken by the method quick sort to sort the array. It is measured by Stopwatch. (Used Elapsed.TotalMilliseconds to have more precise results)

    // 12. Create new string 'faster' 
    // 13. Compares the two times taken by the sorting methods to determine which sorting method was faster, or if they took the same time. To do that used a simple if/else if/else statement and in base of which one is true stores a different string into 'faster'.

    // 14. Used Console.WriteLine to print: file name, execution times for both sorting methods( :F3 is used to limite the decimals to 3), and which one was faster.
    
    // 15. The loop repeats, allowing the user to test another file or decide to exit.
    static void Main(string[] args)
    {
        while (true)
        {
            Console.Write("\nEnter a file path or ONLY press enter to exit: \n");
            string path = Console.ReadLine().Trim();

            if (string.IsNullOrEmpty(path))
            {
                break;
            }
            if (!File.Exists(path))
            {
                Console.WriteLine($"--- Error: File not found ---");
                continue;
            }

            int[] fileArray = ReadFile(path);

            if (fileArray == null)
            {
                continue;
            }

            string fileName = Path.GetFileName(path);
            Stopwatch sw = new Stopwatch();

            // Selection
            int[] SelectionCopy = (int[])fileArray.Clone();
            sw.Restart();
            SelectionSort(SelectionCopy);
            sw.Stop();
            double time1 = sw.Elapsed.TotalMilliseconds;

            // Quick Sort
            int[] quickCopy = (int[])fileArray.Clone();
            sw.Restart();
            QuickSort(quickCopy, 0, quickCopy.Length - 1);
            sw.Stop();
            double time2 = sw.Elapsed.TotalMilliseconds;

            // Determinate faster method
            string faster;
            if (time1 < time2)
            {
                faster = "Selection Sort";
            }
            else if (time1 > time2)
            {
                faster = "Quick Sort";
            }
            else
            {
                faster = "Both took the same time";
            }

            Console.WriteLine($"\nOn file: {fileName}");
            Console.WriteLine($"Selection Sort: {time1:F3} ms");
            Console.WriteLine($"Quick Sort: {time2:F3} ms");
            Console.WriteLine($"---- Faster method: {faster} ----\n");
        }
    }

    // ReadFile method. Used to read and validate the input file, passed to it via 'string path', then return an array of integers to be sorted. To do so:
    // 1. Reads all lines from the file, thanks to ''File.ReadAllLines(path)'', and stores them in a string array called linesFile.

    // 2. if statement n°1: check if file contains exactly two lines(as the file given, but could be modified in case of different file model). If not, prints an error and returns null to MAIN.

    // 3. if statement n°2: Tries to parse the first line of the file(linesFile[0]) as an integer(Used trim to eliminate eventual whitespace or not necessary hidden characters present in the line) and store it into ''line1''
    //          (variable ''line1'' rappresents tot number of values in the second line)
    //          If fails, prints an error and returns null to MAIN.

    // 4. Splits the second line(linesFile[1]) and store each number of the line into an array of strings (called line2), each representing a number. to do so:
    //          linesFile[1] = Take the 2° line of the file
    //          .Trim() = Strips off any spaces, tabs, or new-line characters at the very start or end of that line(as it does in the if above, to guarantee only the number are taken)
    //          .Split(' ', StringSplitOptions.RemoveEmptyEntries) = divides every value
    //                  a) ' ' = tells it to cut the string at every space character
    //                  b) StringSplitOptions.RemoveEmptyEntries = tells it to do not return empty string in case, for example, of 2 whitespaces in a row

    // 5. if statement n°3: Check that length of Line2 array correspond to the number stored into variable Line1. If not, prints an error and returns null to MAIN.

    // 6. Creates an integer array (numbers) of size line1. it will store the parsed numbers that now are stored as string in 'Line2' array.

    // 7. For loop that loop i from 0 to line1 - 1 and increment i of 1 everytime. It will Loops through each value in line2. Inside:
    //          If statement: Try to parse every elements of 'line2' string array into an integer.
    //                  If it succeeds, the parsed int is stored into 'numbers' int array.
    //                  If it fails, and any value is not a valid integer, prints an error and returns null to MAIN.
    
    // 8. If every previous steps succeeded, the code returns 'numbers' int array, filled up with all the integers find in the 2° line of the given file, to the MAIN.
    static int[] ReadFile(string path)
    {
        string[] linesFile = File.ReadAllLines(path);

        if (linesFile.Length != 2)
        {
            Console.WriteLine("--- Error: file must contain exactly two lines ---");
            return null;
        }

        if (!int.TryParse(linesFile[0].Trim(), out int line1))
        {
            Console.WriteLine("--- Error: the first line is not a valid integer ---");
            return null;
        }

        string[] line2 = linesFile[1].Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);

        if (line2.Length != line1)
        {
            Console.WriteLine($"--- Error: number of values ({line2.Length}) doesn't match expected count ({line1}) ---");
            return null;
        }

        int[] numbers = new int[line1];

        for (int i = 0; i < line1; i++)
        {
            if (!int.TryParse(line2[i], out numbers[i]))
            {
                Console.WriteLine("--- Error: file must contains only valid integer ---");
                return null;
            }
        }
        return numbers;
    }

    // SelectionSort. Used to sort the array by keep finding the largest element in the array and moving it to its correct position at the end. To do so:
    // 1. Use for loop to determine how many positions at the end have already been sorted (i). In order to calculare 'end' and stop the loop before confront again the already sorted elements.
    //          - inizialize 'i' at index 0. This will indicates how many iterations have already been done and so how many sorted elements are already placed at the bottom of the array.
    //          - set to run until i < of array length -1, so it will run from 0 to array length -2:
    //              Examples:
    //                  When i = 0, set the largest element in the entire array at the end.
    //                  When i = 1, set the second-largest value to the second-to-last position, and so on.
    //                  At i = array length - 2, only two elements remain to be sorted (the only two not yet set). So this will be the last loop because:
    //                  At i = array length - 1, only one element left, as last one will be sorted thanks to the previou loops of 2 elements, so the loop stop thanks to ''i < array.Length - 1'' 
    //          - increment i of 1 at each loop. To make possible for i to indicates how many elements are already sorted.

    // 2. Inizialize 'lasrgeIdx' to keep track of the index with the largest value find in the part of the array that is still unsorted. And let it starts from index 0(start of our array) 
    // 3. Inizialize 'end' as the last index of the still unsorted part of the array using: array length - 1 - i.
    //              Examples: (n = array.Length)
    //                  When i = 0, end = n-1, we consider the entire array[0..n-1].
    //                  When i = 1, end = n-2, we consider array[0..n-2], because array[n-1] is already the fixed maximum.
    //                  And so on up to i = n-2, where end = 1 and we only consider array[0..1].

    // 5. Use for loop to find the index of the largest element in the unsorted portion [0..end]. To do so:
    //          - inizialize 'j' at index 1 (to compare it with largestIdx that is already index 0)
    //          - set to run until j <= to the last unsorted index included, so it stop only before the already sorted elements
    //          - increment j of 1, in order to let it goes trough the array and confront every value of the array with the current largest index found
    // 6. Inside the for loop: Used if statement with condition: j value > 'largestIdx' value. If true: 'largestIdx' get the index of the current j

    // 7. Swaps the largest value found, stored now in largestIdx, thanks to the for loop, with the element at position 'end'. This will move the largest element of the unsorted part at the end of it.
    // 8. This repeats until the array is sorted.
    static void SelectionSort(int[] array)
    {
        for (int i = 0; i < array.Length - 1; i++)
        {
            int largestIdx = 0;
            int end = array.Length - 1 - i;

            for (int j = 1; j <= end; j++)
            {
                if (array[j] > array[largestIdx])
                {
                    largestIdx = j;
                }
            }
            Swap(ref array[largestIdx], ref array[end]);
        }
    }

    // QuickSort. Receive the array to sort, the ''start Index'' that rappresent where the first element of the array is placed, and endIndex that rappresent where the last element is. This is used to recursively sort the array. To do so:
    // 1. Used if statement: Checked if the array or segment to sort contains at least two elements using the condition: startIndex < endIndex. If true:
    // 2. Calls Partition() to divide the segment into two parts: one with elements < pivot and one with elements ≥ pivot.
    //    Creates a variable ''pi'' to store the final index of the pivot returned by Partition.
    // 3. Recursively calls QuickSort on the two segments:
    //    - From startIndex to pi-1 (elements < pivot). LEFT SIDE
    //    - From pi+1 to endIndex (elements ≥ pivot). RIGHT SIDE
    // The recursion ends when each segment contains only one element (already sorted). Thanks to: startIndex < endIndex. Here all the elements of the array are sorted. 
    static void QuickSort(int[] array, int startIndex, int endIndex)
    {
        if (startIndex < endIndex)
        {
            int pi = Partition(array, startIndex, endIndex);

            QuickSort(array, startIndex, pi - 1);
            QuickSort(array, pi + 1, endIndex);
        }
    }
    
    // Partition function. Rearranges the array segment around a chosen pivot so that elements < pivot are on the left and ≥ pivot on the right, then returns the pivot’s final index. To do so:
    //1. Created int that store the index of the pivot chosen and returned by MedianOfFirstLastMiddleIndexValue()
    //2. use Swap() to place the chosen pivot at the end of the array
    //3. Create pivot variable to store the value of the pivot

    //4. Inizialize i at -1 from the first index

    //5. for loop that inizialize j at the first index, start until j is < of the last index, so it stop before the pivot, to do not confront pivot with itself, and at the end increment j of 1.
    //6. inside the for loop: created a if condition that compare j value with the pivot value and if true: increment i of 1 and swap() its value with J value

    //7. Then, swapped the pivot value with the value of i(last index that resulted with a value < than the pivot) + 1. in order to store the pivot in its right position, with on its left only smaller number and its right equal or larger.
    //8. Return the pivot index i + 1
    static int Partition(int[] array, int startIndex, int endIndex)
    {
        int pivotIndex = MedianOfFirstLastMiddleIndexValue(array, startIndex, endIndex);
        Swap(ref array[pivotIndex], ref array[endIndex]);
        int pivotValue = array[endIndex];

        int i = startIndex - 1;

        for (int j = startIndex; j < endIndex; j++)
        {
            if (array[j] < pivotValue)
            {
                i++;
                Swap(ref array[i], ref array[j]);
            }
        }

        Swap(ref array[i + 1], ref array[endIndex]);
        return i + 1;
    }

    // MedianOfFirstLastMiddleIndexValue function. It is called by Partition method. Used to select the pivot index for QuickSort. To do so:
    // 1. Create var ''mid'' that store the middle index of the array/segment of the array. This is calculated using: low + (high - low) / 2, insteand of (low + high) / 2, to avoid integer overflow.
    // 2. Create a var and store in it the value that correspond to each index: value last index (high) = a, value middle index (mid) = b, value first index (low) = c
    // 3. Used if statement to compare the three values to find the median value (the one that is neither the largest or smallest).
    // 4. Returns the index (low, mid, or high) corresponding to the median value.
    static int MedianOfFirstLastMiddleIndexValue(int[] array, int low, int high)
    {
        int mid = low + (high - low) / 2;

        int a = array[high];
        int b = array[mid];
        int c = array[low];

        if ((b <= a && a <= c) || (c <= a && a <= b))
        {
            return high;
        }
        if ((a <= b && b <= c) || (c <= b && b <= a))
        {
            return mid;
        }
        return low;
    }

    //Simple Swap method that receive 2 ref integers, here called x and y and swap them. To do so:
    //1. Create new a temporary variable called tmp, that store the value of x
    //2. Set to x the value of y
    //3. Set to y the value of tmp
    static void Swap(ref int x, ref int y)
    {
        int tmp = x;
        x = y;
        y = tmp;
    }
}
