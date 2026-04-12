using System;

namespace LmsTests.Interfaces;

public interface INotificationService
{
    Task SendEnrollmentConfirmationAsync(string email, string courseTitle);
    Task SendCourseFullNotificationAsync(string courseTitle);
}
