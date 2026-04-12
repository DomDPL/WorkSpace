using DPLRef.eCommerce.Common.Shared;

namespace DPLRef.eCommerce.Engines.Remmitance
{
    public interface IRemittanceCalculationEngine : IServiceContractBase
    {
        RemittanceCalculationResult CalculateFee(int sellerId, decimal total);
    }
}