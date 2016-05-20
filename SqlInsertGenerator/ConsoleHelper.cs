using System;
using System.Linq;

namespace SqlInsertGenerator
{
    public class ConsoleHelper
    {
        public static string InputValue(string label)
        {
            Console.Write(label + ": ");
            var value = Console.ReadLine();
            if (string.IsNullOrEmpty(value))
            {
                throw new Exception(string.Format("Input value '{0}' cannot be empty.", label));
            }

            return value;
        }

        public static void WriteSeparator(string text)
        {
            var stars = string.Join("", Enumerable.Repeat("*", text.Length));
            Console.WriteLine("");
            Console.WriteLine(stars);
            Console.WriteLine(text);
            Console.WriteLine(stars);
            Console.WriteLine("");
        }
    }
}
