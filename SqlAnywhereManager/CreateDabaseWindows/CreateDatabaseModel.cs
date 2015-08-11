using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlAnywhereManager.CreateDabaseWindows
{
    public class CreateDatabaseModel
    {
        public string DatabasePath { get; set; }
        public string LogPath { get; set; }
        public string PageSize { get; set; }
        public string Collation { get; set; }

        public CreateDatabaseModel()
        {
            Collation = "1252LATIN1";
        }
    }
}
