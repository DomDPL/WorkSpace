using Microsoft.EntityFrameworkCore;

var context = new BookContext();

// TODO: Complete the 4 activities below

// Activity 1: Add books synchronously
// Write a method called AddBooksSync() that adds three books to the database using synchronous EF Core methods.
Console.WriteLine("====== Q 1 ======");
void AddBooksSync()
{
    var addBook1 = new Book
    {
        Title = "title1",
        Author = "Author1",
        Year = 1111
    };
    context.Books.Add(addBook1);
    var addBook2 = new Book
    {
        Title = "title2",
        Author = "Author2",
        Year = 2222
    };
    context.Books.Add(addBook2);
    var addBook3 = new Book
    {
        Title = "title3",
        Author = "Author3",
        Year = 3333
    };
    context.Books.Add(addBook3);
    context.SaveChanges();
}
AddBooksSync(); // methos must be called to run the content in it.
Console.WriteLine("3 Books added successfully.");

// Activity 2: Load books synchronously
// Write a method called LoadBooksSync() that retrieves all books and prints them.
Console.WriteLine("====== Q 2 ======");
void LoadBooksSync()
{
    var data = context.Books
    .ToList();

    if (data != null)
    {
        foreach (var d in data)
        {
            Console.WriteLine($"Title: {d.Title}, Author: {d.Author}, Year: {d.Year}\n");
        }
    }
}
LoadBooksSync();

// Activity 3: Add books asynchronously
// Write a method called AddBooksAsync() that adds books using await and async EF Core methods.
Console.WriteLine("====== Q 3 ======");
async Task AddBooksAsync()
{
    var addBooksAsync = new List<Book>
    {
        new Book
        {
            Title = "Title4",
            Author = "Author4",
            Year = 4444
        },
        new Book
        {
            Title = "Title5",
            Author = "Author5",
            Year = 5555
        }
    };
    await context.Books.AddRangeAsync(addBooksAsync);
    await context.SaveChangesAsync();
}
await AddBooksAsync();
Console.WriteLine("Book using list added successfully.");

// Activity 4: Load books asynchronously
// Write a method called LoadBooksAsync() that retrieves and displays books using await.
Console.WriteLine("====== Q 4 ======");
async Task LoadBooksAsync()
{
    var data = await context.Books
        .ToListAsync();

    if (data != null)
    {
        foreach (var d in data)
        {
            Console.WriteLine($"Title: {d.Title}, Author: {d.Author}, Year: {d.Year}\n");
        }
    }
}
await LoadBooksAsync();
public class Book
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public int Year { get; set; }
}

public class BookContext : DbContext
{
    public DbSet<Book> Books => Set<Book>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseInMemoryDatabase("BooksDb");
    }
}
