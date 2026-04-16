using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaskManagerTests;

[TestClass]
public class TaskServiceTests
{
    private Mock<ITaskRepository> _mockRepo;
    private Mock<IEmailService> _mockEmail;
    private TaskService _service;

    [TestInitialize]
    public void Setup()
    {
        _mockRepo = new Mock<ITaskRepository>();
        _mockEmail = new Mock<IEmailService>();
        _service = new TaskService(_mockRepo.Object, _mockEmail.Object);
    }

    // === Write your tests and experiment with Moq setups below ===

    [TestMethod]
    public async Task CreateTaskAsync_CreatesTaskAndSendsNotification()
    {
        var task = new TaskItem
        {
            Id = 2,
            Title = "Finish Report",
            Description = "Q4 financial report",
            AssignedTo = "john@example.com"
        };
        _mockRepo.Setup(r => r.AddTaskAsync(task)).Returns(Task.CompletedTask);
        _mockEmail.Setup(e => e.SendTaskAssignedNotificationAsync(task.AssignedTo, task.Title))
                  .Returns(Task.CompletedTask);
        _mockRepo.Setup(get => get.GetTaskAsync(It.IsAny<int>())).ReturnsAsync(task);

        int taskId = await _service.CreateTaskAsync(task.Title, task.Description, task.AssignedTo);

        Assert.IsTrue(task.Id > taskId); // Comparing CreateTaskAsync Id [0] with TaskItem Id [1] to see if it creates a new task
        _mockRepo.Verify(r => r.AddTaskAsync(It.Is<TaskItem>(t => t.Title == task.Title && t.Description == task.Description && t.AssignedTo == task.AssignedTo)), Times.Once);
        _mockEmail.Verify(e => e.SendTaskAssignedNotificationAsync(task.AssignedTo, task.Title), Times.Once);
    }
    [TestMethod]
    public async Task CompleteTaskAsync_CompletesTaskAndSendsNotification()
    {
        var task = new TaskItem
        {
            Id = 3,
            Title = "Finish Report",
            Description = "Q4 financial report",
            AssignedTo = "john@example.com",
            IsCompleted = false,
        };
        _mockRepo.Setup(c => c.AddTaskAsync(task)).Returns(Task.CompletedTask);
        _mockRepo.Setup(g => g.GetTaskAsync(task.Id)).ReturnsAsync(task);
        _mockRepo.Setup(u => u.UpdateTaskAsync(task)).Returns(Task.CompletedTask);
        _mockEmail.Setup(s => s.SendTaskCompletedNotificationAsync(task.AssignedTo, task.Title)).Returns(Task.CompletedTask);

        await _service.CompleteTaskAsync(task.Id, task.AssignedTo);

        Assert.IsTrue(task.IsCompleted);
        Assert.AreEqual(task.AssignedTo, task.AssignedTo);
        _mockRepo.Verify(c => c.GetTaskAsync(task.Id), Times.Once);
        _mockRepo.Verify(u => u.UpdateTaskAsync(It.Is<TaskItem>(t => t.Id == task.Id && t.IsCompleted)), Times.Once);
        _mockEmail.Verify(s => s.SendTaskCompletedNotificationAsync(task.AssignedTo, task.Title), Times.Once);
    }
    [TestMethod]
    public async Task GetPendingTasksForUserAsync_ReturnsOnlyPendingTasks()
    {
        var tasks = new List<TaskItem>
        {
            new TaskItem { Id = 4, Title = "Task 1", AssignedTo = "john@example.com", IsCompleted = false },
            new TaskItem { Id = 5, Title = "Task 2", AssignedTo = "john@example.com", IsCompleted = true },
            new TaskItem { Id = 6, Title = "Task 3", AssignedTo = "jane@example.com", IsCompleted = false }
        };
        _mockRepo.Setup(r => r.GetTasksByAssigneeAsync("john@example.com")).ReturnsAsync(tasks);

        var pendingTasks = await _service.GetPendingTasksForUserAsync("john@example.com");

        Assert.AreEqual(2, pendingTasks.Count);
        Assert.IsTrue(pendingTasks.All(t => !t.IsCompleted));
    }
    [TestMethod]
    public async Task DeleteTaskAsync_DeletesTask()
    {
        int taskId = 7;
        _mockRepo.Setup(d => d.DeleteTaskAsync(taskId)).Returns(Task.CompletedTask);

        await _service.DeleteTaskAsync(taskId);

        _mockRepo.Verify(d => d.DeleteTaskAsync(taskId), Times.Once);
    }
}