using Microsoft.EntityFrameworkCore;

var context = new ShopContext();
SeedData(context);

Console.WriteLine("eCommerce Database is ready!\n");

// TODO: Write your 5 easy activities here

//Activity 1: List all products
//Show the name, price, and stock of every product in the database.
Console.WriteLine("====== Question 1 =======");
var listAllProducts = context.Products
    .Select(p => $"Name: {p.Name}\nPrice: {p.Price}\nStock: {p.Stock}\n")
    .ToList();

foreach (var item in listAllProducts)
{
    Console.WriteLine(item);
}
//Activity 2: Show only Electronics products
//Display the name and price of all products in the "Electronics" category.
Console.WriteLine("====== Question 2 =======");
var electronics = context.Products
    .Where(e => e.Category == "Electronics");

foreach (var item in electronics)
{
    Console.WriteLine($"Electronic Name: {item.Name}, Price: {item.Price}");
}
//Activity 3: Add a new product
//Add a new product called "Blue Pen" with price 2.99, category "Stationery", and stock 150.
Console.WriteLine("====== Question 3 =======");
var newProduct = new Product
{
    Name = "Blue Pen",
    Price = 2.99M,
    Category = "Stationery",
    Stock = 150
};
context.Products.Add(newProduct);
context.SaveChanges();

if (context.Products.Count() > 5)
{
    Console.WriteLine("Product added sucessfully!.");
}
else
{
    Console.WriteLine("Error: Product not added.");
}

//Activity 4: Update stock of a product
//Find the "Coffee Mug" and change its stock to 200.
Console.WriteLine("====== Question 4 =======");
var updateCoffeeMug = context.Products
    .First(c => c.Name == "Coffee Mug");

if (updateCoffeeMug is not null)
{
    updateCoffeeMug.Stock = 200;
}
context.SaveChanges();
Console.WriteLine("Coffee Mug Stock updated sucessfully.");

//Activity 5: Show products that cost less than $20
//List the name and price of all products that cost less than 20 dollars.
Console.WriteLine("====== Question 5 =======");
var priceLess = context.Products
    .Where(p => p.Price < 20)
    .Select(s => $"Name: {s.Name}, Price: {s.Price}");

foreach (var item in priceLess)
{
    Console.WriteLine(item);
}

// ====================== SEED DATA ======================
void SeedData(ShopContext ctx)
{
    if (ctx.Products.Any()) return;

    var products = new List<Product>
    {
        new Product { Name = "Wireless Headphones", Price = 79.99m, Category = "Electronics", Stock = 25 },
        new Product { Name = "Coffee Mug", Price = 12.99m, Category = "Home", Stock = 120 },
        new Product { Name = "Notebook", Price = 5.49m, Category = "Stationery", Stock = 80 },
        new Product { Name = "USB Cable", Price = 8.99m, Category = "Electronics", Stock = 60 },
        new Product { Name = "Water Bottle", Price = 15.99m, Category = "Sports", Stock = 45 }
    };

    ctx.Products.AddRange(products);
    ctx.SaveChanges();
    Console.WriteLine("5 products seeded successfully.\n");
}

// ====================== MODELS ======================
public class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string Category { get; set; } = string.Empty;
    public int Stock { get; set; }
}

public class ShopContext : DbContext
{
    public DbSet<Product> Products => Set<Product>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseInMemoryDatabase("ShopDb");
    }
}