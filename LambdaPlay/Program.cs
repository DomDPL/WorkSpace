using System;

Console.WriteLine("Lambda playground started\n");

// 1. Create a lambda that takes one number and returns double that number.
//Print the result for 5, 12 and 100.

Func<int, int> square = (a) => a * 2;

Console.WriteLine(square(5));

// 2. Say something nice
//Create an Action<string> lambda that prints
//You're awesome, {name}!
//Call it with three different names.
Action<string> greate = name => Console.WriteLine("You are awesome, " + name);
greate("Dominic.");


// 3. Create a lambda Func<int, bool> that returns true if a number is even.
//Test it with 4, 7, 0, 13.

Func<int, bool> evenNumbers = n => n % 2 == 0; // I can also use Predicate for anything to return true or false.
Console.WriteLine(evenNumbers(4));
Console.WriteLine(evenNumbers(7));
Console.WriteLine(evenNumbers(0));
Console.WriteLine(evenNumbers(13));

//4. Run something twice
//Write a small method void Twice(Action action) that calls the action twice.
//Use it with two different lambdas.
// Action action => Console.WriteLine("I am called. ");
Twice(() => Console.WriteLine("I am called. "));

static void Twice(Action action)
{
    action();
    action();
}

//5. Simple math picker
//Write a method

//6. Greeting depending on hour
//Create a Func<string> (no parameters) that returns
//• "Morning!" before noon
//• "Afternoon!" between 12–17
//• "Evening!" after 17

// call
Func<string> greet = Greeting();
Console.WriteLine(greet());
// function.
static Func<string> Greeting()
{
    return () => DateTime.Now.Hour < 12 ? "Morning!" :
        DateTime.Now.Hour < 17 ? "Afternoon!" :
        "Evening!";
}


//7. Bonus mini-challenges (pick any)
//• Lambda that returns square of a number

static Func<double> SquareNumber(double number)
{
    return () => number * number;
}
// implement.
Func<double> sqrt = SquareNumber(5);
Console.WriteLine(sqrt());


//• Lambda that returns "big" / "small" depending on number > 100
static Func<string> Check(double number1)
{
    return () => number1 >= 100 ? "big" : "small";
}
Func<string> check = Check(10);
Console.WriteLine(check());


//• Action that prints a star line: ****
static Action Print()
{
    return () => Console.WriteLine("****");
}
Print()(); // wierd code. I have to call the method first to get the lambda, then call the lambda to print the stars.
//• Method void ThreeTimes(Action a) that calls action 3×

//• Func<double, string> that formats money ($12.50)

Console.WriteLine("\nDone. Press ENTER to close...");
Console.ReadLine();

//Quick lambda cheat sheet
//------------------------------------------------------------------------------
// Looks like	                                Meaning
// x => x * 2	                                take x, return x×2
// () => "hi"	                                no input, return "hi"
// (a,b) => a + b	                            two parameters
// n => n % 2 == 0	                            one-line condition (returns bool)
// name => Console.WriteLine(name)	            Action (no return)
//------------------------------------------------------------------------------