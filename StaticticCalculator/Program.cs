// Your program should do the following:

// Ask the user: "How many numbers do you want to enter?" (accept 3 to 8 numbers)
// Ask the user to enter that many numbers (can be decimals)
// After all numbers are entered, display:
// All the numbers entered
// The sum of the numbers
// The average (rounded to 2 decimal places)
// The largest number
// The smallest number
// Ask if the user wants to run the program again (yes/no)
// Minimum Requirements
// Use a loop to collect the numbers
// Store the numbers in an array or list
// Calculate sum, average, max, and min
// Handle basic input safely
// Output must be clean and easy to read
// 💡 Tip: Start simple. Get it working for one run first, then add the "run again" feature.
// Starter Code for Each Language
// C# Starter (Program.cs)

using System;

namespace NumberStatistics
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Number Statistics Calculator ===\n");

            bool runAgain = true;

            while (runAgain)
            {
                // TODO: Ask how many numbers to enter (3 to 8)
                Console.WriteLine("How many numbers would you like to calculate (3 - 8)");
                string input = Console.ReadLine()?.Trim();
                while(!int.TryParse(input, out int validInput) || validInput < 3 || validInput > 8)
                {
                    Console.WriteLine("Error: enter numbet between 3 and 8.");
                    input = Console.ReadLine()?.Trim();
                }

                // TODO: Create an array or List to store the numbers
                List<int> store = new List<int>();

                // TODO: Loop and ask user to enter each number
                int counter = 0;
                int indexer = 1;
                int start = int.Parse(input);
                //bool exit = false;
                do{

                    Console.WriteLine($"Enter number {indexer}:");
                    string numberInput = Console.ReadLine()?.Trim();

                    while (!int.TryParse(numberInput, out int number))
                    {
                        Console.WriteLine("Invalid input. Please enter a valid number:");
                        numberInput = Console.ReadLine()?.Trim();
                    }
                    store.Add(int.Parse(numberInput));
                    counter++;
                    indexer++;
                    
                }while(counter < start);
                
                // TODO: Calculate 
                // sum, 
                var sum = store.Sum();

                //average, 
                var average = (double)sum / store.Count();
                // max, and 
                var max = store.Max();
                // min
                var min = store.Min();

                // TODO: Display all results neatly
                string concat = "";
                for(int i = 0; i < store.Count; i++ )
                {
                    var display = store[i] + " + ";
                    concat += display;
                }
                if(concat.EndsWith(" + "))
                {
                    concat = concat.Substring(0, concat.Length - 3); // Remove the last " + " from the string
                }
                Console.Write("\nSum: ");
                Console.Write(concat);
                Console.Write($" = {sum}");

                Console.WriteLine($"\nAverage: {average:F2}");
                Console.WriteLine($"Largest number: {max}");
                Console.WriteLine($"Smallest number: {min}");

                Console.WriteLine("\nWould you like to run again? (yes/no)");
                string answer = Console.ReadLine().ToLower().Trim();
                runAgain = answer == "yes" || answer == "y";
            }

            Console.WriteLine("Goodbye!");
        }
    }
}