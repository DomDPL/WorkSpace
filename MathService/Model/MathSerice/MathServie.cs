using System.Formats.Asn1;

namespace Model.MathSerice;

public class MathService
{
    public int Add(int a, int b)
    {
        return a + b;
    }

    public int Subtract(int a, int b)
    {
        return a - b;
    }

    public int Multiply(int a, int b)
    {
        return a * b;
    }

    public double Divide(int a, int b)
    {
        if (b == 0)
            throw new DivideByZeroException("Cannot divide by zero.");
        return (double)a / b;
    }

    public bool IsEven(int number)
    {
        return number % 2 == 0;
    }

    public int GetMax(int a, int b)
    {
        return a > b ? a : b;
    }
    // Bonus 
    public double DivideByAero(int a, int b)
    {
        try
        {

            return Divide(a, b);
        }
        catch (DivideByZeroException)
        {
            Console.WriteLine("Cannot divide by zero. Returning 0 as default value.");
            return 0; // Return a default value or handle it as needed
        }
    }
    public int AddNegativeNumbers(int a, int b)
    {
        if (a < 0 && b < 0)
        {
            return a + b;
        }
        return Add(a, b);
    }
}