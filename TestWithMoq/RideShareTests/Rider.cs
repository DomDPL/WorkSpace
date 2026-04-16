namespace RideShareTests;

public record Rider
{
    public int Id { get; init; }
    public string? Name { get; init; }
    public string? Phone { get; init; }
    public decimal Rating { get; init; }
}
