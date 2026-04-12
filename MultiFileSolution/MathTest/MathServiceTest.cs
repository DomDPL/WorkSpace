using MathConsole;

namespace MathTest;

[TestClass]
public sealed class MathSeviceTest
{
    private readonly MathService _mathService;

    public MathSeviceTest()
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
}