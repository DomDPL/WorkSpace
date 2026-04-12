using System;
using LmsTests.Common;
using LmsTests.Interfaces;
using Moq;

namespace LmsTests.Models;

public class CourseService
{
    private readonly IEnrollmentRepository _repository;
    private readonly INotificationService _notificationService;

    public CourseService(IEnrollmentRepository repository, INotificationService notificationService)
    {
        _repository = repository;
        _notificationService = notificationService;
    }

    public async Task<bool> ProcessRequestAsync(CorseRequest request)
    {
        return request switch
        {
            EnrolStudentRequest enroll => await HandleEnrolmentAsync(enroll),
            CheckAvailabilityRequest availability => await HandleAvailablityCheckAsync(availability),
            _ => throw new ArgumentException("UnSupported request type")
        };
    }

    public async Task<bool> HandleAvailablityCheckAsync(CheckAvailabilityRequest availability)
    {
        int enrolled = await _repository.GetEnrollmentCountAsync(availability.CourseId);
        return enrolled < 30; // Only 29 students can be enrolled per course.
    }

    public async Task<bool> HandleEnrolmentAsync(EnrolStudentRequest enroll)
    {
        // Check if alredy enrolled
        var existing = await _repository.GetEnrollmentAsync(enroll.StudentId, enroll.CourseId);
        if (existing is not null)
            return false;

        // Check if course is full
        var isFull = await _repository.IsCourseFullAsync(enroll.CourseId);
        if (isFull)
        {
            await _notificationService.SendCourseFullNotificationAsync(enroll.CourseTitle);
            return false;
        }
        // Now enrol a student
        var newEnrolment = new Enrollment
        {
            Id = enroll.Id,
            StudentId = enroll.StudentId,
            CourseId = enroll.CourseId,
            EnrolledDate = DateTime.UtcNow,
            IsCompleted = true
        };
        await _repository.AddEnrollmentAsync(newEnrolment);
        await _notificationService.SendEnrollmentConfirmationAsync(enroll.StudentEmail, enroll.CourseTitle);
        return true;
    }
}
