using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlAnywhere
{
    public class SqlCommandBuilder
    {
        public static string CreateSqlQuery(string tableName, string[] columns, string[] filters)
        {
            string command = "SELECT ";

            if (columns.Count() != 0)
            {
                foreach (var column in columns)
                {
                    command = command + column;
                    if(columns[columns.Count()-1] != column)
                        command = command + ", ";
                }
            }
            else
            {
                command = command + "* ";
            }

            command = command +" FROM "+ tableName + " ";

            if (filters.Count() != 0)
            {
                command = command + "WHERE ";
                foreach (var filter in filters)
                {
                    command = command + filter;
                    if (filters[filters.Count() - 1] != filter)
                        command = command + " AND ";
                }
            }

            command = command + ";";
            return command;
        }

        public static string GenerateDatabaseDdl(string databasePath, string logPath, string collation, string page)
        {
            var command = "CREATE DATABASE '"+databasePath+"' LOG ON '"+logPath+"' PAGE SIZE "
                +page+" COLLATION '"+collation+"' NCHAR COLLATION";
            return command;
        }

        public static string GenerateDatabaseInit(string databasePath, string logPath, string collation, string page)
        {
            var command = "dbinit -p " + page + " -z \"" + collation + "\" -zn \"UCA\" -t \""
                + logPath + "\" \"" + databasePath + "\"";
            return command;
        }
    }
}
