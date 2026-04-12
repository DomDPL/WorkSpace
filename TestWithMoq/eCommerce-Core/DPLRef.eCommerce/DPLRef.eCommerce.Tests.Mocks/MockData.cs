using DPLRef.eCommerce.Accessors.DataTransferObjects;
using DPLRef.eCommerce.Common.Contracts;
using System;
using System.Collections.Generic;

namespace DPLRef.eCommerce.Tests.Mocks
{
    public class MockData
    {
        public static readonly Guid MySessionId = new("aaaaaaaa-bbbb-cccc-dddd-eeeeeeeeeeee");
        public static readonly Guid MyBadSessionId = new("11111111-bbbb-cccc-dddd-eeeeeeeeeeee");
        public static readonly Guid MyBothInfoSessionId = new("ffffffff-bbbb-cccc-dddd-eeeeeeeeeeee");
        public static readonly Guid MySessionIdForOrder = new("aaaaaaaa-bbbb-cccc-dddd-eeeeeeeeeeef");

        public static readonly Address MyAddress = new()
        {
            Addr1 = "4771 Snowbird Lane",
            Addr2 = "",
            City = "Hooper",
            First = "Neil",
            Last = "Diaz",
            Postal = "68512",
            State = "Nebraska"
        };

        public static readonly Address MySameAddress = new()
        {
            Addr1 = "2498 Post Avenue",
            Addr2 = "",
            City = "Lincoln",
            First = "Jeanine",
            Last = "Jeanine M Gilbert",
            Postal = "68512",
            State = "Nebraska"
        };

        public static readonly Address MyBadAddress = new();
        public bool CartDeleted;

        public bool OrderCreated;
        public bool OrderSucceeded;

        public SellerSalesTotal SellerSalesTotal = new()
        {
            OrderCount = 1,
            OrderTotal = 10.0m
        };

        public AmbientContext Context { get; set; }
        public bool AsyncCalled { get; set; }
        public bool ForceException { get; set; }
        public bool ForceCaptureFail { get; set; }

        public bool OrderCaptureAttempted { get; set; }
        public bool OrderCapturedStatus { get; set; }
        public bool OrderShippingRequested { get; set; }
        public bool ForceShippingFail { get; set; }
        public bool OrderFulfilled { get; set; }
        public Order OrderToFulfill { get; set; }

        public List<WebStoreCatalog> Catalogs { get; set; } = new()
        {
            new()
            {
                Id = 1,
                Name = "My Webstore"
            },
            new()
            {
                Id = 2,
                Name = "My Second Webstore"
            }
        };

        public List<Product> Products { get; set; } = new()
        {
            new()
            {
                Id = 1,
                Name = "My Product",
                Summary = "My Product Summary",
                CatalogId = 1,
                Price = 1.50m
            },
            new()
            {
                Id = 2,
                Name = "My Second Product",
                Summary = "My Second Product Summary",
                CatalogId = 1
            }
        };

        public List<Cart> Carts { get; set; } = new()
        {
            new()
            {
                Id = new Guid("aaaaaaaa-bbbb-cccc-dddd-eeeeeeeeeeee"),
                CartItems = Array.Empty<CartItem>()
            },
            new()
            {
                Id = MySessionIdForOrder,
                BillingAddress = MyAddress,
                ShippingAddress = MyAddress,
                CartItems = new[]
                {
                    new CartItem
                    {
                        ProductId = 1,
                        ProductName = "Mock Product Name",
                        Quantity = 1
                    }
                }
            }
        };

        public List<Order> Orders { get; set; } = new();

        public List<Seller> Sellers { get; set; } = new()
        {
            new Seller()
            {
                Id = 1,
                Name = "Test Seller"
            }
        };

        public List<SellerSalesTotal> SellerSalesTotaList { get; set; } = new()
        {
            new SellerSalesTotal()
            {
                SellerId = 1,
                SellerName = "UNIT TEST",
                OrderCount = 1,
                OrderTotal = 10.0M
            }
        };

        public List<SellerOrderData> SellerOrderData { get; set; } = new()
        {
            new SellerOrderData()
            {
                SellerId = 1,
                SellerName = "UNIT TEST",
                OrderCount = 1,
                OrderTotal = 10.0M
            }
        };

        public ShippingResult ShippingResult { get; set; } = new()
        {
            Success = false,
            ShippingProvider = "",
            TrackingCode = ""
        };
    }
}