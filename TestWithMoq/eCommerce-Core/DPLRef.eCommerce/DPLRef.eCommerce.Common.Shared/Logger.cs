using System;

namespace DPLRef.eCommerce.Common.Shared
{
    public class Logger
    {
        public void Error(Exception ex) => Console.WriteLine(ex.Message);

        public void Error(string message) => Console.WriteLine(message);

        public void Error(string message, Exception ex) => Console.WriteLine($"{message}: {ex.Message}");

        public void Info(string message) => Console.WriteLine(message);

        public void Debug(string message) => Console.WriteLine(message);
    }
}