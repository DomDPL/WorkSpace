using DPLRef.eCommerce.Accessors.DataTransferObjects;
using System.Text;

namespace DPLRef.eCommerce.Engines.Notification
{
    internal class EmailFormattingEngine : EngineBase, IEmailFormattingEngine
    {
        public string FormatOrderEmailBody(Order order, Seller seller)
        {
            var sb = new StringBuilder();

            _ = sb.AppendLine("Order Notification Email");
            _ = sb.AppendLine("==============================");
            _ = sb.AppendLine($"Order ID: {order.Id}");
            _ = sb.AppendLine($"Webstore: {seller.Name}");
            _ = sb.AppendLine($"Status: {order.Status}");
            _ = sb.AppendLine($"Shipping Provider: {order.ShippingProvider}");
            _ = sb.AppendLine($"Tracking Code: {order.TrackingCode}");
            _ = sb.AppendLine("==============================");
            foreach (var orderLine in order.OrderLines)
            {
                _ = sb.AppendLine(
                    $"Product: {orderLine.ProductName}, Quantity: {orderLine.Quantity}, Unit Price: ${orderLine.UnitPrice}, Extended Price: ${orderLine.ExtendedPrice}");
            }

            _ = sb.AppendLine("==============================");
            _ = sb.AppendLine($"Subtotal: ${order.SubTotal}");
            _ = sb.AppendLine($"Tax: ${order.TaxAmount}");
            _ = sb.AppendLine($"Total: ${order.Total}");

            return sb.ToString();
        }
    }
}