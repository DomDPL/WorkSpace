using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model.MathSerice;

[TestClass]
public class MathServiceTests
{
    private readonly MathService _mathService;

    public MathServiceTests()
    {
        _mathService = new MathService();
    }

    [TestMethod]
    public void Add_ReturnsCorrectSum()
    {
        // Arrange
        int a = 5;
        int b = 7;

        // Act
        int result = _mathService.Add(a, b);

        // Assert
        Assert.AreEqual(12, result);
    }

    [TestMethod]
    public void Subtract_ReturnsCorrectDifference()
    {
        int result = _mathService.Subtract(10, 4);
        Assert.AreEqual(6, result);
    }

    [TestMethod]
    public void Multiply_ReturnsCorrectProduct()
    {
        int result = _mathService.Multiply(6, 7);
        Assert.AreEqual(42, result);
    }

    [TestMethod]
    public void Divide_ReturnsCorrectQuotient()
    {
        double result = _mathService.Divide(20, 5);
        Assert.AreEqual(4.0, result);
    }

    // [TestMethod]
    // [ExpectedException(typeof(DivideByZeroException))]
    // public void Divide_ThrowsException_WhenDividingByZero()
    // {
    //     _mathService.Divide(10, 0);
    // }

    [TestMethod]
    public void IsEven_ReturnsTrue_ForEvenNumbers()
    {
        Assert.IsTrue(_mathService.IsEven(4));
        Assert.IsTrue(_mathService.IsEven(0));
    }

    [TestMethod]
    public void IsEven_ReturnsFalse_ForOddNumbers()
    {
        Assert.IsFalse(_mathService.IsEven(5));
        Assert.IsFalse(_mathService.IsEven(1));
    }

    [TestMethod]
    public void GetMax_ReturnsLargerNumber()
    {
        Assert.AreEqual(10, _mathService.GetMax(10, 7));
        Assert.AreEqual(25, _mathService.GetMax(3, 25));
        Assert.AreEqual(100, _mathService.GetMax(100, 100));
    }
    // shoul thro DivideByZeroException from MathService class
    [TestMethod]
    public void DivideByNegeativeNumber_ReturnsCorrectQuotient()
    {
        // Arrange
        int a = 20;
        int b = 0;

        // Act
        double result = _mathService.DivideByAero(a, b);
        // Assert
        Assert.AreEqual(0, result);
    }
    [TestMethod]
    public void AddNegativeNumbers_ReturnsCorrectSum()
    {
        // Arrange
        int a = -5;
        int b = -7;

        // Act
        int result = _mathService.AddNegativeNumbers(a, b);

        // Assert
        Assert.AreEqual(-12, result);
    }
    [DataTestMethod]
    [DataRow(1, 2, 3)]
    [DataRow(-1, -2, -3)]
    [DataRow(0, 0, 0)]
    [DataRow(100, 200, 300)]
    public void Add_ReturnsCorrectSum_ForMultipleInputs(int a, int b, int expected)
    {
        int result = _mathService.Add(a, b);
        Assert.AreEqual(expected, result);
    }
}