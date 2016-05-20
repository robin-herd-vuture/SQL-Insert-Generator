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

            var headingsList = GetHeadingsList(config);

            foreach (var line in lines)
            {
                var values = line.Split(',');
                for (var i = 0; i < values.Length; i++)
                {
                    var heading = config.ColumnHeadings.ElementAt(i);
                    if (!heading.Ignore)
                    {
                        var value = values[i];
                    }
                }
            }
        }

        static string FormatValue(string value)
        {
            var replaced = value.Replace("'", "''");
            return string.Format("'{0}'", replaced);
        }

        // e.g. "([EmailId], [FieldName], [FieldValue])"
        static string GetHeadingsList(Config config)
        {
            var headings = config.ColumnHeadings.Where(x => !x.Ignore).Select(x => string.Format("[{0}]", x.Name));
            return string.Format("({0})", string.Join(", ", headings));
        }
    }
}
