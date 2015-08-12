using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace SqlAnywhere
{
    public class SqlAnywhereConnectionManager
    {
        public List<SqlAnywhereDatabase> Databases;
        private SqlAnywhereDatabase _currentDatabase;

        public SqlAnywhereConnectionManager()
        {
            _currentDatabase = null;
            Databases = new List<SqlAnywhereDatabase>();
            
        }

        public bool ConnectToDatabase(string databaseName, string userId, string password)
        {
            var database = SqlAnywhereDatabase.GetDatabase(databaseName, userId, password);
            if (database != null)
            {
                Databases.Add(database);
                _currentDatabase = database;
                return true;
            }
            return false;
        }

        public bool DisconnectDatabase(string dataBaseName)
        {
            var databaseToDisconnect = Databases.First(database => database.Name == dataBaseName);
            if(databaseToDisconnect != null)
            {
                databaseToDisconnect.Disconnect();
                Databases.Remove(databaseToDisconnect);
                if(Databases.Count != 0)
                    _currentDatabase = Databases.First();
                else
                {
                    _currentDatabase = null;
                }
                return true;
            }
            return false;
        }

        public List<string> GetTables()
        {
            return _currentDatabase.GetTables();
        }

        public List<string> GetData(string objectType)
        {
            if (objectType == "Tables")
                return GetTables();
            return null;
        }

        public SqlAnywhereTable GetDataFromTable(string tableName)
        {
            return _currentDatabase.GetDataFromTable(tableName);
        }

        public void ExecuteSqlCommand(string command)
        {
            _currentDatabase.ExecuteNonQueryCommand(command);
        }

        public SqlAnywhereTable ExecuteSqlQuery(string command)
        {
            return _currentDatabase.ExecuteQueryCommand(command);
        }

        public bool IsConnected(string databaseName)
        {
            return Databases.Any(sqlAnywhereDatabase => sqlAnywhereDatabase.Name == databaseName);
        }

        public bool IsCurrentDatabase(string dataBaseName)
        {
            return _currentDatabase.Name == dataBaseName;
        }

        public string GetCurrentDatabase()
        {
            return _currentDatabase.Name;
        }
    }
}
