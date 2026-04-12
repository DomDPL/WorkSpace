using DPLRef.eCommerce.Accessors.Catalog;
using DPLRef.eCommerce.Accessors.DataTransferObjects;
using System;
using System.Linq;

namespace DPLRef.eCommerce.Tests.Mocks
{
    public class MockCatalogAccessor : MockBase, ICatalogAccessor
    {
        public MockCatalogAccessor(MockData data) : base(data)
        {
        }

        public void DeleteCatalog(int id)
        {
            var catalog = MockData.Catalogs.FirstOrDefault(c => c.Id == id);
            _ = MockData.Catalogs.Remove(catalog);
        }

        public void DeleteProduct(int catalogId, int id)
        {
            var product = MockData.Products.FirstOrDefault(p => p.Id == id);
            _ = MockData.Products.Remove(product);
        }

        public WebStoreCatalog Find(int catalogId) => catalogId == 99 ? throw new Exception() : MockData.Catalogs.FirstOrDefault(p => p.Id == catalogId);

        public Product[] FindAllProductsForCatalog(int catalogId) => MockData.Products.Where(p => p.CatalogId == catalogId).ToArray();

        public WebStoreCatalog[] FindAllSellerCatalogs() => MockData.Catalogs.ToArray();

        public Product FindProduct(int id) => MockData.Products.FirstOrDefault(p => p.Id == id);

        public WebStoreCatalog SaveCatalog(WebStoreCatalog catalog)
        {
            if (catalog.Id == 0)
            {
                catalog.Id = MockData.Catalogs.Count > 0 ? MockData.Catalogs.Max(x => x.Id) + 1 : 1;
            }

            var found = MockData.Catalogs.FirstOrDefault(x => x.Id == catalog.Id);
            if (found != null)
            {
                _ = MockData.Catalogs.Remove(found);
            }

            MockData.Catalogs.Add(catalog);

            return catalog;
        }

        public Product SaveProduct(int catalogId, Product product)
        {
            if (product.Id == 0)
            {
                product.Id = MockData.Products.Max(x => x.Id) + 1;
            }

            var found = MockData.Products.FirstOrDefault(x => x.Id == product.Id);
            if (found != null)
            {
                _ = MockData.Products.Remove(found);
                MockData.Products.Add(product);
            }

            return product;
        }

        public string TestMe(string input) => input;
    }
}