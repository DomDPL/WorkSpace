namespace RideShareTests.Request;

public record BookRideRequest : RideRequest
{
    public string? PickupLocation { get; init; }
    public string? DropoffLocation { get; init; }
    public decimal EstimatedFare { get; init; }

    public BookRideRequest(int riderId, string? pickupLocation, string? dropoffLocation, decimal estimatedFare)
        : base(riderId)
    {
        PickupLocation = pickupLocation;
        DropoffLocation = dropoffLocation;
        EstimatedFare = estimatedFare;
    }
}
