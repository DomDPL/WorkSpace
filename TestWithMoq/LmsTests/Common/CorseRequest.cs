using System;

namespace LmsTests.Common;

public abstract record CorseRequest
{
    public int StudentId { get; set; }
    public int CourseId { get; set; }
}
