using System;

Console.WriteLine("Value Types vs Reference Types Activity\n");

// TODO: Complete the 10 activities below
// 1.Value Type - int
// Create two int variables. Assign the first to the second, then change the second. Print both values. What do you observe?
// Solution
Console.WriteLine("===== Q 1 =====\n");
int x = 50;
int y = x;      // Copy is made
y = 100;

Console.WriteLine($"x = {x}");   // 50
Console.WriteLine($"y = {y}");   // 100
// 2.Value Type - struct
// Create a simple struct Point with two int fields: X and Y.Create two instances, assign one to the other, modify the second, and print both.
// Solution
Console.WriteLine("===== Q 2 =====\n");
Point p1 = new Point { X = 10, Y = 20 };
Point p2 = p1;           // Copy of the struct
p2.X = 99;
Console.WriteLine($"p1: ({p1.X}, {p1.Y})");   // (10, 20)
Console.WriteLine($"p2: ({p2.X}, {p2.Y})");   // (99, 20)

// 3.Reference Type - class
// Create a simple class Student with a string Name property. Create two instances, assign one to the other, change the name on the second, and print both names.
// Solution
Console.WriteLine("===== Q 3 =====\n");
Student s1 = new Student { Name = "Alice" };
Student s2 = s1;           // Both point to same object
s2.Name = "Bob";

Console.WriteLine($"s1.Name = {s1.Name}");   // Bob
Console.WriteLine($"s2.Name = {s2.Name}");   // Bob

// 4.Reference Type - string
// Strings are reference types but behave specially (immutable). Create two string variables, assign one to the other, then reassign the second. Print both.
// Solution
Console.WriteLine("===== Q 4 =====\n");
string str1 = "Hello";
string str2 = str1;
str2 = "World";

Console.WriteLine(str1);   // Hello
Console.WriteLine(str2);   // World

// 5.Passing Value Type to Method
// Create a method that takes an int parameter and modifies it. Call the method and show that the original variable is unchanged.
// Solution
Console.WriteLine("===== Q 5 =====\n");
void ModifyValue(int num)
{
    num = num + 100;
    Console.WriteLine($"Inside method: {num}");
}

int original = 10;
ModifyValue(original);
Console.WriteLine($"Original after method: {original}");   // 10

// 6.Passing Reference Type to Method
// Create a method that takes a Student parameter and changes its Name. Show that the original object is modified.
// Solution
Console.WriteLine("===== Q 6 =====\n");
void ModifyStudent(Student student)
{
    student.Name = "Modified Name";
}

Student student1 = new Student { Name = "Original" };
ModifyStudent(student1);
Console.WriteLine(student1.Name);   // Modified Name

// 7.Array is a Reference Type
// Create an int[] array, assign it to another variable, modify an element in the second array, and show both arrays reflect the change.
// Solution
Console.WriteLine("===== Q 7 =====\n");
int[] arr1 = { 1, 2, 3 }
;
int[] arr2 = arr1;
arr2[0] = 99;

Console.WriteLine(arr1[0]);   // 99
Console.WriteLine(arr2[0]);   // 99

// 8.Create your own struct
// Create a struct called Rectangle with Width and Height. Demonstrate that assigning one to another creates a copy.
// Solution
Console.WriteLine("===== Q 8 =====\n");
Rectangle r1 = new Rectangle { Width = 10, Height = 20 };
Rectangle r2 = r1;
r2.Width = 50;

Console.WriteLine($"r1 Width: {r1.Width}");   // 10
Console.WriteLine($"r2 Width: {r2.Width}");   // 50

// 9.Create your own class
// Create a class called Car with a string Model property. Demonstrate reference behavior.
// Solution
Console.WriteLine("===== Q 9 =====\n");
Car car1 = new Car { Model = "Toyota" };
Car car2 = car1;
car2.Model = "Honda";

Console.WriteLine(car1.Model);   // Honda

// 10.Summary Question
// In your own words, explain why modifying a struct does not affect the original variable, but modifying a class does. Write your explanation as comments in the code.
// Solution
// Value types (like structs and primitives) store the actual data.
// When you assign them, a full copy is created.
// Reference types (like classes) store a reference to the data on the heap.
// When you assign them, only the reference is copied, so both variables point to the same object.

Console.WriteLine("\nActivity completed. Press ENTER to exit...");
Console.ReadLine();
class Car
{
    public string? Model { get; set; }
}

struct Rectangle
{
    public int Width;
    public int Height;
}

class Student
{
    public string? Name { get; set; }
}




struct Point
{
    public int X;
    public int Y;
}

