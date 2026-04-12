// See https://aka.ms/new-console-template for more information

using MathConsole;

Console.WriteLine("Enter two numbers for addition");
Console.WriteLine("Enter first numbers");
var Number1 = int.Parse(Console.ReadLine());

Console.WriteLine("Enter Your second Number");
var Number2 = int.Parse(Console.ReadLine());

MathService service = new MathService();
var answer = service.Add(Number1, Number2);
Console.WriteLine(answer);
