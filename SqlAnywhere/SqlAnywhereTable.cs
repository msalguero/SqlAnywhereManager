using System.Collections.Generic;

namespace SqlAnywhere
{
    public class SqlAnywhereTable  
    {
        public List<string> Columns { get; set; }
        public List<object> Rows { get; set; }

        public SqlAnywhereTable()
        {
            Columns = new List<string>();
            Rows = new List<object>();
        }
    }
}