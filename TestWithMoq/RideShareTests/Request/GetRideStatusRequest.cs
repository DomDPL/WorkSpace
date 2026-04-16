namespace RideShareTests.Request;

public record class GetRideStatusRequest : RideRequest
{
    public int RideId { get; init; }

    public GetRideStatusRequest(int riderId, int rideId) : base(riderId)
    {
        RideId = rideId;
    }
}
