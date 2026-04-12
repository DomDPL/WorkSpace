using System;

namespace LmsTests.Common;

public record EnrolStudentRequest : CorseRequest
{
    public int Id { get; set; }
    public DateTime EnrolledDate { get; set; }
    public bool IsCompleted { get; set; }
    public string StudentEmail { get; set; } = string.Empty;
    public string CourseTitle { get; set; } = string.Empty;
   
   public EnrolStudentRequest() { } // StudentId and CourseId are inherited from CorseRequest, so they can be set directly when creating an instance of EnrolStudentRequest.

    // public EnrolStudentRequest(int studentId, int courseId)
    // {
    //     StudentId = studentId;
    //     CourseId = courseId;
    // }
}
