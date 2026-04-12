using System;
using System.Collections.Generic;
using System.Linq;

var people = new List<Person>
{
    new("Mia",    24, "Berlin",   true,  3),
    new("Lucas",  31, "Lisbon",   false, 2),
    new("Aisha",  19, "Toronto",  true,  5),
    new("Noah",   42, "Austin",   true,  4),
    new("Sofia",  28, "Barcelona",false, 2),
    new("Kai",    22, "Seoul",    true,  3)
};

Console.WriteLine("LINQ + Lambda playground ready\n");

// Your code goes here

//1.All people who like coffee
//Print their names.
Console.WriteLine("====== Q 1 ======");
var coffeeLovers = people.Where(p => p.LikesCoffee);
foreach (var person in coffeeLovers)
{
    Console.WriteLine(person.Name);
}
//2.Names of people under 25
//Sorted from youngest to oldest.
Console.WriteLine("====== Q 2 ======");
var under25 = people.Where(p => p.Age < 25).OrderBy(p => p.Age);
foreach (var person in under25)
{
    Console.WriteLine(person.Name);
}

//3.Anyone from Berlin?
//Print "Yes" or "No".
Console.WriteLine("====== Q 3 ======");
var anyoneFromBerlin = people.Any(p => p.City == "Berlin");
Console.WriteLine(anyoneFromBerlin ? "Yes" : "No");
//4.How many people have 3+ hobbies?
//Just print the number.
Console.WriteLine("====== Q 4 ======");
var countHobbyists = people.Count(p => p.HobbiesCount >= 3);
Console.WriteLine(countHobbyists);
//5.Create short descriptions
//Format like: Mia(24) – Berlin – ☕ yes
Console.WriteLine("====== Q 5 ======");
var descriptions = people.Select(p => $"{p.Name}({p.Age}) – {p.City} – ☕ {(p.LikesCoffee ? "yes" : "no")}");
foreach (var desc in descriptions)
{
    Console.WriteLine(desc);
}
//6.People sorted by name length (longest first)
Console.WriteLine("====== Q 6 ======");
var sortedByNameLength = people.OrderByDescending(p => p.Name.Length);
foreach (var person in sortedByNameLength)
{
    Console.WriteLine(person.Name);
}
//7.Bonus – combined filter
//People who are 20–30 years old AND like coffee, sorted by age.
Console.WriteLine("====== Q 7 ======");
var combinedFilter = people.Where(p => p.Age >= 20 && p.Age <= 30 && p.LikesCoffee).OrderBy(p => p.Age);
foreach (var person in combinedFilter)
{
    Console.WriteLine($"{person.Name} ({person.Age})");
}
//8.Free play ideas(pick any)
// • Count people from Europe (Berlin, Lisbon, Barcelona)
// • Find if anyone has more than 4 hobbies
// • Show names in uppercase
// • List cities (without duplicates) – hint: .Select(p => p.City).Distinct()
Console.WriteLine("====== Q 8 ======");
var europeans = people.Count(p => p.City == "Berlin" || p.City == "Lisbon" || p.City == "Barcelona");
Console.WriteLine($"People from Europe: {europeans}");
var anyoneWithManyHobbies = people.Any(p => p.HobbiesCount > 4);
Console.WriteLine($"Anyone with more than 4 hobbies: {(anyoneWithManyHobbies ? "Yes" : "No")}");
var namesInUppercase = people.Select(p => p.Name.ToUpper());
Console.WriteLine("Names in uppercase:");
foreach (var name in namesInUppercase)
{
    Console.WriteLine(name);
}
var distinctCities = people.Select(p => p.City).Distinct();
Console.WriteLine("Cities (without duplicates):");
foreach (var city in distinctCities)
{
    Console.WriteLine(city);
}
Console.WriteLine("\nDone. Press ENTER to close...");
Console.ReadLine();

record Person(string Name, int Age, string City, bool LikesCoffee, int HobbiesCount);