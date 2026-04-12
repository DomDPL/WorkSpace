using DPLRef.eCommerce.Accessors.DataTransferObjects;

namespace DPLRef.eCommerce.Engines.Sales
{
    public interface IOrderValidationEngine
    {
        ValidationResponse ValidateOrder(Order order);
    }
}