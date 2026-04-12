using DPLRef.eCommerce.Accessors.DataTransferObjects;
using DPLRef.eCommerce.Accessors.Remittance;

namespace DPLRef.eCommerce.Tests.Mocks
{
    public class MockRemittanceAccessor : MockBase, IRemittanceAccessor
    {
        public MockRemittanceAccessor(MockData data) : base(data)
        {
        }

        public SellerOrderData[] SalesTotal() => MockData.SellerOrderData.ToArray();

        public string TestMe(string input) => input;
    }
}