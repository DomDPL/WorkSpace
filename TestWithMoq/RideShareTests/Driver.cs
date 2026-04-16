namespace RideShareTests;

public record class Driver
{
    public int Id { get; init; }
    public string? Name { get; init; }
    public string? Vehicle { get; init; }
    public decimal Rating { get; init; }
    public bool IsAvailable { get; init; }

    public Driver(int id, string? name, string? vehicle, decimal rating, bool isAvailable)
    {
        Id = id;
        Name = name;
        Vehicle = vehicle;
        Rating = rating;
        IsAvailable = isAvailable;
    }
}
