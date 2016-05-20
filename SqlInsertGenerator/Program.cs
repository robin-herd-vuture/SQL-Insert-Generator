using System.Collections.Generic;
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

            var statements = GetAllStatements(config, lines);
        }

        static IEnumerable<string> GetAllStatements(Config config, IEnumerable<string> lines)
        {
            var insertStatement = GetInsertStatement(config);
            foreach (var line in lines)
            {
                var values = line.Split(',');
                var formattedValues = new List<string>();
                for (var i = 0; i < values.Length; i++)
                {
                    var heading = config.ColumnHeadings.ElementAt(i);
                    if (!heading.Ignore)
                    {
                        var value = values[i];
                        formattedValues.Add(FormatValue(value));
                    }
                }

                var valueStatement = string.Format("({0})", string.Join(", ", formattedValues));
                var fullStatement = insertStatement + valueStatement;
                yield return fullStatement;
            }
        }

        static string FormatValue(string value)
        {
            var replaced = value.Replace("'", "''");
            return string.Format("'{0}'", replaced);
        }

        // e.g. "INSERT INTO [Robin].[dbo].[Names] ([EmailId], [FieldName], [FieldValue]) VALUES "
        static string GetInsertStatement(Config config)
        {
            var headings = config.ColumnHeadings.Where(x => !x.Ignore).Select(x => string.Format("[{0}]", x.Name));
            var joinedHeadings = string.Join(", ", headings);
            return string.Format("INSERT INTO [{0}].[dbo].[{1}] ({2}) VALUES ", config.DatabaseName, config.TableName, joinedHeadings);
        }
    }
}
