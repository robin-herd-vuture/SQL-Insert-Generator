using System.Collections.Generic;

namespace SqlInsertGenerator.Configuration
{
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
}
