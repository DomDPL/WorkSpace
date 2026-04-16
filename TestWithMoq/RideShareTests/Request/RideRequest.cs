namespace RideShareTests.Request;

public abstract record RideRequest
{
    public int RiderId { get; init; }

    protected RideRequest(int riderId)
    {
        RiderId = riderId;
    }

}

