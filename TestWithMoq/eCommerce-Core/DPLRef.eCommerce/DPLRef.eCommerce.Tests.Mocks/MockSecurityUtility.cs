using DPLRef.eCommerce.Utilities;

namespace DPLRef.eCommerce.Tests.Mocks
{
    public class MockSecurityUtility : MockBase, ISecurityUtility
    {
        public MockSecurityUtility(MockData data) : base(data)
        {
        }

        public bool BackOfficeAdminAuthenticated() => !string.IsNullOrWhiteSpace(MockData.Context.AuthToken);

        public bool SellerAuthenticated() => !string.IsNullOrWhiteSpace(MockData.Context.AuthToken);

        public string TestMe(string input) => input;
    }
}