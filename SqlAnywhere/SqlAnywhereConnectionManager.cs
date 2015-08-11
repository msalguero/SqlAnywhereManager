using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlAnywhere
{
    public class SqlAnywhereConnectionManager
    {
        public List<SqlAnywhereDatabase> Databases;
        private SqlAnywhereDatabase _currentDatabase;
        public string UserId { get; set; }
        public string Password { get; set; }

        public SqlAnywhereConnectionManager(string userId, string password)
        {
            _currentDatabase = null;
            UserId = userId;
            Password = password;
            Databases = new List<SqlAnywhereDatabase>();
            
        }

        public bool ConnectToDatabase(string databaseName)
        {
            var database = SqlAnywhereDatabase.GetDatabase(databaseName, UserId, Password);
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
                _currentDatabase = null;
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
    }
}
