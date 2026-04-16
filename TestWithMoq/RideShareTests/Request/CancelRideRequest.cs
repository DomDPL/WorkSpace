namespace RideShareTests.Request;

public record class CancelRideRequest : RideRequest
{
    public int RideId { get; init; }
    public string? Reason { get; init; }

    public CancelRideRequest(int riderId, int rideId, string? reason) : base(riderId)
    {
        RiderId = riderId;
        RideId = rideId;
        Reason = reason;
    }
}
