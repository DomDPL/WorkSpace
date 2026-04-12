using System;
using System.Collections.Generic;
using System.Linq;


var students = new List<Student>
{
    new Student { Id = 1, Name = "Alice Smith", Age = 20, Grade = "A" },
    new Student { Id = 2, Name = "Bob Johnson", Age = 19, Grade = "B" },
    new Student { Id = 3, Name = "Charlie Brown", Age = 21, Grade = "C" },
    new Student { Id = 4, Name = "Diana Prince", Age = 20, Grade = "A" },
    new Student { Id = 5, Name = "Ethan Hunt", Age = 22, Grade = "B" }
};
// TODO: Complete the 5 activities below

// 5 Easy Activities
// Activity 1: List all students
// Print the name and grade of every student.
Console.WriteLine("\n==== Q 1 ====");
var all = students.Select(s => $"{s.Name}, {s.Grade}");
foreach (var s in all)
{
    Console.WriteLine(s);
}
// Activity 2: Show students with grade "A"
// Display only the names of students who have grade "A".
Console.WriteLine("\n==== Q 2 ====");
var gradeA = students.Where(s => s.Grade == "A").Select(s => $"{s.Name}, {s.Grade}");
foreach (var s in gradeA)
{
    Console.WriteLine(s);
}

// Activity 3: Add a new student
// Add a new student: Name = "Fiona Green", Age = 19, Grade = "B".
Console.WriteLine("\n==== Q 3 ====");
var newStudent = new Student
{
    Name = "Fiona Green",
    Age = 19,
    Grade = "B"
};
students.Add(newStudent);
Console.WriteLine("Studetn added successfully.");

// Activity 4: Update a student's age
// Change Bob Johnson's age to 20.
Console.WriteLine("\n==== Q 4 ====");
var updadeAge = students.FirstOrDefault(f => f.Name == "Bob Johnson");
if (updadeAge is not null)
{
    updadeAge.Age = 20;
}
Console.WriteLine("Age for Bob Updated to 20");
// Activity 5: Count students older than 20
// Print how many students are older than 20 years old.
Console.WriteLine("\n==== Q 5 ====");
var count = students.Where(s => s.Age > 20);

Console.WriteLine(count.Count());

class Student
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Age { get; set; }
    public string Grade { get; set; } = string.Empty;

    public Student()
    {

    }
}