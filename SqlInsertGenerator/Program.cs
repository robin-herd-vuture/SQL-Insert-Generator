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

            var path = ConsoleHelper.InputValue("Tab-delimited file name");
            var db = ConsoleHelper.InputValue("Database name");
            var table = ConsoleHelper.InputValue("Table name");
            
            var lines = File.ReadAllLines(path);
            var first = lines.FirstOrDefault();
            var config = new ConfigBuilder().Build(first, table, db);

            var statements = GetAllStatements(config, lines);

            File.WriteAllLines("output.sql", statements);
        }

        static IEnumerable<string> GetAllStatements(Config config, IEnumerable<string> lines)
        {
            var insertStatement = GetInsertStatement(config);
            var goCounter = 0;
            foreach (var line in lines)
            {
                var values = line.Split(Constants.Separator);
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

                // prevents the following error: There is insufficient system memory in resource pool 'internal' to run this query.
                goCounter++;
                if (goCounter > 100)
                {
                    yield return "GO";
                    goCounter = 0;
                }
            }
        }

        static string FormatValue(string value)
        {
            if (value == "NULL")
            {
                return "NULL";
            }

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
