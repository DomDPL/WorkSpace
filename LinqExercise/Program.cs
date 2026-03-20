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

    // Filter
    var young = people.Where(p => p.Age < 30);
    //Console.WriteLine(string.Join("\n", young.Select(p => $"{p.Name}, {p.Age} years old, from {p.City}")));
    // Project
    var names = people.Select(p => p.Name);
    // foreach(var person in people){
    //     Console.WriteLine(person.Name);
    // }
    // Sort
    var byAge = people.OrderBy(p => p.Age);
    //Console.WriteLine(string.Join("\n", byAge.Select(p => "Name: " + p.Name + " Age: " + p.Age + " City: " + p.City + " Like Coffe: " + p.LikesCoffee + " Hobbie: " + p.Hobbies)));

    // Take
    var top3 = people.OrderByDescending(p => p.Age).Take(3);
    Console.WriteLine(string.Join("\n", top3.Select(p => $"{p.Name}, {p.Age} years old, from {p.City}").Take(3)));

    // Exists?
    bool anyGamers = people.Any(p => p.Hobbies.Contains("gaming"));

    // Count
    int coffeeLovers = people.Count(p => p.LikesCoffee);

    // Group
    var byCity = people.GroupBy(p => p.City);

    // Most common combo
    var result = people
        .Where(p => p.Age >= 20 && p.Age <= 35)
        .Where(p => p.Hobbies.Contains("gaming"))
        .OrderBy(p => p.Name)
        .Select(p => $"{p.Name} ({p.City})");

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