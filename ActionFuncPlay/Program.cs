using System;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            SayHello("Val");
            Action<string> sayHello = name => Console.WriteLine("Hello " + name);
            sayHello("Cooper");

            var showNum = (int n) => Console.WriteLine("the number is " + n);
            showNum(40);

            Func<int, int, int> add = (a, b) => a + b;
            Func<int, int, int> multiply = (a, b) => a * b;
            Console.WriteLine(add(5, 9));
            Console.WriteLine(multiply(5, 9));

            DoTwice( () => Console.WriteLine("Hi"));
            DoTwice( () => Console.Beep());

            DoTwice(() => DoTwice(() => Console.WriteLine("Repeat me 4 times.")));

            Console.WriteLine(Calculate(5, 8, (x, y) => x / y));
        }
        static int Calculate(int a, int b, Func<int, int, int> operation){
            return operation(a,b);
        }
        static void SayHello(string name)
        {
            Console.WriteLine("Hello," + name);

        }
        static void DoTwice(Action action)
        {
            action();
            action();
        }
    }
}