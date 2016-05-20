using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SqlInsertGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            WriteSeparator("Welcome to the SQL INSERT generator.");

            var path = InputValue("CSV file name");
            var db = InputValue("Database name");
            var table = InputValue("Table name");
            
            var config = new Config {TableName = table, DatabaseName = db};
            
            var lines = File.ReadAllLines(path);
            var first = lines.FirstOrDefault();
            if (string.IsNullOrEmpty(first))
            {
                throw new Exception("CSV file cannot be empty.");
            }

            WriteSeparator("Column headings. Enter '#' for any columns that should be ignored.");

            var values = first.Split(',');
            for (var i = 0; i < values.Length; i++)
            {
                var headingName = InputValue(string.Format("Column {0} heading (example value '{1}')", i, values[i]));
                if (headingName == "#")
                {
                    config.ColumnHeadings.Add(new ColumnHeading{Ignore = true});
                }
                else
                {
                    config.ColumnHeadings.Add(new ColumnHeading {Name = headingName});
                }
            }

        }

        static string InputValue(string label)
        {
            Console.Write(label + ": ");
            var value = Console.ReadLine();
            if (string.IsNullOrEmpty(value))
            {
                throw new Exception(string.Format("Input value '{0}' cannot be empty."));
            }

            return value;
        }

        static void WriteSeparator(string text)
        {
            var stars = string.Join("", Enumerable.Repeat("*", text.Length));
            Console.WriteLine("");
            Console.WriteLine(stars);
            Console.WriteLine(text);
            Console.WriteLine(stars);
            Console.WriteLine("");
        }
    }

    public class Config
    {
        public Config()
        {
            ColumnHeadings = new List<ColumnHeading>();
        }

        public IList<ColumnHeading> ColumnHeadings { get; set; }

        public string TableName { get; set; }

        public string DatabaseName { get; set; }
    }

    public class ColumnHeading
    {
        public bool Ignore { get; set; }

        public string Name { get; set; }
    }
}
