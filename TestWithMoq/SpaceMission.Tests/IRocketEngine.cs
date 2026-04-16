using System;

namespace SpaceMission.Tests;

public interface IRocketEngine
{
    Task<bool> CheckEnginesAsync(string rocketId);
    Task<int> GetFuelLevelAsync(string rocketId);
    Task LaunchAsync(string rocketId);
}
