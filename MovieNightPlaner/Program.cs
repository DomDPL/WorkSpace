// 1 Ask the user how many movies they want to add (between 1 and 6).

// 2 For each movie, ask for:
// Movie Title (string)
// Rating (integer from 1 to 10)
// Genre (string, e.g. Action, Comedy, Horror, Sci-Fi)

// 3 After entering all movies, display:
// A clean list of all movies with their title, rating, and genre
// The highest rated movie (title + rating)
// The average rating of all movies (rounded to 1 decimal place)
// How many movies were entered per genre (simple count)

// 4 Ask the user if they want to run the program again (yes/no).
using System;
class Program
{
    static void Main()
    {
        bool runProgram = true;
        while (runProgram)
        {
            Console.Write("How many movies do you want to add (1-6)? ");
            int movieCount;
            while (!int.TryParse(Console.ReadLine(), out movieCount) || movieCount < 1 || movieCount > 6)
            {
                Console.Write("Please enter a valid number between 1 and 6: ");
            }
            string[] titles = new string[movieCount];
            int[] ratings = new int[movieCount];
            string[] genres = new string[movieCount];
            for (int i = 0; i < movieCount; i++)
            {
                Console.Write($"Enter title for movie #{i + 1}: ");
                titles[i] = Console.ReadLine() ?? string.Empty;
                Console.Write($"Enter rating for movie #{i + 1} (1-10): ");
                while (!int.TryParse(Console.ReadLine(), out ratings[i]) || ratings[i] < 1 || ratings[i] > 10)
                {
                    Console.Write("Please enter a valid rating between 1 and 10: ");
                }
                Console.Write($"Enter genre for movie #{i + 1}: ");
                genres[i] = Console.ReadLine() ?? string.Empty;
            }
            Console.WriteLine("\nMovies Entered:");
            for (int i = 0; i < movieCount; i++)
            {
                Console.WriteLine($"{i + 1}. {titles[i]} - Rating: {ratings[i]} - Genre: {genres[i]}");
            }
            int highestRatingIndex = FindHighestRatedMovie(ratings); Console.WriteLine($"\nHighest Rated Movie: {titles[highestRatingIndex]} with a rating of {ratings[highestRatingIndex]}"); double averageRating = CalculateAverageRating(ratings); Console.WriteLine($"Average Rating: {averageRating:F1}"); CountGenres(genres); Console.Write("\nDo you want to run the program again? (yes/no): ");
            string userResponse = Console.ReadLine()?.ToLower() ?? "no";
            runProgram = userResponse == "yes";
            Console.WriteLine();
        }
    }
    static int FindHighestRatedMovie(int[] ratings)
    {
        int highestIndex = 0;
        for (int i = 1; i < ratings.Length; i++)
        {
            if (ratings[i] > ratings[highestIndex])
            {
                highestIndex = i;
            }
        }
        return highestIndex;
    }
    static double CalculateAverageRating(int[] ratings)
    {
        int sum = 0;
        for (int i = 0; i < ratings.Length; i++)
        {
            sum += ratings[i];
        }
        return (double)sum / ratings.Length;
    }
    static void CountGenres(string[] genres)
    {
        var genreCount = new System.Collections.Generic.Dictionary<string, int>();
        for (int i = 0; i < genres.Length; i++)
        {
            if (genreCount.ContainsKey(genres[i]))
            {
                genreCount[genres[i]]++;
            }
            else
            {
                genreCount[genres[i]] = 1;
            }
        }
        Console.WriteLine("\nGenre Counts:");
        foreach (var genre in genreCount)
        {
            Console.WriteLine($"{genre.Key}: {genre.Value}");
        }
    }
}
// ✅ Minimum Requirements 
// Use dotnet new console to create the project in VS Code
// Use arrays to store the movie data
// Validate that the rating is between 1 and 10
// Use at least one method (e.g. to find the highest rated movie)
// Display output in a neat, readable format
// Program should handle normal use without crashing
// 💡 Helpful Hints:
// You can use 3 separate arrays: one for titles, one for ratings, and one for genres
// Use double for the average rating
// Use a for loop to collect movie information
// For genre counting, you can start simple (just show the count for each entered genre)
// Format ratings nicely using string interpolation