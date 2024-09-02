using System;

namespace PersonalFinanceApp.Helpers
{
    public static class ConsoleHelper
    {
        public static decimal GetDecimalInput()
        {
            decimal value;
            while (!decimal.TryParse(Console.ReadLine(), out value) || value <= 0)
            {
                Console.WriteLine("Please enter a valid positive number.");
            }
            return value;
        }
    }
}
