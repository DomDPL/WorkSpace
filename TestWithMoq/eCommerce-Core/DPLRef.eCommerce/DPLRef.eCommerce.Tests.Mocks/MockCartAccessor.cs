using DPLRef.eCommerce.Accessors.DataTransferObjects;
using DPLRef.eCommerce.Accessors.Sales;
using DPLRef.eCommerce.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DPLRef.eCommerce.Tests.Mocks
{
    public class MockCartAccessor : MockBase, ICartAccessor
    {
        public MockCartAccessor(MockData data) : base(data)
        {
        }

        public bool DeleteCart(int catalogId)
        {
            var cart = MockData.Carts.FirstOrDefault(x => x.Id == MockData.Context.SessionId);
            if (cart != null)
            {
                _ = MockData.Carts.Remove(cart);
                MockData.CartDeleted = true;
                return true;
            }

            return false;
        }

        public Cart SaveBillingInfo(int catalogId, Address billingAddress)
        {
            if (billingAddress == MockData.MyBadAddress)
            {
                throw new Exception();
            }

            var cart = MockData.Carts.FirstOrDefault(x => x.Id == MockData.Context.SessionId);
            cart.BillingAddress = billingAddress;
            return cart;
        }

        public Cart SaveCartItem(int catalogId, int productId, int quantity)
        {
            if (quantity < 0)
            {
                throw new Exception();
            }

            var cart = MockData.Carts.FirstOrDefault(x => x.Id == MockData.Context.SessionId);
            var list = new List<CartItem>(cart.CartItems);

            var product = MockData.Products.FirstOrDefault(p => p.Id == productId);
            var productName = product != null ? product.Name : "";

            var existing = list.FirstOrDefault(x => x.ProductId == productId);
            if (existing != null)
            {
                if (quantity == 0)
                {
                    _ = list.Remove(existing);
                }
                else
                {
                    existing.Quantity = quantity;
                }
            }
            else
            {
                if (quantity > 0)
                {
                    list.Add(new CartItem
                    {
                        ProductId = productId,
                        Quantity = quantity,
                        ProductName = productName
                    });
                }
            }

            cart.CartItems = list.ToArray();

            return cart;
        }

        public Cart SaveShippingInfo(int catalogId, Address shippingAddress)
        {
            if (shippingAddress == MockData.MyBadAddress)
            {
                throw new Exception();
            }

            var cart = MockData.Carts.FirstOrDefault(x => x.Id == MockData.Context.SessionId);
            cart.ShippingAddress = shippingAddress;
            return cart;
        }

        public Cart ShowCart(int catalogId)
        {
            if (MockData.Context.SessionId == MockData.MyBadSessionId)
            {
                throw new Exception();
            }

            var cart = MockData.Carts.FirstOrDefault(x => x.Id == MockData.Context.SessionId);
            return cart;
        }

        public string TestMe(string input) => input;
    }
}