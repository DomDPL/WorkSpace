// Create a console application called ExpenseTracker that does the following:

// Ask the user how many expenses they want to enter (between 1 and 8).
// For each expense, ask for:
// Description (e.g. "Groceries", "Gas", "Movie Tickets")
// Amount (a positive decimal number)
// After all expenses are entered, display:
// A nice list of all expenses with their amounts
// The total amount spent
// The average expense amount
// The most expensive single expense (show description and amount)
// Ask the user if they want to run the program again (yes/no).
// Minimum Requirements to Pass
// Use dotnet new console to create the project
// Use arrays (or List<T> if you know how) to store the expenses
// Validate that the amount entered is greater than 0
// Use at least one method (for example, to calculate the total)
// Format money values nicely (show 2 decimal places)
// Program must not crash on normal input
// 💡 Helpful Hints for Beginners:
// Use double to store money amounts
// Use Console.WriteLine("{0:C}", amount); or :F2 to format money
// You can use two arrays: one for descriptions and one for amounts
// Keep it simple — no need for classes on this version
// Use a do-while or while loop for the "run again" feature
// 🚀 How to Start (CLI Commands)
// dotnet new console -o ExpenseTracker
// cd ExpenseTracker
// code .
//using System;
using System.Text.RegularExpressions;

bool runAgain;
do{
    Console.WriteLine();
    Console.WriteLine("How many expenses would you like to calculater (1 - 8)");
    string? input = Console.ReadLine()?.Trim();
    int numberOfExpenses = int.TryParse(input, out numberOfExpenses) ? numberOfExpenses : 0;
    while (numberOfExpenses < 1 || numberOfExpenses > 8)
    {
        Console.WriteLine("Invalid input. Please enter a number between 1 and 8.");
        input = Console.ReadLine()?.Trim();
        numberOfExpenses = int.TryParse(input, out numberOfExpenses) ? numberOfExpenses : 0;
    }

    // Create a 2D array to store descriptions and amounts of expenses
    string[,] expenses = new string[numberOfExpenses, 2];
    for (int i = 0; i < numberOfExpenses; i++)
    {
        Console.WriteLine($"Enter description for expense {i + 1}:");
        string description = Console.ReadLine()?.Trim() ?? "Error: No description entered";
        while (string.IsNullOrWhiteSpace(description) || !Regex.IsMatch(description, @"^[a-zA-Z\s]+$"))
        {
            Console.WriteLine("Invalid input. Please enter a valid description.");
            description = Console.ReadLine()?.Trim() ?? "Error: No description entered";
        }

        Console.WriteLine($"Enter amount for expense {i + 1}:");
        string? amountInput = Console.ReadLine()?.Trim();
        double amount = double.TryParse(amountInput, out amount) ? amount : -1;
        while (amount <= 0)
        {
            Console.WriteLine("Invalid input. Please enter a positive number.");
            amountInput = Console.ReadLine()?.Trim();
            amount = double.TryParse(amountInput, out amount) ? amount : -1;
        }
        // Store description and amount in the expenses 2D array
        expenses[i, 0] = description;
        expenses[i, 1] = amount.ToString("F2");
    }

    // Access data from the 2D array to display expenses and calculate totals
    Console.WriteLine("\nExpenses:");
    double total = 0;
    double maxAmount = 0;
    string HighestDescription = "";
    for (int i = 0; i < numberOfExpenses; i++)
    {
        string description = expenses[i, 0]; // Get description from first column of the 2D array
        double amount = double.Parse(expenses[i, 1]); // Get amount from second column and parse it back to double
        Console.WriteLine($"{description}: {amount:C}"); // Format to Culture-specific currency
        total += amount;
        if (amount > maxAmount)
        {
            maxAmount = amount;
            HighestDescription = description;
        }
    }

    // Calculate average and display all results
    double average = total / numberOfExpenses;
    Console.WriteLine($"\nTotal: {total:C}");
    Console.WriteLine($"Average: {average:C}");
    Console.WriteLine($"Most expensive: {HighestDescription} ({maxAmount:C})");

    Console.WriteLine("\nWould you like to run the program again? (yes/no)");
    string response = Console.ReadLine()?.Trim().ToLower();
    runAgain = response == "yes";
} while (runAgain);

Console.WriteLine("Goodbye!");
    
