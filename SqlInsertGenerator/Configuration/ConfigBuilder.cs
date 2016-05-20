using System;

namespace SqlInsertGenerator.Configuration
{
    public class ConfigBuilder
    {
        public Config Build(string firstLine, string tableName, string databaseName)
        {
            if (string.IsNullOrEmpty(firstLine))
            {
                throw new Exception("CSV file cannot be empty.");
            }

            var config = new Config { TableName = tableName, DatabaseName = databaseName };

            ConsoleHelper.WriteSeparator("Column headings. Enter '#' for any columns that should be ignored.");

            var values = firstLine.Split(Constants.Separator);
            for (var i = 0; i < values.Length; i++)
            {
                var headingName = ConsoleHelper.InputValue(string.Format("Column {0} heading (example value '{1}')", i, values[i]));
                config.ColumnHeadings.Add(headingName == "#"
                    ? new ColumnHeading { Ignore = true }
                    : new ColumnHeading { Name = headingName });
            }

            return config;
        }
    }
}
