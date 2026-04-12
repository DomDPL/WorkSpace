using DPLRef.eCommerce.Accessors.DataTransferObjects;
using DPLRef.eCommerce.Accessors.Sales;
using DPLRef.eCommerce.Common.Contracts;
using System;
using System.Linq;

namespace DPLRef.eCommerce.Tests.Mocks
{
    public class MockOrderAccessor : MockBase, IOrderAccessor
    {
        public MockOrderAccessor(MockData data) : base(data)
        {
        }

        public Order FindOrder(int id) => MockData.ForceException ? throw new ArgumentException("forced exception") : MockData.Orders.FirstOrDefault(o => o.Id == id);

        public Order FulfillOrder(int orderId, string shippingProvider, string trackingCode, string notes)
        {
            var order = MockData.Orders.FirstOrDefault(o => o.Id == orderId);
            order.Status = OrderStatuses.Shipped;
            order.ShippingProvider = shippingProvider;
            order.TrackingCode = trackingCode;
            MockData.OrderCaptureAttempted = true;
            MockData.OrderFulfilled = true;
            return order;
        }

        public SellerSalesTotal SalesTotal() => MockData.SellerSalesTotal;

        public Order SaveOrder(int catalogId, Order order)
        {
            if (order.Id == 0)
            {
                MockData.OrderCreated = true;
                order.Id = MockData.Orders.Count > 0 ? MockData.Orders.Max(x => x.Id) + 1 : 1;
            }

            var found = MockData.Orders.FirstOrDefault(x => x.Id == order.Id);
            if (found != null)
            {
                _ = MockData.Orders.Remove(found);
            }

            MockData.Orders.Add(order);

            return order;
        }

        public string TestMe(string input) => input;

        public Order[] UnfulfilledOrders()
        {
            var orders = MockData.Orders.Where(o => o.Status == OrderStatuses.Authorized).ToArray();
            return orders;
        }

        public Order UpdateOrderStatus(int orderId, OrderStatuses status, string notes)
        {
            var order = MockData.Orders.FirstOrDefault(o => o.Id == orderId);
            order.Status = status;
            order.Notes = notes;
            if (status == OrderStatuses.Captured)
            {
                MockData.OrderCapturedStatus = true;
            }

            return order;
        }
    }
}