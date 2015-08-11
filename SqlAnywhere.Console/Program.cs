using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqlAnywhere;

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

            var table = "BANCOS";
            var colummns = new string[] {"ID_BANCO", "NOMBRE_BANCO"};
            var filters = new string[] { "ID_BANCO = 1", "NOMBRE_BANCO = FICOHSA" };

            var command = SqlCommandBuilder.CreateSqlQuery(table, colummns, filters);
        }
    }
}
