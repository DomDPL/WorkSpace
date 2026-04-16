using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RideShareTests;
using RideShareTests.Interface;
using RideShareTests.Request;
using RideShareTests.Services;
using System;
using System.Threading.Tasks;

namespace RideShareTests.Tests;

[TestClass]
public class RideServiceTests
{
    private Mock<IRideRepository>? _mockRepo;
    private Mock<INotificationRepository>? _mockNotification;
    private RideService? _service;

    [TestInitialize]
    public void Setup()
    {
        _mockRepo = new Mock<IRideRepository>();
        _mockNotification = new Mock<INotificationRepository>();
        _service = new RideService(_mockRepo, _mockNotification);
    }

    [TestMethod]
    public async Task ProcessRequestAsync_BookRide_SuccessfullyBooksRide()
    {
        var driver = new Driver(101, "Alice Smith", "Toyota Camry", 4.9m, true);
        _mockRepo!.Setup(r => r.FindAvailableDriverAsync("123 Main St")).ReturnsAsync(driver);
        _mockRepo!.Setup(r => r.AddRideAsync(It.IsAny<Ride>())).Returns(Task.CompletedTask);
        _mockNotification!.Setup(n => n.SendRideConfirmationAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                        .Returns(Task.CompletedTask);
        _mockNotification!.Setup(n => n.SendDriverAssignedNotificationAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                        .Returns(Task.CompletedTask);

        var request = new BookRideRequest(42, "123 Main St", "456 Airport Blvd", 25.50m);

        var result = await _service!.ProcessRequestAsync(request);

        Assert.IsTrue((bool)result);
        _mockRepo!.Verify(r => r.AddRideAsync(It.IsAny<Ride>()), Times.Once);
        _mockNotification!.Verify(n => n.SendDriverAssignedNotificationAsync(It.IsAny<string>(), "Alice Smith", "Toyota Camry"), Times.Once);
    }

    [TestMethod]
    public async Task ProcessRequestAsync_NoAvailableDriver_ReturnsFalse()
    {
        _mockRepo!.Setup(r => r.FindAvailableDriverAsync(It.IsAny<string>())).ReturnsAsync((Driver?)null);

        var request = new BookRideRequest(42, "789 Park Ave", "101 Ocean Dr", 18.75m);

        var result = await _service!.ProcessRequestAsync(request);
        Assert.IsFalse((bool)result);
    }

    [TestMethod]
    public async Task ProcessRequestAsync_CancelRide_SuccessfullyCancels()
    {
        var existingRide = new Ride(1001, 42, 101, "123 Main St", "456 Airport Blvd", 25.50m, DateTime.UtcNow, false);
        _mockRepo!.Setup(r => r.GetRideAsync(1001)).ReturnsAsync(existingRide);
        _mockRepo!.Setup(r => r.UpdateRideAsync(It.IsAny<Ride>())).Returns(Task.CompletedTask);
        _mockNotification!.Setup(n => n.SendRideCancelledNotificationAsync(It.IsAny<string>(), It.IsAny<string>()))
                        .Returns(Task.CompletedTask);

        var request = new CancelRideRequest(42, 1001, "Change of plans");

        var result = await _service!.ProcessRequestAsync(request);

        Assert.IsTrue((bool)result);
        _mockNotification!.Verify(n => n.SendRideCancelledNotificationAsync(It.IsAny<string>(), "Change of plans"), Times.Once);
    }

    [TestMethod]
    public async Task ProcessRequestAsync_GetRideStatus_ReturnsCorrectStatus()
    {
        var completedRide = new Ride(2002, 42, 101, "A", "B", 15m, DateTime.UtcNow, true);
        _mockRepo!.Setup(r => r.GetRideAsync(2002)).ReturnsAsync(completedRide);

        var request = new GetRideStatusRequest(42, 2002);

        var status = await _service!.ProcessRequestAsync(request);
        Assert.AreEqual("In Progress", (string)status);
    }

    [TestMethod]
    public async Task ProcessRequestAsync_UnsupportedRequest_ThrowsException()
    {
        await Assert.ThrowsExactlyAsync<InvalidOperationException>(
            () => _service!.ProcessRequestAsync(It.IsAny<RideRequest>()));
    }
}