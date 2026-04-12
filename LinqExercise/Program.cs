using System;
using System.Collections.Generic;
using System.Linq;


var people = new List<Person>
    {
        new("Mia",     24, "Berlin",     true,  new List<string>{ "skiing", "gaming", "coffee" }),
        new("Lucas",   31, "Lisbon",     false, new List<string>{ "surfing", "guitar" }),
        new("Aisha",   19, "Toronto",    true,  new List<string>{ "photography", "hiking", "gaming" }),
        new("Noah",    42, "Austin",     true,  new List<string>{ "bbq", "motorcycles", "coffee", "gaming" }),
        new("Sofia",   28, "Barcelona",  false, new List<string>{ "dancing", "yoga" }),
        new("Kai",     22, "Seoul",      true,  new List<string>{ "gaming", "street food", "photography" })
    };
Console.WriteLine("Data ready. Start playing with LINQ!\n");

// // Filter
// var young = people.Where(p => p.Age < 30);
// //Console.WriteLine(string.Join("\n", young.Select(p => $"{p.Name}, {p.Age} years old, from {p.City}")));
// // Project
// var names = people.Select(p => p.Name);
// // foreach(var person in people){
// //     Console.WriteLine(person.Name);
// // }
// // Sort
// var byAge = people.OrderBy(p => p.Age);
// //Console.WriteLine(string.Join("\n", byAge.Select(p => "Name: " + p.Name + " Age: " + p.Age + " City: " + p.City + " Like Coffe: " + p.LikesCoffee + " Hobbie: " + p.Hobbies)));
//
// // Take
// var top3 = people.OrderByDescending(p => p.Age).Take(3);
// Console.WriteLine(string.Join("\n", top3.Select(p => $"{p.Name}, {p.Age} years old, from {p.City}").Take(3)));
//
// // Exists?
// bool anyGamers = people.Any(p => p.Hobbies.Contains("gaming"));
//
// // Count
// int coffeeLovers = people.Count(p => p.LikesCoffee);
//
// // Group
// var byCity = people.GroupBy(p => p.City);
//
// // Most common combo
// var result = people
//     .Where(p => p.Age >= 20 && p.Age <= 35)
//     .Where(p => p.Hobbies.Contains("gaming"))
//     .OrderBy(p => p.Name)
//     .Select(p => $"{p.Name} ({p.City})");

// 1. All people who like coffee
Console.WriteLine("\n===== Question 1 =====");
var likesCoffee = people.Where(p => p.LikesCoffee);
Console.WriteLine(string.Join("\n", likesCoffee.Select(p => $"{p.Name}, {p.Age} years old, from {p.City}").Take(3)));
// 2. Names of people whose city starts with "B"
Console.WriteLine("\n===== Question 2 =====");
var cityStartsWithB = people.Where(p => p.City.StartsWith("B")).Select(p => p.Name);
Console.WriteLine(string.Join("\n", cityStartsWithB));
// 3. Everyone under 25, sorted youngest → oldest
Console.WriteLine("\n===== Question 3 =====");
var age = people.Where(a => a.Age < 25)
    .OrderBy(o => o.Age)
    .Select(s => s.Name);
Console.WriteLine(string.Join("\n", age));
// 4. All hobbies from everyone (one big flat list) (hint: SelectMany)
Console.WriteLine("\n===== Question 4 =====");
var allHobbies = people.SelectMany(p => p.Hobbies);
Console.WriteLine(string.Join("\n", allHobbies));
// 5. People who like both coffee and gaming
Console.WriteLine("\n===== Question 5 =====");
var coffeeAndGaming = people.Where(p => p.LikesCoffee && p.Hobbies.Contains("gaming"));
Console.WriteLine(string.Join("\n", coffeeAndGaming.Select(p => $"{p.Name}, {p.Age} years old, from {p.City}")));
// 6. The person with the most hobbies (name + count)
Console.WriteLine("\n===== Question 6 =====");
var mostHobbies = people.OrderByDescending(p => p.Hobbies.Count).First();
Console.WriteLine($"{mostHobbies.Name} has {mostHobbies.Hobbies.Count()} hobbies.");
// 7. Group by LikesCoffee → show count in each group
Console.WriteLine("\n===== Question 7 =====");
var groupByCoffee = people.GroupBy(p => p.LikesCoffee)
    .Select(g => new { LikesCoffee = g.Key, Count = g.Count() });
foreach (var group in groupByCoffee)
{
    Console.WriteLine($"Likes Coffee: {group.LikesCoffee}, Count: {group.Count}");
}
// 8. Cities that have ≥ 2 people
Console.WriteLine("\n===== Question 8 =====");
var citiesWithAtLeast2People = people.GroupBy(p => p.City)
    .Where(g => g.Count() >= 2)
    .Select(g => g.Key);
Console.WriteLine(string.Join("\n", citiesWithAtLeast2People));
// 9. (bonus evil) People sorted by number of
Console.WriteLine("\n===== Question 9 =====");
var peopleSortedByHobbies = people.OrderByDescending(p => p.Hobbies.Count)
    .Select(p => $"{p.Name} has {p.Hobbies.Count} hobbies.");
Console.WriteLine(string.Join("\n", peopleSortedByHobbies));
class Person
{
    public string Name { get; set; }

    public int Age { get; set; }

    public string City { get; set; }

    public bool LikesCoffee { get; set; }

    public List<string> Hobbies { get; set; } = new List<string>();

    public Person(string name, int age, string city, bool likesCoffee, List<string> hobbies)
    {
        Name = name;
        Age = age;
        City = city;
        LikesCoffee = likesCoffee;
        Hobbies = hobbies;
    }
}