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
// using System;

// namespace NumberStatistics
// {
//     class Program
//     {
//         static void Main(string[] args)
//         {
//             Console.WriteLine("=== Number Statistics Calculator ===\n");

//             bool runAgain = true;

//             while (runAgain)
//             {
//                 // TODO: Ask how many numbers to enter (3 to 8)

//                 // TODO: Create an array or List to store the numbers

//                 // TODO: Loop and ask user to enter each number

//                 // TODO: Calculate sum, average, max, and min

//                 // TODO: Display all results neatly

//                 Console.WriteLine("\nWould you like to run again? (yes/no)");
//                 string answer = Console.ReadLine().ToLower().Trim();
//                 runAgain = answer == "yes" || answer == "y";
//             }

//             Console.WriteLine("Goodbye!");
//         }
//     }
// }