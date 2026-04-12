using System;

namespace LmsTests.Common;

public record CheckAvailabilityRequest : CorseRequest
{
    public CheckAvailabilityRequest() { } // StudentId and CourseId are inherited from CorseRequest, so they can be set directly when creating an instance of CheckAvailabilityRequest.
}
