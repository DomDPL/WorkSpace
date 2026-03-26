using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System;
using System.Collections.Concurrent;

var context = new CrmContext();
SeedData(context);

Console.WriteLine("EF CRM Database is ready!\n");

// TODO: Write your 10 activities here below

//Activity 1: List all customers with their order count
Console.WriteLine("===== Q 1 ======");
var listAll = context.Customers // can also use include without select.
    .Select(s => $"First Name: {s.FirstName}\nLast Name: {s.LastName}\nEmail: {s.Email}\nPhone: {s.Phone}\nBirthday: {s.DateOfBirth}\nNumber Of Orders: {s.Orders.Count()}\n")
    .ToList();

foreach (var list in listAll)
{
    Console.WriteLine(list);
}

//Activity 2: Show full details of Liam Chen
Console.WriteLine("===== Q 2 ======");
var liam = context.Customers
    .Include(o => o.Orders)
    .FirstOrDefault(s => s.FirstName == "Liam" && s.LastName == "Chen" && s.Email == "liam.chen@email.com");

if (liam != null)
{
    Console.WriteLine($"First Name: {liam.FirstName}\nLast Name: {liam.LastName}\nEmail: {liam.Email}\nPhone: {liam.Phone}\nBirthday: {liam.DateOfBirth}\nNumber Of Orders: {liam.Orders.Count()}\n");
}
else
{
    Console.WriteLine("Liam Chen not found.");
}

//Activity 3: Add a new customer
Console.WriteLine("===== Q 3 ======");
var newCustomer = new Customer
{
    FirstName = "Dominic",
    LastName = "Obiala",
    Email = "dominic@gmail.com",
    Phone = "12345678",
    DateOfBirth = new DateTime(1234, 12, 3),
};

var addeNewCustomer = context.Customers;
addeNewCustomer.Add(newCustomer);
context.SaveChanges();

foreach (var list in addeNewCustomer)
{
    Console.WriteLine($"First Name: {list.FirstName}\nLast Name: {list.LastName}\nEmail: {list.Email}\nPhone: {list.Phone}\nBirthday: {list.DateOfBirth}\n");
}


//Activity 4: Add an order for Emma Johnson
Console.WriteLine("===== Q 4 ======");
var emma = context.Customers.First(c => c.Email == "emma.j@email.com");

var newOrder = new Order
{
    OrderDate = DateTime.Now,
    TotalAmount = 159.99m,
    Status = "Completed",
    CustomerId = emma.Id
};
context.Orders.Add(newOrder);
context.SaveChanges();

var check = context.Orders.Count();
Console.WriteLine($"Total orders increased from 3 to {check}\n");

//Activity 5: List all completed orders
Console.WriteLine("===== Q 5 ======");
var listAllOrders = context.Orders
    .Include(c => c.Customer) // to include the customer details in the order query.
    .Where(c => c.Status == "Completed")
    .ToList();

if (listAllOrders is not null)
{
    foreach (var list in listAllOrders)
    {
        Console.WriteLine($"First Name: {list.Customer?.FirstName}\nOrdered date: {list.OrderDate}\nStatus: {list.Status}\nAmount: {list.TotalAmount}\n");
    }
}
//Activity 6: Update Sophia’s phone number
Console.WriteLine("===== Q 6 ======");
var getSophiaByEmail = context.Customers
    .FirstOrDefault(s => s.Email == "sophia.r@email.com");

if (getSophiaByEmail != null)
{

    getSophiaByEmail.Phone = "000,000,0000";
}
context.SaveChanges();
Console.WriteLine($"You have successfully updated Sbohias numbet to {getSophiaByEmail?.Phone}");

//Activity 7: Show total revenue from all orders
Console.WriteLine("===== Q 7 ======");
var totalRevenue = context.Orders
    .Select(r => r.TotalAmount).Sum();
Console.WriteLine($"Total Revenue: {totalRevenue:C}");

//Activity 8: Find customers born after 1995
Console.WriteLine("===== Q 8 ======");
var filterAge = context.Customers
    .Where(a => a.DateOfBirth.Year > 1995)
    .ToList();

foreach (var date in filterAge)
{
    Console.WriteLine($"Name: {date.FirstName}, Date Of Birth: {date.DateOfBirth}");
}

//Activity 9: Remove Liam Chen
Console.WriteLine("===== Q 9 ======");
var removeLiam = context.Customers
    .First(liam => liam.FirstName == "Liam" && liam.LastName == "Chen");

if (removeLiam is not null)
{
    Console.WriteLine("Liam removed successfully!.");


}
else
{
    Console.WriteLine("Liam not found in the database.");
}
context.Customers.Remove(removeLiam);
context.SaveChanges();
//Activity 10: List all customers sorted by last name
Console.WriteLine("===== Q 10 ======");
var sortByLastName = context.Customers
    .OrderBy(sort => sort.LastName)
    .Select(n => n.LastName)
    .ToList();

foreach (var date in sortByLastName)
{
    Console.WriteLine($"Last Name: {date}");
}


// ====================== HELPER METHOD ======================
void SeedData(CrmContext ctx)
{
    if (ctx.Customers.Any()) return;

    var customers = new List<Customer>
    {
        new Customer
        {
            FirstName = "Emma", LastName = "Johnson", Email = "emma.j@email.com",
            Phone = "555-1234", DateOfBirth = new DateTime(1995, 3, 15),
            Orders = new List<Order>
            {
                new Order { OrderDate = DateTime.Now.AddDays(-30), TotalAmount = 249.99m, Status = "Completed" },
                new Order { OrderDate = DateTime.Now.AddDays(-5),  TotalAmount = 89.50m,  Status = "Pending" }
            }
        },
        new Customer
        {
            FirstName = "Liam", LastName = "Chen", Email = "liam.chen@email.com",
            Phone = "555-5678", DateOfBirth = new DateTime(1988, 7, 22),
            Orders = new List<Order>
            { new Order { OrderDate = DateTime.Now.AddDays(-15), TotalAmount = 1249.00m, Status = "Completed" } }
        },
        new Customer
        {
            FirstName = "Sophia", LastName = "Rodriguez", Email = "sophia.r@email.com",
            Phone = "555-9012", DateOfBirth = new DateTime(2000, 11, 8)
        }
    };

    ctx.Customers.AddRange(customers);
    ctx.SaveChanges();
    Console.WriteLine("Seeded 3 customers with sample orders.\n");
}

// ====================== MODELS & CONTEXT ======================
public class Customer
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; }
    public List<Order> Orders { get; set; } = new();

    internal object Select(object s, string v)
    {
        throw new NotImplementedException();
    }
}

public class Order
{
    public int Id { get; set; }
    public DateTime OrderDate { get; set; }
    public decimal TotalAmount { get; set; }
    public string Status { get; set; } = "Pending";
    public int CustomerId { get; set; }
    public Customer? Customer { get; set; }
}

public class CrmContext : DbContext
{
    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<Order> Orders => Set<Order>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseInMemoryDatabase("CrmDb");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>()
            .HasMany(c => c.Orders)
            .WithOne(o => o.Customer)
            .HasForeignKey(o => o.CustomerId);
    }
}