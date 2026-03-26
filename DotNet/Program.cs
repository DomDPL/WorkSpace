using Microsoft.EntityFrameworkCore;

namespace DotNet
{
    // Entity: Contact
    public class Contact
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<Address> Addresses { get; set; } = new();
    }

    // Entity: Address
    public class Address
    {
        public int Id { get; set; }
        public string Street { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public int ContactId { get; set; }
        public Contact? Contact { get; set; }
    }

    // DbContext
    public class ContactsContext : DbContext
    {
        public DbSet<Contact> Contacts => Set<Contact>();
        public DbSet<Address> Addresses => Set<Address>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("ContactsDb");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Contact>()
                .HasMany(c => c.Addresses)
                .WithOne(a => a.Contact)
                .HasForeignKey(a => a.ContactId);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            using var context = new ContactsContext();
            SeedData(context);

            // Activity 1: List all contacts with their number of addresses
            Console.WriteLine("---------Question 1 -------");
            var list = context.Contacts
                .Select(a => "Name: " + a.Name + ", Number of Addresses: " + a.Addresses.Count)
                .ToList();
            foreach (var l in list)
            {
                Console.WriteLine(l);
            }

            //Activity 2: Find contacts who live in Springfield
            Console.WriteLine("---------Question 2 -------");
            var findContact = context.Contacts
                .Where(c => c.Addresses.Any(a => a.City == "Springfield"))
                .Select(s => $"-{s.Name}, Lives in SpringField")
                .ToList();

            foreach (var f in findContact)
            {
                Console.WriteLine(f);
            }

            //Activity 3: Show all addresses grouped by city.
            //Task: Group all addresses by city and display the city name with the count of addresses in it.
            Console.WriteLine("---------Question 3 -------");
            var adressWithCityCount = context.Addresses
                .GroupBy(a => a.City)
                .Select(g => new { City = g.Key, Count = g.Count() })
                .OrderByDescending(o => o.Count)
                .ToList();

            foreach (var a in adressWithCityCount)
            {
                Console.WriteLine(a.City + " , " + a.Count + " Addresses");
            }
            //Activity 4: Find contacts with more than one address
            // Task: List the names of contacts who have 2 or more addresses.
            Console.WriteLine("---------Question 5 -------");
            var contact = context.Contacts
                .Where(c => c.Addresses.Count() > 1)
                .Select(n => $"Name: {n.Name} Has {n.Addresses.Count()}")
                .ToList();
            foreach (var c in contact)
            {
                Console.WriteLine(c);
            }


            //Activity 5: Show full details of a specific contact
            //Task: Find the contact named "Henry Brown" and display his name +all his addresses.
            Console.WriteLine("---------Question 5 -------");
            var henry = context.Contacts
                .Include(a => a.Addresses)
                .FirstOrDefault(c => c.Name == "Henry Brown");

            if (henry != null)
            {
                Console.WriteLine("Name" + henry.Name);
                Console.WriteLine("Address");
                foreach (var adress in henry.Addresses)
                {
                    Console.WriteLine(adress.Street + " , " + adress.City);
                }
            }
            else
            {
                Console.WriteLine("Henry not found.");
            }
            // //Activity 6: Count total number of contacts and addresses
            // //Task: Display how many contacts and how many addresses exist in the database.
            // Console.WriteLine("---------Question 6 -------");
            // var address = context.Addresses
            //     .ToList();
            // Console.WriteLine("Number of Contacts: " + address.Count());

            // var count = context.Contacts
            //     .ToList();
            // Console.WriteLine("Number of Contacts: " + count.Count());

            // //Activity 7: List contacts who have an address in Shelbyville
            // //Task: Show the names of all contacts that have at least one address in "Shelbyville".
            // Console.WriteLine("\n=== Activity 7: Contacts in Shelbyville ===");
            // var shelbyville = context.Contacts
            //     .Where(a => a.Addresses.Any(s => s.City == "Shelbyville" && s.City.Length > 1))
            //     .Select(n => n.Name)
            //     .ToList();
            // foreach (var s in shelbyville)
            // {
            //     Console.WriteLine(s);
            // }

            // //Activity 8: Show all addresses sorted by city
            // //Task: Display every address sorted alphabetically by city, then by street.
            // Console.WriteLine("\n=== Activity 8: All Addresses Sorted by City ===");
            // var sortAdresses = context.Addresses
            //     .OrderBy(a => a.City)
            //     .ThenBy(s => s.Street)
            //     .Select(sel => "Order by city = " + sel.City)
            //     .ToList();

            // foreach (var sort in sortAdresses)
            // {
            //     Console.WriteLine(sort);
            // }
            // //Activity 9: Find the contact with the most addresses
            // //Task: Find and display the contact who has the highest number of addresses.
            // Console.WriteLine("\n=== Activity 9: Contact with Most Addresses ===");
            // var mostAdress = context.Contacts
            //     .OrderByDescending(o => o.Addresses.Count())
            //     .Select(f => f.Name)
            //     .ToList();

            // foreach (var sort in mostAdress)
            // {
            //     Console.WriteLine(sort);
            // }

            // //Activity 10: List contacts who do NOT live in Springfield
            // //Task: Show names of contacts who have NO address in "Springfield".
            // Console.WriteLine("\n=== Activity 10: Contact with Most Addresses ===");
            // var notLive = context.Contacts
            //     .Where(c => !c.Addresses.Any(a => a.City == "Springfield"))
            //     .Select(s => s.Name)
            //     .ToList();

            // foreach (var sort in notLive)
            // {
            //     Console.WriteLine(sort);
            // }
        }

        static void SeedData(ContactsContext context)
        {
            if (context.Contacts.Any()) return;

            var contacts = new List<Contact>
            {
                new Contact
                {
                    Name = "Alice Smith",
                    Addresses = new List<Address>
                    {
                        new Address { Street = "123 Main St", City = "Springfield" },
                        new Address { Street = "456 Oak Ave", City = "Shelbyville" }
                    }
                },
                new Contact
                {
                    Name = "Bob Johnson",
                    Addresses = new List<Address>
                    {
                        new Address { Street = "789 Pine Rd", City = "Springfield" }
                    }
                },
                new Contact
                {
                    Name = "Carol White",
                    Addresses = new List<Address>
                    {
                        new Address { Street = "101 Maple St", City = "Ogdenville" }
                    }
                },
                new Contact
                {
                    Name = "David Martinez",
                    Addresses = new List<Address>
                    {
                        new Address { Street = "234 Elm St", City = "Springfield" },
                        new Address { Street = "567 Cedar Ln", City = "Capital City" }
                    }
                },
                new Contact
                {
                    Name = "Emma Davis",
                    Addresses = new List<Address>
                    {
                        new Address { Street = "890 Birch Ave", City = "Shelbyville" }
                    }
                },
                new Contact
                {
                    Name = "Frank Wilson",
                    Addresses = new List<Address>
                    {
                        new Address { Street = "321 Willow Dr", City = "Ogdenville" },
                        new Address { Street = "654 Spruce Way", City = "Springfield" }
                    }
                },
                new Contact
                {
                    Name = "Grace Lee",
                    Addresses = new List<Address>
                    {
                        new Address { Street = "987 Aspen Ct", City = "Capital City" }
                    }
                },
                new Contact
                {
                    Name = "Henry Brown",
                    Addresses = new List<Address>
                    {
                        new Address { Street = "147 Poplar St", City = "Springfield" },
                        new Address { Street = "258 Cherry Blvd", City = "Shelbyville" },
                        new Address { Street = "369 Walnut Rd", City = "Ogdenville" }
                    }
                },
                new Contact
                {
                    Name = "Iris Chen",
                    Addresses = new List<Address>
                    {
                        new Address { Street = "741 Magnolia Ave", City = "Capital City" }
                    }
                },
                new Contact
                {
                    Name = "Jack Thompson",
                    Addresses = new List<Address>
                    {
                        new Address { Street = "852 Sycamore Ln", City = "Springfield" }
                    }
                },
                new Contact
                {
                    Name = "Kelly Anderson",
                    Addresses = new List<Address>
                    {
                        new Address { Street = "963 Hickory Dr", City = "Shelbyville" },
                        new Address { Street = "159 Redwood Way", City = "Capital City" }
                    }
                },
                new Contact
                {
                    Name = "Liam Garcia",
                    Addresses = new List<Address>
                    {
                        new Address { Street = "753 Cypress Ct", City = "Ogdenville" }
                    }
                },
                new Contact
                {
                    Name = "Maya Patel",
                    Addresses = new List<Address>
                    {
                        new Address { Street = "486 Beech St", City = "Springfield" },
                        new Address { Street = "297 Dogwood Ave", City = "Shelbyville" }
                    }
                }
            };

            context.Contacts.AddRange(contacts);
            context.SaveChanges();
        }
    }
}
