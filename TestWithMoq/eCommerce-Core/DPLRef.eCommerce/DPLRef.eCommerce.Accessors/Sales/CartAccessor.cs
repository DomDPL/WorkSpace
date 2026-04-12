using DPLRef.eCommerce.Accessors.EntityFramework;
using DPLRef.eCommerce.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using Cart = DPLRef.eCommerce.Accessors.DataTransferObjects.Cart;

namespace DPLRef.eCommerce.Accessors.Sales
{
    internal class CartAccessor : AccessorBase, ICartAccessor
    {
        public Cart ShowCart(int catalogId) => FindCart(catalogId, Context.SessionId);

        public Cart SaveBillingInfo(int catalogId, Address billingAddress)
        {
            EntityFramework.Cart model = null;

            // cannot create a cart without a session id
            if (Context.SessionId == Guid.Empty)
            {
                return null;
            }

            using (var db = eCommerceDbContext.Create())
            {
                model = db.Carts.Find(Context.SessionId);

                if (model == null)
                {
                    model = new EntityFramework.Cart();
                    _ = db.Carts.Add(model);
                }

                DTOMapper.MapBilling(billingAddress, model);
                model.CatalogId = catalogId;
                model.Id = Context.SessionId;

                _ = db.SaveChanges();
            }

            return FindCart(catalogId, model.Id);
        }

        public Cart SaveShippingInfo(int catalogId, Address shippingAddress)
        {
            EntityFramework.Cart model = null;

            // cannot create a cart without a session id
            if (Context.SessionId == Guid.Empty)
            {
                return null;
            }

            using (var db = eCommerceDbContext.Create())
            {
                model = db.Carts.Find(Context.SessionId);

                if (model == null)
                {
                    model = new EntityFramework.Cart();
                    _ = db.Carts.Add(model);
                }

                DTOMapper.MapShipping(shippingAddress, model);
                model.CatalogId = catalogId;
                model.Id = Context.SessionId;

                _ = db.SaveChanges();
            }

            return FindCart(catalogId, model.Id);
        }

        public Cart SaveCartItem(int catalogId, int productId, int quantity)
        {
            EntityFramework.Cart model = null;

            // cannot create a cart without a session id
            if (Context.SessionId == Guid.Empty)
            {
                return null;
            }

            using (var db = eCommerceDbContext.Create())
            {
                model = db.Carts.Find(Context.SessionId);

                if (model == null)
                {
                    model = new EntityFramework.Cart
                    {
                        CatalogId = catalogId,
                        Id = Context.SessionId
                    };
                    _ = db.Set<EntityFramework.Cart>().Add(model);
                    _ = db.SaveChanges();
                }

                var cartItemModel =
                    (from ci in db.CartItems
                        where ci.CartId == Context.SessionId && ci.ProductId == productId
                        select ci).FirstOrDefault();

                if (quantity == 0)
                {
                    if (cartItemModel != null)
                    {
                        // we need to remove this item
                        _ = db.CartItems.Remove(cartItemModel);
                    }
                }
                else if (cartItemModel == null)
                {
                    // we need to add this item
                    cartItemModel = new CartItem
                    {
                        CartId = model.Id,
                        CatalogId = catalogId,
                        ProductId = productId
                    };
                    _ = db.CartItems.Add(cartItemModel);
                }

                if (cartItemModel != null)
                {
                    cartItemModel.Quantity = quantity;
                }

                _ = db.SaveChanges();
            }

            return FindCart(catalogId, model.Id);
        }

        public bool DeleteCart(int catalogId)
        {
            var result = false;

            var sessionId = Context.SessionId;
            if (sessionId != Guid.Empty)
            {
                using var db = eCommerceDbContext.Create();
                var model = db.Carts.Find(sessionId);
                // gracefully handle situation where the cart id does not exist
                // or there is a catalog id mismatch
                if (model != null && model.CatalogId == catalogId)
                {
                    var cartItems = db.CartItems.Where(ci => ci.CartId == sessionId);

                    if (cartItems.Any())
                    {
                        foreach (var item in cartItems)
                        {
                            _ = db.CartItems.Remove(item);
                        }

                        _ = db.SaveChanges(); // delete so FK allows delete of carts
                    }

                    _ = db.Carts.Remove(model);
                }

                _ = db.SaveChanges();
                result = true; // we deleted a cart
            }

            return result;
        }

        private static Cart FindCart(int catalogId, Guid id)
        {
            Cart cart = null;
            if (id != Guid.Empty)
            {
                using var db = eCommerceDbContext.Create();
                var model = db.Carts.Find(id);

                // gracefully handle situation where the cart id does not exist
                // or there is a catalog id mismatch
                if (model != null && model.CatalogId == catalogId)
                {
                    cart = DTOMapper.Map<Cart>(model);

                    var cartItemModels = from ci in db.CartItems
                                         join p in db.Products on ci.ProductId equals p.Id
                                         where ci.CartId == cart.Id
                                         select new { Model = ci, p.Name };

                    var cartItems = new List<DataTransferObjects.CartItem>();

                    foreach (var cim in cartItemModels)
                    {
                        var cartitem = DTOMapper.Map<DataTransferObjects.CartItem>(cim.Model);
                        cartitem.ProductName = cim.Name;
                        cartItems.Add(cartitem);
                    }

                    cart.CartItems = cartItems.ToArray();
                }
            }

            return cart;
        }
    }
}