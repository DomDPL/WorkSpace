using DPLRef.eCommerce.Accessors.DataTransferObjects;
using DPLRef.eCommerce.Accessors.Sales;

namespace DPLRef.eCommerce.Tests.Mocks
{
    public class MockShippingAccessor : MockBase, IShippingAccessor
    {
        public MockShippingAccessor(MockData data) : base(data)
        {
        }

        public ShippingResult RequestShipping(int orderId)
        {
            MockData.OrderShippingRequested = true;
            return MockData.ShippingResult;
        }

        public string TestMe(string input) => input;
    }
}