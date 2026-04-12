using System;

namespace CoffeeShop.Tests;

public interface ICoffeeMachine
{
    Task<bool> MakeCoffeeAsync(CoffeeOrder order);
    Task<int> GetCoffeeStockAsync();
}
