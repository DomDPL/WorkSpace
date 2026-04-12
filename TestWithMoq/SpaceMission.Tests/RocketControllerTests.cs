using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Rocket;
using System;
using System.Threading.Tasks;
using System.Timers;

[TestClass]
public class RocketControllerTests
{
    private Mock<Rocket.IRocketEngine> _mockEngine;
    private RocketController _controller;

    [TestInitialize]
    public void Setup()
    {
        _mockEngine = new Mock<Rocket.IRocketEngine>();
        _controller = new RocketController(_mockEngine.Object);
    }

    [TestMethod]
    public async Task PrepareForLaunchAsync_AllSystemsGood_ReturnsTrue()
    {
        // Arrange
        _mockEngine.Setup(e => e.CheckEnginesAsync(It.IsAny<string>())).ReturnsAsync(true);
        _mockEngine.Setup(e => e.GetFuelLevelAsync(It.IsAny<string>())).ReturnsAsync(95);
        _mockEngine.Setup(e => e.LaunchAsync(It.IsAny<string>())).Returns(Task.CompletedTask);

        // Act
        bool launched = await _controller.PrepareForLaunchAsync("Falcon9");

        // Assert
        Assert.IsTrue(launched);
    }

    [TestMethod]
    public async Task PrepareForLaunchAsync_LowFuel_ReturnsFalse()
    {
        _mockEngine.Setup(e => e.CheckEnginesAsync(It.IsAny<string>())).ReturnsAsync(true);
        _mockEngine.Setup(e => e.GetFuelLevelAsync(It.IsAny<string>())).ReturnsAsync(40);

        bool launched = await _controller.PrepareForLaunchAsync("Falcon9");

        Assert.IsFalse(launched);
    }

    [TestMethod]
    public async Task PrepareForLaunchAsync_EngineCheckFails_ReturnsFalse()
    {
        _mockEngine.Setup(e => e.CheckEnginesAsync(It.IsAny<string>())).ReturnsAsync(false);

        bool launched = await _controller.PrepareForLaunchAsync("Falcon9");

        Assert.IsFalse(launched);
    }

    [TestMethod]
    public async Task PrepareForLaunchAsync_EngineThrowsException_TestFailsGracefully()
    {
        _mockEngine.Setup(e => e.CheckEnginesAsync(It.IsAny<string>()))
                   .ThrowsAsync(new Exception("Engine failure!"));

        await Assert.ThrowsExactlyAsync<InvalidOperationException>(
            () => _controller.PrepareForLaunchAsync("Falcon9"));
    }

    [TestMethod]
    public async Task GetMissionStatusAsync_HighFuel_ReturnsGoForLaunch()
    {
        _mockEngine.Setup(e => e.GetFuelLevelAsync("Falcon9")).ReturnsAsync(75);

        string status = await _controller.GetMissionStatusAsync("Falcon9");

        Assert.AreEqual("Go for launch!", status);
    }

    [TestMethod]
    public async Task GetMissionStatusAsync_LowFuel_ReturnsRefuelNeeded()
    {
        _mockEngine.Setup(e => e.GetFuelLevelAsync("Falcon9")).ReturnsAsync(30);

        string status = await _controller.GetMissionStatusAsync("Falcon9");

        Assert.AreEqual("Refuel needed!", status);
    }

    [TestMethod]
    public async Task PrepareForLaunchAsync_VerifyLaunchIsCalled()
    {
        _mockEngine.Setup(e => e.CheckEnginesAsync(It.IsAny<string>())).ReturnsAsync(true);
        _mockEngine.Setup(e => e.GetFuelLevelAsync(It.IsAny<string>())).ReturnsAsync(90);
        _mockEngine.Setup(e => e.LaunchAsync(It.IsAny<string>())).Returns(Task.CompletedTask);

        await _controller.PrepareForLaunchAsync("Falcon9");

        _mockEngine.Verify(e => e.LaunchAsync("Falcon9"), Times.Once);
    }
}