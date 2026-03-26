using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

const string FilePath = "disney_movies.json.txt";

var movies = new List<Movie>
{
    new Movie { Title = "Tangled", Year = 2010, Rating = 7.7 },
    new Movie { Title = "Frozen", Year = 2013, Rating = 7.4 },
    new Movie { Title = "The Little Mermaid", Year = 1989, Rating = 7.6 }
};
// TODO: Complete the 4 activities below

// Activity 1: Save movies synchronously
// Write a method called SaveMoviesSync() that saves the movies list to "disney_movies.json" using synchronous file operations.
Console.WriteLine("======= Q 1 ======");
void SaveMoviesSync()
{
    string serializeJson = JsonSerializer.Serialize(movies, new JsonSerializerOptions { WriteIndented = true });
    File.WriteAllText(FilePath, serializeJson);
    Console.WriteLine("Movies saved synchronously.");
}

SaveMoviesSync();

// Activity 2: Load movies synchronously
// Write a method called LoadMoviesSync() that reads the movies from "disney_movies.json" and prints them.
Console.WriteLine("======= Q 2 ======");
void LoadMoviesSync()
{
    if (!File.Exists(FilePath))
    {
        Console.WriteLine("No file found.");
        return;
    }

    string json = File.ReadAllText(FilePath);
    var loadedMovies = JsonSerializer.Deserialize<List<Movie>>(json);

    Console.WriteLine("Movies loaded synchronously:");
    foreach (var m in loadedMovies ?? new List<Movie>())
    {
        Console.WriteLine($"{m.Title} ({m.Year}) - Rating: {m.Rating}");
    }
}
LoadMoviesSync();

// Activity 3: Save movies asynchronously
// Write a method called SaveMoviesAsync() that saves the movies using await and async file operations.
Console.WriteLine("======= Q 3 ======");
async Task SaveMoviesAsync()
{
    string json = JsonSerializer.Serialize(movies, new JsonSerializerOptions { WriteIndented = true });
    await File.WriteAllTextAsync(FilePath, json);
    Console.WriteLine("Movies saved asynchronously.");
}
await SaveMoviesAsync();
// Activity 4: Load movies asynchronously
// Write a method called LoadMoviesAsync() that loads and displays the movies using await.
Console.WriteLine("======= Q 4 ======");
async Task LoadMoviesAsync()
{
    if (!File.Exists(FilePath))
    {
        Console.WriteLine("No file found.");
        return;
    }

    string json = await File.ReadAllTextAsync(FilePath);
    var loadedMovies = JsonSerializer.Deserialize<List<Movie>>(json);

    Console.WriteLine("Movies loaded asynchronously:");
    foreach (var m in loadedMovies ?? new List<Movie>())
    {
        Console.WriteLine($"{m.Title} ({m.Year}) - Rating: {m.Rating}");
    }
}
await LoadMoviesAsync();

class Movie
{
    public string Title { get; set; } = string.Empty;
    public int Year { get; set; }
    public double Rating { get; set; }
}

