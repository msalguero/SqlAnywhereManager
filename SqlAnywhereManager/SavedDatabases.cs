using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SqlAnywhereManager
{
    public class SavedDatabases
    {
        private List<string> Databases { get; set; }

        public SavedDatabases()
        {
            Databases = new List<string>();
            if (System.IO.File.Exists(@"savedDatabases.json"))
            {
                string json = System.IO.File.ReadAllText(@"savedDatabases.json");

                Databases = JsonConvert.DeserializeObject<List<string>>(json);
            }
            
        }

        public List<string> GetSavedDatabases()
        {
            return Databases;
        }

        public void SaveDatabase(string databaseName)
        {
            if(IsDataBaseSaved(databaseName))
                return;
            Databases.Add(databaseName);
            string json = JsonConvert.SerializeObject(Databases.ToArray());

            System.IO.File.WriteAllText(@"savedDatabases.json", json);
        }

        public void RemoveDatabase(string databaseName)
        {
            Databases.Remove(databaseName);

            string json = JsonConvert.SerializeObject(Databases.ToArray());

            System.IO.File.WriteAllText(@"savedDatabases.json", json);
        }

        public bool IsDataBaseSaved(string dataBaseName)
        {
            if (Databases.Contains(dataBaseName))
                return true;
            return false;
        }
    }
}
