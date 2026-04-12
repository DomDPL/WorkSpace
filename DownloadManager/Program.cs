using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

Console.WriteLine("Download Manager Started\n");

// TODO: Add your async methods and calls here

// Task 1: Create a basic async download method
// Write an async method called DownloadFile that takes two parameters:
// - string filename
// - int seconds (how long the download should take)

// Inside the method:
// - Print "Starting download: {filename}"
// - Wait using Task.Delay (convert seconds to milliseconds)
// - Print "Download completed: {filename}"

// The method should return Task.
// Task 2: Download files sequentially
// From top-level statements, call the following downloads one after another using await:
// 1. "report.pdf" (4 seconds)
// 2. "image.jpg" (6 seconds)
// 3. "data.csv" (3 seconds)
Console.WriteLine("====== Dowloading with delay ======");
async Task DownloadFile(string filename, int seconds)
{
    Console.WriteLine("Starting download: " + filename);
    await Task.Delay(seconds * 1000);
    Console.WriteLine("Download completed: " + filename);
}
await DownloadFile("report.pdf", 4);
await DownloadFile("image.jpg", 6);
await DownloadFile("data.csv", 3);

// After all downloads finish, print "All sequential downloads completed!".
Console.WriteLine("All sequential downloads completed!");


// Task 3: Download files in parallel
// Create three more download tasks:
// - "movie.mp4" (8 seconds)
// - "music.mp3" (5 seconds)
// - "ebook.pdf" (7 seconds)

var download1 = DownloadFile("movie.mp4", 8);
var download2 = DownloadFile("music.mp3", 5);
var download3 = DownloadFile("ebook.pdf", 7);

// Run all three at the same time using Task.WhenAll and await the result.
// Print "All parallel downloads completed!" when done.
await Task.WhenAll(download1, download2, download3);
Console.WriteLine("All parallel downloads completed!");



// Task 4: Async method that returns a value
// Create an async method called GetFileSize that takes a string filename and returns an int (simulated file size in MB).
// Inside the method, wait 2 seconds, then return a random size between 10 and 100.
// Call this method for "video.mp4" and print the returned size.
Console.WriteLine("====== File size ======");
async Task<int> GetFileSize(string filename)
{
    await Task.Delay(2000);
    Random rand = new Random();
    return rand.Next(10, 101);
}

int fileSize = await GetFileSize("video.mp4");
Console.WriteLine($"File size of video.mp4: {fileSize} MB");

// Task 5: Bonus - Show progress simulation
// Modify or create a new async method called DownloadWithProgress that prints a simple progress message every second while downloading.
// Example output: "Downloading report.pdf... 25% complete"
Console.WriteLine("====== Dowloading with progress ======");
async Task DownloadWithProgress(string filename, int seconds)
{
    for (int i = 0; i <= seconds; i++)
    {
        await Task.Delay(1000);
        int percent = (i * 100) / seconds;
        Console.WriteLine("Downloading " + filename + "..." + percent + "% completed");
    }
}
await DownloadWithProgress("pdf", 4);
Console.WriteLine("\nProgram finished.");
Console.ReadLine();