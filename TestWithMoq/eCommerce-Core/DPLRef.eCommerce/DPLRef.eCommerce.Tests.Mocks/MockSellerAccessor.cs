using DPLRef.eCommerce.Accessors.DataTransferObjects;
using DPLRef.eCommerce.Accessors.Remittance;
using System.Linq;

namespace DPLRef.eCommerce.Tests.Mocks
{
    public class MockSellerAccessor : MockBase, ISellerAccessor
    {
        public MockSellerAccessor(MockData data) : base(data)
        {
        }

        public void Delete(int id)
        {
            var catalog = MockData.Sellers.FirstOrDefault(c => c.Id == id);
            _ = MockData.Sellers.Remove(catalog);
        }

        public Seller Find(int id)
        {
            var seller = MockData.Sellers.FirstOrDefault(c => c.Id == id);
            return seller;
        }

        public Seller Save(Seller seller)
        {
            if (seller.Id == 0)
            {
                seller.Id = MockData.Sellers.Count > 0 ? MockData.Sellers.Max(x => x.Id) + 1 : 1;
            }

            var found = MockData.Sellers.FirstOrDefault(x => x.Id == seller.Id);
            if (found != null)
            {
                _ = MockData.Sellers.Remove(found);
            }

            MockData.Sellers.Add(seller);

            return seller;
        }

        public string TestMe(string input) => input;
    }
}