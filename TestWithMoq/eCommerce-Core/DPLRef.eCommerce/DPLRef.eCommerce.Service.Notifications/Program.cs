using DPLRef.eCommerce.Common.Contracts;
using DPLRef.eCommerce.Contracts.ServiceHost.Notifications;
using DPLRef.eCommerce.Managers;
using DPLRef.eCommerce.Utilities;
using System;
using System.Threading;

namespace DPLRef.eCommerce.Service.Notifications
{
    public class Program
    {
        public static void Main()
        {
            Console.WriteLine("Starting Notifications Service");

            while (true)
            {
                var utilityFactory = new UtilityFactory(new AmbientContext());
                var asyncUtility = utilityFactory.CreateUtility<IAsyncUtility>();
                var item = asyncUtility.CheckForNewItem();
                if (item != null)
                {
                    // If the queued message contains a Context then pass it along instead of creating a new one
                    var managerFactory = new ManagerFactory(item.AmbientContext ?? new AmbientContext());
                    var notificationManager = managerFactory.CreateManager<INotificationManager>();

                    if (item.EventType == AsyncEventTypes.OrderSubmitted)
                    {
                        _ = notificationManager.SendNewOrderNotices(item.EventId);
                    }

                    if (item.EventType == AsyncEventTypes.OrderShipped)
                    {
                        _ = notificationManager.SendOrderFulfillmentNotices(item.EventId);
                    }
                }

                Thread.Sleep(5000); // sleep 5 seconds
            }
        }
    }
}