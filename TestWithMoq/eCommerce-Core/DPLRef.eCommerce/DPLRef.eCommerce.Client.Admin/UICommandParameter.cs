using System;

namespace DPLRef.eCommerce.Client
{
    internal abstract class UICommandParameter
    {
        public string Message { get; set; }

        public abstract void PromptForValue();
    }

    internal class IntUICommandParameter : UICommandParameter
    {
        public int Value { get; set; }

        public override void PromptForValue()
        {
            Console.WriteLine(Message);
            var str = Console.ReadLine();
            Value = int.TryParse(str, out var tmp) ? tmp : throw new InvalidOperationException("Integer parameter could not be parsed.");
        }
    }

    internal class DecimalUICommandParameter : UICommandParameter
    {
        public decimal Value { get; set; }

        public override void PromptForValue()
        {
            Console.WriteLine(Message);
            var str = Console.ReadLine();

            Value = decimal.TryParse(str, out var tmp) ? tmp : throw new InvalidOperationException("Decimal parameter could not be parsed.");
        }
    }

    internal class StringUICommandParameter : UICommandParameter
    {
        public string Value { get; set; }

        public override void PromptForValue()
        {
            Console.WriteLine(Message);
            Value = Console.ReadLine();
        }
    }
}