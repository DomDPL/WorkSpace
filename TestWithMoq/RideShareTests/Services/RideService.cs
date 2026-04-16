using System;
using Moq;
using RideShareTests.Interface;
using RideShareTests.Request;

namespace RideShareTests.Services;

public class RideService
{
    private Mock<IRideRepository>? _repository;
    private Mock<INotificationRepository>? _notificationRepository;

    public RideService(Mock<IRideRepository> rideRepository, Mock<INotificationRepository> notificationRepository)
    {
        _repository = rideRepository;
        _notificationRepository = notificationRepository;
    }
    public async Task<object> ProcessRequestAsync(RideRequest request)
    {
        return request switch
        {
            BookRideRequest bookRequest => await HandleBookRideAsync(bookRequest),
            GetRideStatusRequest statusRequest => await HandleGetRideStatusAsync(statusRequest),
            CancelRideRequest cancelRequest => await HandleCancelRideAsync(cancelRequest),
            _ => throw new InvalidOperationException("Unknown request type")
        };
    }

    private async Task<bool> HandleBookRideAsync(BookRideRequest request)
    {
        var driver = await _repository!.Object.FindAvailableDriverAsync(request.PickupLocation!);
        if (driver == null || !driver.IsAvailable)
            return false;

        var ride = new Ride
        {
            Id = 0,
            RiderId = request.RiderId,
            DriverId = driver.Id,
            PickupLocation = request.PickupLocation,
            DropoffLocation = request.DropoffLocation,
            Fare = request.EstimatedFare,
            RequestedTime = DateTime.UtcNow
        };

        await _repository!.Object.AddRideAsync(ride);
    await _notificationRepository!.Object.SendRideConfirmationAsync(
            "555-1234", request.PickupLocation!, request.DropoffLocation!); // simplified phone

        await _notificationRepository!.Object.SendDriverAssignedNotificationAsync(
        "555-1234", driver.Name!, driver.Vehicle!);

        return true;
    }
    private async Task<bool> HandleCancelRideAsync(CancelRideRequest request)
    {
        var ride = await _repository!.Object.GetRideAsync(request.RideId);
        if (ride == null || ride.IsCompleted)
            return false;

        var updatedRide = ride with { IsCompleted = true }; // records are immutable
        await _repository!.Object.UpdateRideAsync(updatedRide);

    await _notificationRepository!.Object.SendRideCancelledNotificationAsync(
            "555-1234", request.Reason!);

        return true;
    }

    private async Task<string> HandleGetRideStatusAsync(GetRideStatusRequest request)
    {
        var ride = await _repository!.Object.GetRideAsync(request.RideId);
        return ride?.IsCompleted == true ? "Completed" : "In Progress";
    }
}
