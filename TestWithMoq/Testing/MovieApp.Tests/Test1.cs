using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using MovieApp;

[TestClass]
public class MovieAccessTests
{
    private readonly MovieAccess _movieAccess;

    public MovieAccessTests()
    {
        _movieAccess = new MovieAccess();
    }

    [TestMethod]
    public void AddMovie_IncreaseTotalCount()
    {
        // arrange
        var movies = new Movie { Title = "Inception", Genre = "Sci-Fi", ReleaseYear = 2010, Rating = 8.8M };

        // act
        _movieAccess.AddMovie(movies);
        int count = _movieAccess.GetTotalMovieCount();
        // assert

        Assert.AreEqual(1, count);
    }

    [TestMethod]
    public void GetMovieById_ReturnsCorrectMovie()
    {
        // Arrange
        var movie = new Movie() { Title = "The Matrix", Genre = "Action", ReleaseYear = 1999, Rating = 8.7m };
        _movieAccess.AddMovie(movie);
        // Act
        var result = _movieAccess.GetMovieById(1);
        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(movie.Id, result.Id);

    }

    [TestMethod]
    public void GetMoviesByGenre_ReturnsOnlyMatchingGenre()
    {
        // Arrange
        _movieAccess.AddMovie(new Movie { Title = "Inception", Genre = "Sci-Fi", ReleaseYear = 2010, Rating = 8.8m });
        _movieAccess.AddMovie(new Movie { Title = "The Dark Knight", Genre = "Action", ReleaseYear = 2008, Rating = 9.0m });
        _movieAccess.AddMovie(new Movie { Title = "Interstellar", Genre = "Sci-Fi", ReleaseYear = 2014, Rating = 8.6m });

        // Act
        var sciFiMovies = _movieAccess.GetMoviesByGenre("sci-fi");

        // Assert
        Assert.AreEqual(2, sciFiMovies.Count);
        Assert.IsTrue(sciFiMovies.All(m => m.Genre.Equals("Sci-Fi", StringComparison.OrdinalIgnoreCase)));
    }

    [TestMethod]
    public void DeleteMovie_ReturnsTrue_WhenMovieExists()
    {
        // Arrange
        var movie = new Movie { Title = "Dune", Genre = "Sci-Fi", ReleaseYear = 2021, Rating = 8.0m };
        _movieAccess.AddMovie(movie);

        // Act
        bool deleted = _movieAccess.DeleteMovie(1);

        // Assert
        Assert.IsTrue(deleted);
        Assert.AreEqual(0, _movieAccess.GetTotalMovieCount());
    }

    [TestMethod]
    public void GetAllMovies_ReturnsCopyOfList()
    {
        // Arrange
        _movieAccess.AddMovie(new Movie { Title = "Test", Genre = "Drama", ReleaseYear = 2020, Rating = 7.5m });
        var movies1 = _movieAccess.GetAllMovies();

        // Act
        movies1.Clear();  // Modify the returned list

        // Assert
        Assert.IsTrue(_movieAccess.GetAllMovies().Any()); // Original list should still have the movie
    }
}