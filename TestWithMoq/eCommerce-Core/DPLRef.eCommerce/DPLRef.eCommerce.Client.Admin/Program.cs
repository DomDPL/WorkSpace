using DPLRef.eCommerce.Common.Contracts;
using System;

namespace DPLRef.eCommerce.Client.Admin
{
    public class Program
    {
        private static BaseUICommand[] commands;

        public static void Main()
        {
            Console.WriteLine("Starting Admin");

            var context = new AmbientContext {SellerId = 2, AuthToken = "MyToken"}; // this is used for a fresh seller
            var orderContext = new AmbientContext
                {SellerId = 1, AuthToken = "MyToken"}; // this is used for the seller where orders get created

            commands = new BaseUICommand[]
            {
                new FindCatalogsCommand(context),
                new QueryWithBadTokenCommand(context),
                new ShowCatalogCommand(context),
                new CreateCatalogCommand(context),
                new ShowProductCommand(context),
                new CreateProductCommand(context),
                new TotalsCommand(context),
                new OrdersToFulfillCommand(orderContext),
                new FulfillOrderCommand(orderContext)
            };

            // get the first menu selection
            var menuSelection = InitConsoleMenu();

            while (menuSelection != 99)
            {
                if (menuSelection < commands.Length)
                {
                    var cmd = commands[menuSelection];
                    cmd?.Run();
                }
                else
                {
                    Console.WriteLine("Invalid Command");
                }

                // re-initialize the menu selection
                menuSelection = InitConsoleMenu();
            }
        }

        private static int InitConsoleMenu()
        {

            Console.WriteLine("Select desired option:");

            for (var i = 0; i < commands.Length; i++)
            {
                var cmd = commands[i];
                Console.WriteLine($" {i}: {cmd.Name}");
            }

            Console.WriteLine(" 99: exit");
            var selection = Console.ReadLine();
            if (int.TryParse(selection, out int result) == false)
            {
                result = 0;
            }

            return result;
        }
    }
}