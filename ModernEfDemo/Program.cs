using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;



Console.WriteLine("Modern C# Features Practice Started\n");

// TODO: Complete the 10 activities below
await using var db = new AppDbContext();

// 1.Seed some initial data(Async)
// Create an async method called SeedData that adds 2 categories and 4 products. Save changes asynchronously.
Console.WriteLine("======= Q 1 ======");
async Task SeedData(AppDbContext db)
{
    if (await db.Categories.AnyAsync()) return;
    var electronics = new Category
    {
        Name = "Electronics devices",
    };
    var books = new Category
    {
        Name = "Books"
    };
    // save
    await db.Categories.AddRangeAsync(electronics, books);
    await db.SaveChangesAsync();

    // Products
    await db.Products.AddRangeAsync(
        new Product { Name = "Laptop", Price = 999.99m, CategoryId = electronics.Id },
        new Product { Name = "Phone", Price = 599.99m, CategoryId = electronics.Id },
        new Product { Name = "C# Programming", Price = 39.99m, CategoryId = books.Id },
        new Product { Name = "Clean Code", Price = 45.50m, CategoryId = books.Id }
    );
}
//save
await db.SaveChangesAsync();
Console.WriteLine("Data seeded successfully");
await SeedData(db);
// 2.Get all products asynchronously
// Write an async method to retrieve and print all product names.
Console.WriteLine("======= Q 2 ======");
async Task GetAllProduct(AppDbContext db)
{
    var products = await db.Products.ToListAsync();
    foreach (var product in products)
    {
        Console.WriteLine(product.Name);
    }
}
await db.SaveChangesAsync();
await GetAllProduct(db);

//3.Filter products by price using LINQ
//Find and print all products that cost more than 100 using LINQ and async.
Console.WriteLine("======= Q 3 ======");
async Task GetExpensiveProduct(AppDbContext db)
{
    var product = await db.Products
        .Where(p => p.Price > 100)
        .Select(p => $"Price for {p.Name} = {p.Price:C}")
        .ToListAsync();

    foreach (var p in product)
    {
        Console.WriteLine(p);
    }
}
await GetExpensiveProduct(db);

// 4.Include related data(Category)
// Retrieve all products with their category names using Include and async.
Console.WriteLine("======= Q 4 ======");
async Task ProductsWithCategories(AppDbContext db)
{
    var pWithC = await db.Products
        .Include(c => c.Category)
        .Select(p => $"Ctegory: {p.Category.Name}, Procuts Name: {p.Name}")
        .ToListAsync();

    foreach (var p in pWithC)
    {
        Console.WriteLine(p);
    }
}
await ProductsWithCategories(db);

// 5.Add a new product asynchronously
// Add a new product called "Tablet" priced at 399.99 in the Electronics category.
Console.WriteLine("======= Q 5 ======");
// async Task AddNewProduct(AppDbContext db)
// {
//     var electronics = await db.Categories.FirstAsync(c => c.Name == "Electronics");

//     var tablet = new Product
//     {
//         Name = "Tablet",
//         Price = 399.99m,
//         CategoryId = electronics.Id
//     };

//     db.Products.Add(tablet);
//     await db.SaveChangesAsync();
//     Console.WriteLine("Tablet added successfully.");
// }
//await db.SaveChangesAsync()
//await AddNewProduct(db);
// 6.Update a product price
// Find the product "Phone" and increase its price by 50 using async.
Console.WriteLine("======= Q 6 ======");
async Task UpdatePhonePrice(AppDbContext db)
{
    var getPhone = await db.Products
        .FirstOrDefaultAsync(p => p.Name == "Phone");

    if (getPhone is not null)
    {
        getPhone.Price += 50;
        Console.WriteLine($"Updated price for Phone: ${getPhone.Price}");
    }
}
await db.SaveChangesAsync();
await UpdatePhonePrice(db);
// 7.Delete a product
// Delete the product named "Clean Code" using async.
Console.WriteLine("======= Q 7 ======");
async Task DeleteCleanCode(AppDbContext db)
{
    var getCleanCode = await db.Products
        .FirstOrDefaultAsync(p => p.Name == "Clean Code");

    if (getCleanCode is not null)
    {
        db.Products.Remove(getCleanCode);
    }
}
await db.SaveChangesAsync();
await DeleteCleanCode(db);
Console.WriteLine("Clean Code Deleted successfully.");

// 8.Count products per category using LINQ
// Show how many products are in each category.
Console.WriteLine("======= Q 8 ======");
async Task CountProductsPerCategory(AppDbContext db)
{
    var counts = await db.Categories
        .Select(c => new
        {
            Category = c.Name,
            Count = c.Products.Count
        })
        .ToListAsync();

    foreach (var item in counts)
    {
        Console.WriteLine($"{item.Category}: {item.Count} products");
    }
}
await CountProductsPerCategory(db);

// 9.Find the most expensive product
// Use LINQ to find and display the most expensive product.
Console.WriteLine("======= Q 9 ======");
async Task GetMostExpensiveProduct(AppDbContext db)
{
    var expensive = await db.Products
        .OrderByDescending(p => p.Price)
        .FirstOrDefaultAsync();

    if (expensive != null)
    {
        Console.WriteLine($"Most expensive: {expensive.Name} - ${expensive.Price}");
    }
}
await GetMostExpensiveProduct(db);

// 10.Combine everything in Main flow
// In your top-level statements, call all the above methods in order using await.
Console.WriteLine("======= Q 10 ======");
Console.WriteLine("\nAll activities completed. Press ENTER to exit...");
Console.ReadLine();


class Category
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public List<Product> Products { get; set; } = new();
}
class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int CategoryId { get; set; }
    public Category? Category { get; set; }
}

class AppDbContext : DbContext
{
    public DbSet<Product> Products => Set<Product>();
    public DbSet<Category> Categories => Set<Category>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseInMemoryDatabase("ModernDemoDb");
    }
}