using DPLRef.eCommerce.Common.Contracts;
using System;

namespace DPLRef.eCommerce.Client.BackOfficeAdmin
{
    internal class Program
    {
        private static BaseUICommand[] commands;

        private static void Main()
        {
            Console.WriteLine("Starting BackOffice");

            var context = new AmbientContext {SellerId = 2, AuthToken = "MyToken"};

            commands = new BaseUICommand[]
            {
                new TotalsCommand(context)
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