using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SpaceMission.Tests;
using System;
using System.Threading.Tasks;

[TestClass]
public class RocketControllerTests
{
    private Mock<IRocketEngine>? _mockEngine;
    private RocketController? _controller;

    [TestInitialize]
    public void Setup()
    {
        _mockEngine = new Mock<IRocketEngine>();
        _controller = new RocketController(_mockEngine.Object);
    }
    [TestMethod]
    public async Task PrepareForLaunchAsync_LowFuel_ReturnsFalse()
    {
        string rocketId = "121";
        // arrange
        _mockEngine!
            .Setup(e => e.CheckEnginesAsync(rocketId))
            .ReturnsAsync(true);
        _mockEngine
            .Setup(f => f.GetFuelLevelAsync(rocketId))
            .ReturnsAsync(20);
        // act
        var result = await _controller!.PrepareForLaunchAsync(rocketId);

        // Assert
        Assert.IsFalse(result);
        _mockEngine.Verify(e => e.LaunchAsync(It.IsAny<string>()), Times.Never);
    }
    [TestMethod]
    public async Task PrepareForLaunchAsync_EngineFails_SholdReturnTrue()
    {
        string rocketId = "122";
        //Arrange
        _mockEngine!
            .Setup(e => e.CheckEnginesAsync(rocketId))
            .ReturnsAsync(false);

        // act
        var result = await _controller!.PrepareForLaunchAsync(rocketId);

        //assert
        Assert.IsFalse(result);
        _mockEngine.Verify(f => f.GetFuelLevelAsync(It.IsAny<string>()), Times.Never);
        _mockEngine.Verify(v => v.LaunchAsync(It.IsAny<string>()), Times.Never);
    }
    [TestMethod]
    public async Task PrepareForLaunchAsync_EngineThrowsException_TestFailsGracefully()
    {
        string rocketId = "123";

        _mockEngine!
            .Setup(e => e.CheckEnginesAsync(It.IsAny<string>()))
            .ThrowsAsync(new Exception("Engine fails Gracefully."));

        await Assert.ThrowsExactlyAsync<Exception>(
            () => _controller!.PrepareForLaunchAsync(rocketId)
        );
    }
    [TestMethod]
    public async Task PrepareForLaunchAsync_HightFuel_ReturnsGoForLaunch()
    {
        string rocketId = "124";
        _mockEngine!
            .Setup(f => f.GetFuelLevelAsync(rocketId))
            .ReturnsAsync(90);

        var result = await _controller!.GetMissionStatusAsync(rocketId);

        Assert.AreEqual("Go for launch!", result);
    }
    [TestMethod]
    public async Task PrepareForLaunchAsync_LowFuel_ReturnsRefuelNeeded()
    {
        var rocketId = "125";
        _mockEngine!
            .Setup(f => f.GetFuelLevelAsync(rocketId))
            .ReturnsAsync(30);

        var result = await _controller!.GetMissionStatusAsync(rocketId);

        Assert.AreEqual("Refuel needed!", result);
    }
    [TestMethod]
    public async Task PrepareForLaunchingAsync_VarifyLaunchingIsCalled()
    {
        string rocketId = "126";
        _mockEngine!
            .Setup(e => e.CheckEnginesAsync(rocketId))
            .ReturnsAsync(true);

        _mockEngine!
            .Setup(f => f.GetFuelLevelAsync(rocketId))
            .ReturnsAsync(100);

        _mockEngine
            .Setup(l => l.LaunchAsync(rocketId))
            .Returns(Task.CompletedTask);

        var result = await _controller!.PrepareForLaunchAsync(rocketId);

        Assert.IsTrue(result);
        _mockEngine.Verify(p => p.LaunchAsync(rocketId), Times.Once);
    }
    [TestMethod]
    [DataRow("126", true, 90, true)]
    [DataRow("126", true, 79, false)]
    [DataRow("126", false, 90, false)]
    public async Task PrepareForLaunchAsync_ReturnsExpectedResult(
    string rocketId,
    bool engineReady,
    int fuelLevel,
    bool expectedResult)
    {
        // Arrange
        _mockEngine!
            .Setup(e => e.CheckEnginesAsync(rocketId))
            .ReturnsAsync(engineReady);

        _mockEngine!
            .Setup(e => e.GetFuelLevelAsync(rocketId))
            .ReturnsAsync(fuelLevel);

        _mockEngine!
            .Setup(e => e.LaunchAsync(rocketId))
            .Returns(Task.CompletedTask);

        // Act
        var result = await _controller!.PrepareForLaunchAsync(rocketId);

        // Assert
        Assert.AreEqual(expectedResult, result);

        if (expectedResult)
        {
            _mockEngine.Verify(e => e.LaunchAsync(rocketId), Times.Once);
        }
        else
        {
            _mockEngine.Verify(e => e.LaunchAsync(It.IsAny<string>()), Times.Never);
        }
    }
}
