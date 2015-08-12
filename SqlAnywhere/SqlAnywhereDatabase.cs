using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.Data.SqlClient;
using System.Runtime.InteropServices;

namespace SqlAnywhere
{
    public class SqlAnywhereDatabase
    {
        private string _connectionString;
        private OdbcConnection _connection;
        public string UserId { get; set; }
        public string Password { get; set; }

        private SqlAnywhereDatabase()
        {
            
        }

        public string Name { get; set; }

        public void SetConnectionString(string dataBase, string userId, string password)
        {
            _connectionString = @"Driver={SQL Anywhere 16};databasename="+dataBase+";enginename=Server;uid="+userId+";pwd="+password;
            UserId = userId;
            Password = password;
        }

        public void Connect()
        {
            _connection = new OdbcConnection(_connectionString);
            _connection.Open();
        }

        public static SqlAnywhereDatabase GetDatabase(string dataBaseName, string userId, string password)
        {
            var dataBase = new SqlAnywhereDatabase();
            dataBase.Name = dataBaseName;
            dataBase.SetConnectionString(dataBaseName, userId, password);

            try
            {
                dataBase.Connect();
                return dataBase;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public void Disconnect()
        {
            _connection.Close();
        }

        public List<string> GetTables()
        {
            var tableList = new List<string>();
            using (_connection)
            {
                var cmd = new OdbcCommand("SELECT * FROM SYS.SYSTAB where creator = USER_ID();", _connection);

                using (var reader = cmd.ExecuteReader())
                {
                    for (int ordinal = 0; ordinal < reader.FieldCount; ordinal++)
                    {
                        var x = reader.GetName(ordinal);
                    }
                   
                    while (reader.Read())
                    {
                        for (var i = 0; i < reader.FieldCount; i++)
                        {
                            // option here is to read a name of the column
                            Console.WriteLine(reader[i]);
                            
                            tableList.Add(reader[i].ToString());
                        }
                    }
                }
            }
            return tableList;
        }

        public SqlAnywhereTable GetDataFromTable(string tableName)
        {
                var cmd = new OdbcCommand("SELECT * FROM "+tableName+";", _connection);

                return ReadCommand(cmd);
        }

        public void ExecuteNonQueryCommand(string command)
        {
            var cmd = new OdbcCommand(command, _connection);
            cmd.ExecuteNonQuery();
        }

        public SqlAnywhereTable ExecuteQueryCommand(string command)
        {
            var cmd = new OdbcCommand(command, _connection);
            return ReadCommand(cmd);
        }

        public SqlAnywhereTable ReadCommand(OdbcCommand cmd)
        {
            var table = new SqlAnywhereTable();

            using (var reader = cmd.ExecuteReader())
            {
                for (int ordinal = 0; ordinal < reader.FieldCount; ordinal++)
                {
                    table.Columns.Add(reader.GetName(ordinal));
                }

                while (reader.Read())
                {
                    var row = new string[reader.FieldCount];
                    for (var i = 0; i < reader.FieldCount; i++)
                    {
                        row[i] = reader[i].ToString();
                    }
                    table.Rows.Add(row);
                }
            }

            return table;
        }
    }
}
/*
CONNECT
TO demo16
USER DBA
IDENTIFIED BY sql*/