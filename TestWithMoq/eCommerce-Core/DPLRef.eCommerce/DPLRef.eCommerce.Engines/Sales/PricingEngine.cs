using DPLRef.eCommerce.Accessors.Catalog;
using DPLRef.eCommerce.Accessors.DataTransferObjects;
using DPLRef.eCommerce.Contracts.WebStore.Sales;
using System;

namespace DPLRef.eCommerce.Engines.Sales
{
    internal class PricingEngine : EngineBase, ICartPricingEngine
    {
        public override string TestMe(string input)
        {
            input = base.TestMe(input);
            input = AccessorFactory.CreateAccessor<ICatalogAccessor>().TestMe(input);
            return input;
        }

        public WebStoreCart GenerateCartPricing(Cart cart)
        {
            var result = new WebStoreCart();
            // map the cart input to a webstore cart
            DTOMapper.Map(cart, result);

            // loop through all cart items, get the current price and apply to the cart item
            var catAccessor = AccessorFactory.CreateAccessor<ICatalogAccessor>();

            foreach (var item in result.CartItems)
            {
                if (item.Quantity > 0)
                {
                    // get the current unit price and update the cart item
                    var product = catAccessor.FindProduct(item.ProductId);
                    if (product != null)
                    {
                        var unitPrice = product.Price;
                        var extendedPrice = Math.Round(unitPrice * item.Quantity, 2);

                        // update the web store cart
                        item.UnitPrice = unitPrice;
                        item.ExtendedPrice = extendedPrice;

                        // add the amount to the subtotal
                        result.SubTotal += Math.Round(extendedPrice, 2);
                    }
                    else
                    {
                        Logger.Error("Invalid Product Id");
                        throw new ArgumentException("Invalid Product Id");
                    }
                }
                else
                {
                    Logger.Error("Invalid item quantity");
                    throw new ArgumentException("Invalid item quantity");
                }
            }

            // set the cart total to the subtotal
            result.Total = result.SubTotal;

            return result;
        }
    }
}