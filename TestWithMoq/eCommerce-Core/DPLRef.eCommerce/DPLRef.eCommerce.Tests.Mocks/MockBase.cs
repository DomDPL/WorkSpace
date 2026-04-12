namespace DPLRef.eCommerce.Tests.Mocks
{
    public abstract class MockBase
    {
        public MockBase(MockData data) => MockData = data;

        public MockData MockData { get; set; }
    }
}