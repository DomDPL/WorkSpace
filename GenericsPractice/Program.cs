using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;

Console.WriteLine("Generics Practice Started\n");

// Your code for the 20 activities goes here

//Section 1: 5 Activities with Generic List<T>
//1. Create a List<int> called numbers and add the values 10, 20, 30, 40, 50. Print all numbers.
Console.WriteLine("======= Q 1 =======");
List<int> numbers = new List<int>();
numbers.Add(10);
numbers.Add(20);
numbers.Add(30);
numbers.Add(40);
numbers.Add(50);

foreach (var number in numbers)
{
    Console.WriteLine(number);
}
Console.WriteLine();

//2.Create a List<string> called names. Add five names, then print how many names are in the list using .Count.
Console.WriteLine("======= Q 2 =======");
List<string> names = new List<string>();
names.Add("Chad");
names.Add("Val");
names.Add("Jim");
names.Add("Dominic");
names.Add("Malika");

foreach (var name in names)
{
    Console.WriteLine(name);
}
//3.Create a List<double> called prices. Add 5.99, 12.50, 3.75. Remove the first item and print the remaining prices.
Console.WriteLine("======= Q 3 =======");
List<double> prices = new List<double>();
prices.Add(5.99);
prices.Add(12.50);
prices.Add(3.75);

prices.Remove(prices[0]);

foreach (var price in prices)
{
    Console.WriteLine(price);
}
Console.WriteLine();

//4.Create a List<bool> called flags and add true, false, true. Check if the list contains false and print the result.
Console.WriteLine("======= Q 4 =======");
List<bool> flags = new List<bool>();
flags.Add(true);
flags.Add(false);
flags.Add(true);

var check = flags.Contains(false);
Console.WriteLine(check);

Console.WriteLine();


//5.Create a List<int> called scores. Add 85, 92, 78. Sort the list and print the sorted values.
Console.WriteLine("======= Q 5 =======");
List<int> scores = new List<int>
{
    85, 95, 78
};
scores.Sort();
foreach (var score in scores)
{

    Console.WriteLine(score);
}
Console.WriteLine();

//6. Create a Stack<int> called numbersStack. Push 100, 200, 300. Pop one value and print it.
Console.WriteLine("======= Q 6 =======");
Stack<int> numbersStack = new Stack<int>();
numbersStack.Push(100);
numbersStack.Push(200);
numbersStack.Push(300);

int popLastIn = numbersStack.Pop();
Console.WriteLine($"Removed: {popLastIn}");
Console.WriteLine();

//7.Create a Stack<string> called undoStack. Push three actions ("save", "edit", "delete"). Use Peek to see the top without removing it.
Console.WriteLine("======= Q 7 =======");
Stack<string> undoStack = new Stack<string>();
undoStack.Push("save");
undoStack.Push("edit");
undoStack.Push("delete");

string peekLastIn = undoStack.Peek();
Console.WriteLine($"Peek the top: {peekLastIn}");
Console.WriteLine();
//8.Create a Stack<char> called letters. Push A, B, C, D.Pop all items and print them (they should come out in reverse order).
// =======================================================================
Console.WriteLine("======= Q 8 =======");
Stack<char> letters = new Stack<char>();
letters.Push('A');
letters.Push('B');
letters.Push('C');
letters.Push('D');

do
{
    var remove = letters.Pop();
    Console.WriteLine($"Peek the top: {remove}");
} while (letters.Count > 0);
Console.WriteLine();

//9.Create a Stack<int> called tempStack. Push 5 numbers.Print the count of items in the stack.
Console.WriteLine("======= Q 9 =======");
Stack<int> tempStack = new Stack<int>();
tempStack.Push(100);
tempStack.Push(200);
tempStack.Push(300);
tempStack.Push(400);
tempStack.Push(500);

int countNums = tempStack.Count();
Console.WriteLine($"number count is : {countNums}");
Console.WriteLine();

//10.Create a Stack<string> called browserHistory. Push three URLs.Pop one and print "Navigated back from" + the URL.
Console.WriteLine("======= Q 10 =======");
Stack<string> browserHistory = new Stack<string>();
browserHistory.Push("URL1");
browserHistory.Push("URL2");
browserHistory.Push("URL3");

string history = browserHistory.Pop();
Console.WriteLine($"Navigated back from: {history}");
Console.WriteLine();

//11. Create a Queue<string> called orderQueue. Enqueue three customer names. Dequeue one and print it.
Console.WriteLine("======= Q 11 =======");
Queue<string> orderQueue = new Queue<string>();
orderQueue.Enqueue("Diane");
orderQueue.Enqueue("Jane");
orderQueue.Enqueue("Emily");

string firstInFirstOut = orderQueue.Dequeue();
Console.WriteLine($"First in firts out, the firtst person was: {firstInFirstOut}");
Console.WriteLine();

//12.Create a Queue<int> called ticketQueue. Enqueue ticket numbers 101 to 105. Use Peek to see the next ticket without removing it.
Console.WriteLine("======= Q 12 =======");
Queue<int> ticketQueue = new Queue<int>();
ticketQueue.Enqueue(101);
ticketQueue.Enqueue(102);
ticketQueue.Enqueue(103);
ticketQueue.Enqueue(104);
ticketQueue.Enqueue(105);

int RemoveFirstInFirstOut = ticketQueue.Peek();

Console.WriteLine($"First in firts out, the firtst person was: {RemoveFirstInFirstOut}");
Console.WriteLine();

//13.Create a Queue<string> called tasks. Enqueue "Task1", "Task2", "Task3". Dequeue all items in a loop and print them.
// =======================================================================
Console.WriteLine("======= Q 13 =======");
Queue<string> tasks = new Queue<string>();
tasks.Enqueue("Task1");
tasks.Enqueue("Task2");
tasks.Enqueue("Task3");

string task = tasks.Peek();

do
{
    var remove = tasks.Dequeue();
    Console.WriteLine($"Peek the top: {remove}");
} while (tasks.Count > 0);
Console.WriteLine();
Console.WriteLine();

//14.Create a Queue<bool> called flagsQueue. Enqueue true, false, true. Print the count and the first item using Peek.
Console.WriteLine("======= Q 14 =======");
Queue<bool> flagsQueue = new Queue<bool>();
flagsQueue.Enqueue(true);
flagsQueue.Enqueue(false);
flagsQueue.Enqueue(true);

var check2 = flagsQueue.Peek();
var count3 = flagsQueue.Count();
Console.WriteLine(count3);
Console.WriteLine(check2);

Console.WriteLine();

//15.Create a Queue<double> called payments. Enqueue three amounts.Dequeue one and print "Processed payment of" + amount.
Console.WriteLine("======= Q 15 =======");
Queue<double> payments = new Queue<double>();
payments.Enqueue(1200);
payments.Enqueue(1300);
payments.Enqueue(1400);

var amount = payments.Dequeue();
Console.WriteLine("Processed payment of: " + amount);

Console.WriteLine();
//16. Create a generic class Message<T> with a property T Content and a constructor that sets it. Create a Message<string> and print its content.
Console.WriteLine("======= Q 16 =======");
// Usage
Message<string> massage = new Message<string>("This is the message.");
Console.WriteLine(massage.Content);

//17.Create a Message<int> with value 42 and print it.
Console.WriteLine("======= Q 17 =======");
Message<int> numbersGT = new Message<int>(42);
Console.WriteLine(numbersGT.Content);

//18.Create a Message<bool> with value true and print "Status:" + value.
Console.WriteLine("======= Q 18 =======");
Message<bool> trueFalse = new Message<bool>(true);
Console.WriteLine(trueFalse.Content);

//19.Create a Message<double> with value 99.99 and print it with a label.
Console.WriteLine("======= Q 19 =======");
Message<double> doubleNum = new Message<double>(99.99);
Console.WriteLine(doubleNum.Content);

//20.Create a List<Message<string>> and add two messages. Loop through the list and print each content.
Console.WriteLine("======= Q 20 =======");
List<Message<string>> twoMesages = new List<Message<string>>();
var message1 = new Message<string>("first message");
var message2 = new Message<string>("second message");

twoMesages.Add(message1);
twoMesages.Add(message2);

foreach (var twoM in twoMesages)
{
    Console.WriteLine(twoM.Content);
}

Console.WriteLine("\nAll activities completed. Press ENTER to exit...");
Console.ReadLine();
// Add this class before the top-level code
class Message<T>
{
    public T Content { get; set; }

    public Message(T content)
    {
        Content = content;
    }
}