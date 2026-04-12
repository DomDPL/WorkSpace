using DPLRef.eCommerce.Contracts.Admin.Catalog;

namespace DPLref.eCommerce.Tests.IntegrationTests.ExpectedResponses
{
    public static class AdminResponses
    {
        #region Catalog Responses

        public static WebStoreCatalog GenericCatalog =>
            new()
            {
                Id = 1,
                Name = "Santi's Wacky Used Car Emporium",
                Description = "Santi's Wacky Used Car Emporium description"
            };

        public static WebStoreCatalog OverflowCatalog =>
            new()
            {
                Id = 2,
                Name = "Santi's Wacky Used Car Overflow",
                Description = "Santi's Wacky Used Car Overflow description"
            };

        // standard list of catalogs response based upon the seed data
        public static AdminCatalogsResponse FindCatalogsResponse { get; }
            = new()
            {
                Success = true,
                Message = null,
                Catalogs = new[]
                {
                    GenericCatalog,
                    OverflowCatalog
                }
            };

        // "Seller not authenticated"
        public static AdminCatalogsResponse FindCatalogsNoAuthResponse { get; }
            = new()
            {
                Success = false,
                Message = "Seller not authenticated",
                Catalogs = null
            };

        // standard catalog response based upon the seed data
        public static AdminCatalogResponse CatalogResponse { get; }
            = new()
            {
                Success = true,
                Message = null,
                Catalog = GenericCatalog
            };

        // "Catalog not found"
        public static AdminCatalogResponse CatalogNotFoundResponse { get; }
            = new()
            {
                Success = false,
                Message = "Catalog not found",
                Catalog = null
            };

        // "Seller not authenticated"
        public static AdminCatalogResponse CatalogNoAuthResponse { get; }
            = new()
            {
                Success = false,
                Message = "Seller not authenticated",
                Catalog = null
            };

        // standard catalog response based upon updated seed data
        public static AdminCatalogResponse CatalogUpdateResponse { get; }
            = new()
            {
                Success = true,
                Message = null,
                Catalog = new WebStoreCatalog
                {
                    Id = 1,
                    Name = "Santi's Wacky Used Car Emporium",
                    Description = "UPDATED"
                }
            };

        // "Seller Id mismatch"
        public static AdminCatalogResponse CatalogSellerIdMismatchResponse { get; }
            = new()
            {
                Success = false,
                Message = "There was a problem saving the catalog",
                Catalog = null
            };

        #endregion

        #region Product Responses

        // standard product response based upon the seed data
        public static AdminProductResponse ProductResponse { get; }
            = new()
            {
                Success = true,
                Message = null,
                Product = new Product
                {
                    Id = 1003,
                    CatalogId = 2,
                    SellerProductId = null,
                    Name = "2006 Chevrolet Suburban",
                    Summary = "Used 2006 Chevrolet Suburban",
                    Detail = "Used car from Santi's Used Car Emporium",
                    Price = 1011.00m,
                    IsDownloadable = false, // want to be sure we are testing for non-default values
                    IsAvailable = true, // want to be sure we are testing for non-default values
                    SupplierName = "Chevrolet",
                    ShippingWeight = 1.25m
                }
            };

        // "Product not found"
        public static AdminProductResponse ProductNotFoundResponse { get; }
            = new()
            {
                Success = false,
                Message = "Product not found",
                Product = null
            };

        // "Seller not authenticated"
        public static AdminProductResponse ProductNoAuthResponse { get; }
            = new()
            {
                Success = false,
                Message = "Seller not authenticated",
                Product = null
            };

        // standard product response based upon updated seed data
        public static AdminProductResponse ProductUpdatedResponse { get; }
            = new()
            {
                Success = true,
                Message = null,
                Product = new Product
                {
                    Id = 1003,
                    CatalogId = 2,
                    SellerProductId = null,
                    Name = "2006 Chevrolet Suburban",
                    Summary = "TEST_PRODUCT updated summary",
                    Detail = "Used car from Santi's Used Car Emporium",
                    Price = 1011.00m,
                    IsDownloadable = false, // want to be sure we are testing for non-default values
                    IsAvailable = true, // want to be sure we are testing for non-default values
                    SupplierName = "Chevrolet",
                    ShippingWeight = 1.25m
                }
            };

        // "Catalog Id mismatch"
        public static AdminProductResponse ProductCatalogIdMismtachResponse { get; }
            = new()
            {
                Success = false,
                Message = "There was a problem saving the product",
                Product = null
            };

        #endregion
    }
}