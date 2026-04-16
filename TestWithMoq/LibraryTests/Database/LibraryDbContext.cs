using Microsoft.EntityFrameworkCore;
using LibraryTests.Models;

namespace LibraryTests.Database;

public class LibraryDbContext : DbContext
{
    public LibraryDbContext(DbContextOptions<LibraryDbContext> options) : base(options)
    {
    }

    public virtual DbSet<Book> Books { get; set; }
    public virtual DbSet<Member> Members { get; set; }
}