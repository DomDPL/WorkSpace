using CoffeeShop.Tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading.Tasks;

[TestClass]
public class BaristaTests
{
    private Mock<ICoffeeMachine>? _mockMachine;
    private Barista? _barista;

    [TestInitialize]
    public void Setup()
    {
        _mockMachine = new Mock<ICoffeeMachine>();
        _barista = new Barista(_mockMachine.Object);
    }

    // === YOUR TESTS GO HERE ===

    [TestMethod]
    public async Task TakeOrderAsync_MachineWorks_OrderSucceeds()
    {
        // Arrange
        _mockMachine!
            .Setup(m => m.MakeCoffeeAsync(It.IsAny<CoffeeOrder>()))
            .ReturnsAsync(true);

        // Act
        bool result = await _barista!.TakeOrderAsync("Latte", true, false);

        // Assert
        Assert.IsTrue(result);
    }

    [TestMethod]
    public async Task TakeOrderAsync_MachineFails_OrderFails()
    {
        // Arrange
        _mockMachine!
            .Setup(m => m.MakeCoffeeAsync(It.IsAny<CoffeeOrder>()))
            .ReturnsAsync(false);

        // Act
        bool result = await _barista!.TakeOrderAsync("Espresso", false, false);

        // Assert
        Assert.IsFalse(result);
    }

    [TestMethod]
    public async Task TakeOrderAsync_MachineThrowsException_TestHandlesIt()
    {
        // Arrange
        _mockMachine!
            .Setup(m => m.MakeCoffeeAsync(It.IsAny<CoffeeOrder>()))
            .ThrowsAsync(new Exception("Coffee machine is broken!"));

        // Act & Assert
        await Assert.ThrowsExactlyAsync<Exception>(
            () => _barista!.TakeOrderAsync("Cappuccino", true, true));
    }

    [TestMethod]
    public async Task CanServeCustomersAsync_HasEnoughStock_ReturnsTrue()
    {
        _mockMachine!
            .Setup(m => m.GetCoffeeStockAsync())
            .ReturnsAsync(12);

        bool canServe = await _barista!.CanServeCustomersAsync();

        Assert.IsTrue(canServe);
    }

    [TestMethod]
    public async Task CanServeCustomersAsync_LowStock_ReturnsFalse()
    {
        _mockMachine!
            .Setup(m => m.GetCoffeeStockAsync())
            .ReturnsAsync(3);

        bool canServe = await _barista!.CanServeCustomersAsync();

        Assert.IsFalse(canServe);
    }

    [TestMethod]
    public async Task TakeOrderAsync_SpecificDrink_UsesCorrectSetup()
    {
        _mockMachine!
            .Setup(m => m.MakeCoffeeAsync(It.Is<CoffeeOrder>(o => o.Drink == "Latte")))
            .ReturnsAsync(true);

        bool result = await _barista!.TakeOrderAsync("Latte", true, false);

        Assert.IsTrue(result);
        _mockMachine.Verify(m => m.MakeCoffeeAsync(It.IsAny<CoffeeOrder>()), Times.Once);

        // Add more tests below...
    }
}