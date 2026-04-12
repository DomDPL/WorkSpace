using LmsTests;
using LmsTests.Common;
using LmsTests.Interfaces;
using LmsTests.Models;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading.Tasks;

namespace LmsTests.Test
{
    [TestClass]
    public class CourseServiceTests
    {
        private Mock<IEnrollmentRepository>? _mockRepo;
        private Mock<INotificationService>? _mockNotification;
        private CourseService? _service;

        [TestInitialize]
        public void Setup()
        {
            _mockRepo = new Mock<IEnrollmentRepository>();
            _mockNotification = new Mock<INotificationService>();
            _service = new CourseService(_mockRepo.Object, _mockNotification.Object);
        }

        [TestMethod]
        public async Task EnrollStudentAsync_NewStudent_SuccessfullyEnrolls()
        {
            // Arrange
            // seed student and course data
            var student = new EnrolStudentRequest
            {
                StudentId = 101,
                CourseId = 1,
                EnrolledDate = DateTime.UtcNow,
                IsCompleted = true,
                StudentEmail = "student@example.com",
                CourseTitle = "C# Basics"
            };
            _mockRepo!.Setup(r => r.GetEnrollmentAsync(student.StudentId, student.CourseId)).ReturnsAsync((Enrollment?)null);
            _mockRepo!.Setup(c => c.IsCourseFullAsync(student.CourseId)).ReturnsAsync(false);
            _mockRepo!.Setup(m => m.AddEnrollmentAsync(It.IsAny<Enrollment>())).Returns(Task.CompletedTask);
            _mockNotification!.Setup(n => n.SendEnrollmentConfirmationAsync(student.StudentEmail, student.CourseTitle))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _service!.HandleEnrolmentAsync(student);

            // Assert
            Assert.IsTrue(result);
            _mockRepo.Verify(r => r.AddEnrollmentAsync(It.IsAny<Enrollment>()), Times.Once);
            _mockNotification.Verify(n => n.SendEnrollmentConfirmationAsync(student.StudentEmail, student.CourseTitle), Times.Once);
        }

        [TestMethod]
        public async Task EnrollStudentAsync_AlreadyEnrolled_ReturnsFalse()
        {
            var enrol = new Enrollment
            {
                Id = 2,
                StudentId = 102,
                CourseId = 1,
                EnrolledDate = DateTime.UtcNow,
                IsCompleted = true
            };
            _mockRepo!
                .Setup(e => e.GetEnrollmentAsync(enrol.StudentId, enrol.CourseId)).ReturnsAsync(enrol);

            var request = new EnrolStudentRequest
            {
                StudentId = enrol.StudentId,
                CourseId = enrol.CourseId,
                EnrolledDate = DateTime.UtcNow,
                IsCompleted = true,
                StudentEmail = "student@example.com",
                CourseTitle = "C# Basics"
            };

            var result = await _service!.ProcessRequestAsync(request);

            Assert.IsFalse(result);
            _mockRepo.Verify(i => i.AddEnrollmentAsync(It.IsAny<Enrollment>()), Times.Never);
        }

        [TestMethod]
        public async Task EnrollStudentAsync_CourseIsFull_ReturnsFalseAndSendsNotification()
        {
            var enrol = new Enrollment
            {
                Id = 2,
                StudentId = 102,
                CourseId = 1
            };
            _mockRepo!.Setup(e => e.GetEnrollmentAsync(enrol.StudentId, enrol.CourseId)).ReturnsAsync(It.IsAny<Enrollment>());
            _mockRepo!.Setup(f => f.IsCourseFullAsync(enrol.CourseId)).ReturnsAsync(true);
            _mockNotification!.Setup(m => m.SendCourseFullNotificationAsync("2026 Cohort")).Returns(Task.CompletedTask);

            
                var enrolRequest = new EnrolStudentRequest
                {
                    StudentId = enrol.StudentId,
                    CourseId = enrol.CourseId,
                    EnrolledDate = DateTime.UtcNow,
                    IsCompleted = true,
                    StudentEmail = "student@example.com",
                    CourseTitle = "DPL 2026 Cohort"
                };

            var result = await _service!.HandleEnrolmentAsync(enrolRequest);


            Assert.IsFalse(result);
            _mockRepo!.Verify(en => en.AddEnrollmentAsync(It.IsAny<Enrollment>()), Times.Never);
        }

        [TestMethod]
        public async Task GetAvailableSpotsAsync_ReturnsCorrectCount()
        {
            var availabilityRequest = new CheckAvailabilityRequest
            {
                CourseId = 10
            };
            _mockRepo!.Setup(r => r.GetEnrollmentCountAsync(availabilityRequest.CourseId)).ReturnsAsync(29);

            var spots = await _service!.HandleAvailablityCheckAsync(availabilityRequest);

            Assert.IsTrue(spots); // available because 30 > 29 so student can enroll
            _mockRepo.Verify(r => r.GetEnrollmentCountAsync(availabilityRequest.CourseId), Times.Once);
        }

        [TestMethod]
        public async Task HandleAvailabilityCheckAsync_RepositoryThrowsException_PropagatesException()
        {
            // Arrange
            var request = new CheckAvailabilityRequest
            {
                CourseId = 20
            };

            _mockRepo!
                .Setup(r => r.GetEnrollmentCountAsync(request.CourseId))
                .ThrowsAsync(new Exception("Database error"));

            // Act
            var ex = await Assert.ThrowsExactlyAsync<Exception>(
                () => _service!.HandleAvailablityCheckAsync(request));

            // Assert
            Assert.AreEqual("Database error", ex.Message);
        }
    }
}