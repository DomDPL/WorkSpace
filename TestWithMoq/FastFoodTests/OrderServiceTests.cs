using Moq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FastFoodTests;

[TestClass]
public sealed class OrderServiceTests
{
    private Mock<IOrderRepository> _mockRepo;
    private Mock<IKitchenService> _mockKitchen;
    private Mock<INotificationService> _mockNotification;
    private OrderService _service;

    [TestInitialize]
    public void Setup()
    {
        _mockRepo = new Mock<IOrderRepository>();
        _mockKitchen = new Mock<IKitchenService>();
        _mockNotification = new Mock<INotificationService>();
        _service = new OrderService(_mockRepo.Object, _mockKitchen.Object, _mockNotification.Object);
    }

    // === Experiment with different Moq setups here ===

    [TestMethod]
    public async Task PlaceOrderAsync_ValidOrder_SucceedsAndSendsConfirmation()
    {
        // Arrange
        _mockKitchen.Setup(k => k.CanPrepareOrderAsync(It.IsAny<List<string>>())).ReturnsAsync(true);
        _mockNotification.Setup(m => m.SendOrderConfirmationAsync(It.IsAny<string>(), It.IsAny<int>())).Returns(Task.CompletedTask);
        _mockKitchen.Setup(k => k.MarkOrderAsPreparedAsync(1)).Returns(Task.CompletedTask);
        _mockNotification.Setup(m => m.SendReadyForPickupAsync(It.IsAny<string>(), It.IsAny<int>())).Returns(Task.CompletedTask);

        // Act
        int orderId = await _service.PlaceOrderAsync("Alice", "555-1111", new List<string> { "Burger", "Fries" }, 12.99m);

        // Assert
        Assert.IsTrue(orderId > -1);
        _mockKitchen.Verify(k => k.CanPrepareOrderAsync(It.IsAny<List<string>>()), Times.Once);
        _mockNotification.Verify(m => m.SendOrderConfirmationAsync("555-1111", orderId), Times.Once);
        _mockKitchen.Verify(k => k.MarkOrderAsPreparedAsync(orderId), Times.Never);
        _mockNotification.Verify(m => m.SendReadyForPickupAsync(It.IsAny<string>(), It.IsAny<int>()), Times.Never);
    }
    [TestMethod]
    public async Task MarkOrderReadyAsync_ExistingOrder_MarksAsPreparedAndSendsNotification()
    {
        // Arrange
        var order = new Order
        {
            Id = 1,
            CustomerName = "Bob",
            PhoneNumber = "555-2222",
            Items = new List<string> { "Pizza" },
            TotalAmount = 15.99m,
            IsPrepared = false
        };
        _mockRepo.Setup(r => r.GetOrderAsync(1)).ReturnsAsync(order);
        _mockKitchen.Setup(k => k.MarkOrderAsPreparedAsync(1)).Returns(Task.CompletedTask);
        _mockRepo.Setup(r => r.UpdateOrderAsync(It.IsAny<Order>())).Returns(Task.CompletedTask);
        _mockNotification.Setup(m => m.SendReadyForPickupAsync(It.IsAny<string>(), It.IsAny<int>())).Returns(Task.CompletedTask);

        // Act
        await _service.MarkOrderReadyAsync(1);

        // Assert
        _mockRepo.Verify(r => r.GetOrderAsync(1), Times.Once);
        _mockKitchen.Verify(k => k.MarkOrderAsPreparedAsync(1), Times.Once);
        _mockRepo.Verify(r => r.UpdateOrderAsync(It.Is<Order>(o => o.Id == 1 && o.IsPrepared)), Times.Once);
        _mockNotification.Verify(m => m.SendReadyForPickupAsync("555-2222", 1), Times.Once);
    }
    [TestMethod]
    public async Task GetPendingOrdersAsync_ReturnsOnlyUnpreparedOrders()
    {
        // Arrange
        var orders = new List<Order>
        {
            new Order { Id = 1, IsPrepared = false },
            new Order { Id = 2, IsPrepared = true },
            new Order { Id = 3, IsPrepared = false },
            new Order { Id = 4, IsPrepared = false }
        };
        _mockRepo.Setup(r => r.GetPendingOrdersAsync()).ReturnsAsync(orders);

        // Act
        var pendingOrders = await _service.GetPendingOrdersAsync();

        // Assert
        Assert.AreEqual(3, pendingOrders.Count);
        Assert.IsTrue(pendingOrders.All(o => !o.IsPrepared));
    }
    [TestMethod]
    public async Task MarkOrderReadyAsync_OrderNotFound_ThrowsException()
    {
        // Arrange
        _mockRepo.Setup(r => r.GetOrderAsync(999)).ReturnsAsync((Order?)null);

        // Act & Assert
        await Assert.ThrowsExactlyAsync<InvalidOperationException>(() => _service.MarkOrderReadyAsync(999));
        _mockRepo.Verify(r => r.GetOrderAsync(999), Times.Once);
        _mockKitchen.Verify(k => k.MarkOrderAsPreparedAsync(It.IsAny<int>()), Times.Never);
        _mockRepo.Verify(r => r.UpdateOrderAsync(It.IsAny<Order>()), Times.Never);
        _mockNotification.Verify(m => m.SendReadyForPickupAsync(It.IsAny<string>(), It.IsAny<int>()), Times.Never);
    }
    [TestMethod]
    public async Task PlaceOrderAsync_KitchenCannotPrepare_ThrowsException()
    {
        // Arrange
        _mockKitchen.Setup(k => k.CanPrepareOrderAsync(It.IsAny<List<string>>())).ReturnsAsync(false);

        // Act & Assert
        await Assert.ThrowsExactlyAsync<InvalidOperationException>(() => _service.PlaceOrderAsync("Charlie", "555-3333", new List<string> { "Sushi" }, 20.00m));
        _mockKitchen.Verify(k => k.CanPrepareOrderAsync(It.IsAny<List<string>>()), Times.Once);
        _mockNotification.Verify(m => m.SendOrderConfirmationAsync(It.IsAny<string>(), It.IsAny<int>()), Times.Never);
    }
}
