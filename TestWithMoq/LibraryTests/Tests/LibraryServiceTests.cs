using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryTests.Models;
using LibraryTests.Database;
using LibraryTests.Services;
using Moq;


namespace LibraryTests;

[TestClass]
public class LibraryServiceTests
{
    private Mock<LibraryDbContext> _mockContext;
    private Mock<DbSet<Book>> _mockBooks;
    private Mock<DbSet<Member>> _mockMembers;
    private LibraryService _service;

    [TestInitialize]
    public void Setup()
    {
        _mockBooks = new Mock<DbSet<Book>>();
        _mockMembers = new Mock<DbSet<Member>>();

        _mockContext = new Mock<LibraryDbContext>(new DbContextOptions<LibraryDbContext>());
        _mockContext.Setup(c => c.Books).Returns(_mockBooks.Object);
        _mockContext.Setup(c => c.Members).Returns(_mockMembers.Object);

        _service = new LibraryService(_mockContext.Object);
    }

    [TestMethod]
    public async Task GetAvailableBooksAsync_ReturnsOnlyAvailableBooks_SortedByTitle()
    {
        var books = new List<Book>
        {
            new Book { Id = 1, Title = "C# in Depth", IsAvailable = true },
            new Book { Id = 2, Title = "Clean Code", IsAvailable = false },
            new Book { Id = 3, Title = "The Pragmatic Programmer", IsAvailable = true },
            new Book { Id = 4, Title = "Design Patterns", IsAvailable = true }
        }.AsQueryable();

         _mockContext.Setup(m => m.Books).ReturnsDbSet(books);

        var result = await _service.GetAvailableBooksAsync();

        Assert.AreEqual(3, result.Count);
        Assert.AreEqual("C# in Depth", result[0].Title);
        Assert.AreEqual("Design Patterns", result[1].Title);
        Assert.AreEqual("The Pragmatic Programmer", result[2].Title);
    }

    [TestMethod]
    public async Task SearchBooksAsync_FindsBooksByTitleOrAuthor()
    {
        var books = new List<Book>
        {
            new Book { Id = 1, Title = "C# Programming", Author = "Jon Skeet" },
            new Book { Id = 2, Title = "Clean Architecture", Author = "Robert Martin" },
            new Book { Id = 3, Title = "Python Crash Course", Author = "Eric Matthes" }
        }.AsQueryable();

         _mockContext.Setup(m => m.Books).ReturnsDbSet(books);

        var result = await _service.SearchBooksAsync("c#");

        Assert.AreEqual(1, result.Count);
        Assert.AreEqual("C# Programming", result[0].Title);
    }

    [TestMethod]
    public async Task BorrowBookAsync_SuccessfulBorrow_ReturnsTrue()
    {
        var book = new Book { Id = 10, Title = "Test Book", IsAvailable = true };
        var member = new Member { Id = 5, Name = "John Doe" };

        _mockBooks.Setup(b => b.FindAsync(10)).ReturnsAsync(book);
        _mockMembers.Setup(m => m.FindAsync(5)).ReturnsAsync(member);

        var result = await _service.BorrowBookAsync(10, 5);

        Assert.IsTrue(result);
        Assert.IsFalse(book.IsAvailable);
        Assert.IsNotNull(book.BorrowedDate);
        _mockContext.Verify(c => c.SaveChangesAsync(default), Times.Once);
    }

    [TestMethod]
    public async Task GetOverdueBooksCountAsync_ReturnsCorrectCount()
    {
        var books = new List<Book>
        {
            new Book { Id = 1, IsAvailable = false, BorrowedDate = DateTime.UtcNow.AddDays(-45) },
            new Book { Id = 2, IsAvailable = true, BorrowedDate = null },
            new Book { Id = 3, IsAvailable = false, BorrowedDate = DateTime.UtcNow.AddDays(-15) },
            new Book { Id = 4, IsAvailable = false, BorrowedDate = DateTime.UtcNow.AddDays(-35) }
        }.AsQueryable();

        _mockContext.Setup(m => m.Books).ReturnsDbSet(books);
        int overdue = await _service.GetOverdueBooksCountAsync();

        Assert.AreEqual(2, overdue);
    }
    [TestMethod]
    public async Task GetMostBorrowedAuthorAsync_ReturnsHighestCount()
    {
        var book = new Book { Id = 1, Title = "Akoko obanda", IsAvailable = true };
        var book1 = new Book { Id = 2, Title = "Coucasian chalk circle", IsAvailable = true };
        var book2 = new Book { Id = 3, Title = "Betreal in the city", IsAvailable = true };
        var book3 = new Book { Id = 4, Title = "Akoko obanda", IsAvailable = true };
        var book4 = new Book { Id = 5, Title = "Akoko obanda", IsAvailable = true };

        List<Book> list = new List<Book>();
        list.Add(book);
        list.Add(book1);
        list.Add(book2);
        list.Add(book3);
        list.Add(book4);

        var books = list.AsQueryable();
        _mockContext.Setup(m => m.Books).ReturnsDbSet(books);

        var result = await _service.GetMostBorrowedAuthorAsync();

        Assert.AreEqual(1, result.Count);
        Assert.AreEqual("Akoko obanda", result[0].Title);
    }

}