namespace RideShareTests;

public record class Ride
{
    public int Id { get; init; }
    public int RiderId { get; init; }
    public int DriverId { get; init; }
    public string? PickupLocation { get; init; }
    public string? DropoffLocation { get; init; }
    public decimal Fare { get; init; }
    public DateTime RequestedTime { get; init; }
    public bool IsCompleted { get; init; } = false;

    public Ride()
    {

    }
    public Ride(int id, int riderId, int driverId, string? pickupLocation,
                 string? dropoffLocation, decimal fare, DateTime requestedTime, bool isCompleted)
    {
        Id = id;
        RiderId = riderId;
        DriverId = driverId;
        PickupLocation = pickupLocation;
        DropoffLocation = dropoffLocation;
        Fare = fare;
        RequestedTime = requestedTime;
        IsCompleted = false;
    }
}
