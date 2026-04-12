
// Once you have completed the main assignment, challenge yourself by improving your program with the following features:

// Use a Class instead of arrays
// Create a new class called Student
// The class should have these properties:
// string Name
// int Score1, Score2, Score3
// double Average
// string LetterGrade
// Add a method inside the Student class called CalculateAverageAndGrade()
// Use a List instead of arrays
// Store all students in a List<Student>
// Save data to a file
// After displaying the results, ask the user if they want to save the report.
// If yes, save all student data + class average to a file called StudentReport.txt
// Use proper formatting so the file is easy to read.
// Load previous report (Advanced)
// At the start of the program, check if StudentReport.txt exists.
// If it does, ask the user if they want to see the last saved report.
// 💡 Bonus Hints:
// Use System.Collections.Generic for List<Student>
// Use System.IO and File.WriteAllText() or StreamWriter for saving files
// Make your Student class clean and reusable
// You can keep your original working code and create a new file called ProgramBonus.cs for this version (recommended for learning)
// Bonus Completion Criteria
// Program uses a proper Student class
// All students are stored in a List<Student>
// Successfully saves a well-formatted report to StudentReport.txt
// Code is clean, readable, and well-commented
// Pro Tip: Completing this bonus version shows you understand Object-Oriented Programming (OOP) and file handling — skills that are very valuable in real C# development!
using System.Text.RegularExpressions;
using StudentGradeManager;

Student student = new Student();

// Check if the report file exists and ask the user if they want to see the last saved report
string reportFilePath = "StudentReport.txt";

if (File.Exists(reportFilePath))
{
    Console.WriteLine();
    Console.WriteLine("A previous report was found. Do you want to see the last saved report? (yes/no)");
    string? showReportInput = Console.ReadLine()?.Trim().ToLower();
    while (showReportInput != "yes" && showReportInput != "no")
    {
        Console.WriteLine("Please enter 'yes' or 'no':");
        showReportInput = Console.ReadLine()?.Trim().ToLower();
    }
    if (showReportInput == "yes")
    {
        string reportContent = File.ReadAllText(reportFilePath);
        Console.WriteLine("\nLast Saved Report:");
        Console.WriteLine(reportContent);
    }
}

else
{
    Console.WriteLine("No previous report found.");
}

bool runAgain = true;
while (runAgain)
{
    Console.WriteLine("");

    Console.WriteLine("How many students do you want to enter? (1-10)");
    string? input = Console.ReadLine();
    int numberOfStudents;
    while (!int.TryParse(input, out numberOfStudents) || numberOfStudents < 1 || numberOfStudents > 10)
    {
        Console.WriteLine("Please enter a valid number of students (1-10):");
        input = Console.ReadLine();
    }


    // Outer loop for the score to be tied to the name.
    for (int i = 0; i < numberOfStudents; i++)
    {

        Console.WriteLine("Enter student name");
        string? name = Console.ReadLine()?.Trim().ToLower();

        // Only accepts string input.
        while (string.IsNullOrEmpty(name) || !Regex.IsMatch(name, @"^[a-zA-Z\s]+$"))
        {
            Console.WriteLine("Please enter a valid name (letters only):");
            name = Console.ReadLine()?.Trim().ToLower();
        }
        //inner loop for the number of socres.
        student.Name = name;
        for (int j = 0; j < 3; j++)
        {
            Console.WriteLine($"Enter test score {j + 1} (0-100)");
            string? scoreInput = Console.ReadLine();
            int score;

            // Keep the user in the loop untill they entered the right range of score.
            while (!int.TryParse(scoreInput, out score) || score < 0 || score > 100)
            {
                Console.WriteLine("Please enter a valid test score (0-100):");
                scoreInput = Console.ReadLine();
            }
            // asign score to the student property in student class.
            switch (j)
            {
                case 0:
                    student.Score1 = score;
                    break;
                case 1:
                    student.Score2 = score;
                    break;
                case 2:
                    student.Score3 = score;
                    break;
            }
        }
        // Print results before saving to the file.
        student.CalculateAverageAndGrade(student.Score1, student.Score2, student.Score3);
        Console.WriteLine($"Average score for {student.Name}: {student.Average}");
        Console.WriteLine($"Letter grade for {student.Name}: {student.LetterGrade = GetLetterGrade(student.Average)}");
        student.StoredStudents.Add(student);

        // Save to the file.
        bool accept = true;
        do
        {
            Console.WriteLine("Do you want to store the report to a file? (yes/no)");
            string? saveReportInput = Console.ReadLine()?.Trim().ToLower();
            if (saveReportInput == "yes")
            {
                if (student.Name == string.Empty)
                {
                    Console.WriteLine("No students to save.");
                    break;
                }
                HandleFileSaving(student.StoredStudents);
                accept = true; // remain in the loop untill the numver of students entered are finishes.
            }
            else if (saveReportInput == "no")
            {
                accept = true;
            }
            else
            {
                Console.WriteLine("Please enter 'yes' or 'no':");
                accept = false;
            }
        } while (!accept);
    }

    // get grade
    static string GetLetterGrade(double average)
    {
        if (average >= 90) return "A";
        else if (average >= 80) return "B";
        else if (average >= 70) return "C";
        else if (average >= 60) return "D";
        else return "F";
    }

    // Prompt the user to remain or ecit the program.
    Console.WriteLine("Do you want to run the program again? (yes/no)");
    string? runAgainInput = Console.ReadLine()?.Trim().ToLower();
    while (runAgainInput != "yes" && runAgainInput != "no")
    {
        Console.WriteLine("Please enter 'yes' or 'no':");
        runAgainInput = Console.ReadLine()?.Trim().ToLower();
    }
    if (runAgainInput == "no")
    {
        runAgain = false;
    }
    else
    {
        runAgain = true;
    }

    // Method to handle saving data to file.
    static void HandleFileSaving(List<Student> students)
    {
        string filePath = "StudentReport.txt";

        // Read existing students from the file to avoid duplicates
        List<string> existingStudents = new List<string>();
        if (File.Exists(filePath))
        {
            using (StreamReader reader = new StreamReader(filePath))
            {
                // Skip the header lines (assuming the first two lines are headers)
                reader.ReadLine(); // Skip the title line
                reader.ReadLine(); // Skip the separator line
                while (reader.Peek() >= 0)
                {
                    string? line = reader.ReadLine();
                    if (line == null) break; // End of file
                    var lineParts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                    existingStudents.Add(lineParts[0].Trim().ToLower()); // Assuming the first part is the student name
                }
            }
        }

        // Write only new students to the file
        using (StreamWriter writer = new StreamWriter(filePath, append: true))
        {
            foreach (var student in students)
            {
                if (!existingStudents.Contains(student.Name))
                {
                    // arange table hedders and titqle
                    writer.WriteLine($"|{student.Name.PadRight(20)}{student.Score1.ToString().PadRight(20)}{student.Score2.ToString().PadRight(20)}{student.Score3.ToString().PadRight(20)}{student.Average.ToString().PadRight(20)}{GetLetterGrade(student.Average).PadRight(10)}|");
                    existingStudents.Add(student.Name); // Add the student name to the existing students list to prevent future duplicates
                    writer.WriteLine("----------------------------------------------------------------------------------------------------------------");
                }
            }
        }

        Console.WriteLine($"Report saved to {filePath}");
    }
}
