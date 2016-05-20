using System.IO;
using System.Linq;
using SqlInsertGenerator.Configuration;

namespace SqlInsertGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            ConsoleHelper.WriteSeparator("Welcome to the SQL INSERT generator.");

            var path = ConsoleHelper.InputValue("CSV file name");
            var db = ConsoleHelper.InputValue("Database name");
            var table = ConsoleHelper.InputValue("Table name");
            
            var lines = File.ReadAllLines(path);
            var first = lines.FirstOrDefault();
            var config = new ConfigBuilder().Build(first, table, db);
        }
    }
}
