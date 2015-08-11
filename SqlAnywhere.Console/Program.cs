using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqlAnywhere;
using SqlAnywhereManager;

namespace SqlAnywhere.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            /*var serverManager = new SqlAnywhereConnectionManager("dba", "sql");
            serverManager.ConnectToDatabase("prueba");
            //var db = SqlAnywhereDatabase.GetDatabase("prueba", "dba", "sql");
            System.Console.WriteLine(serverManager.GetTables());
            serverManager.DisconnectDatabase("prueba");
            System.Console.Read();*/

            SavedDatabases saved = new SavedDatabases();
            saved.RemoveDatabase("Base3");
        }
    }
}
