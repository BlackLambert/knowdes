using System;
using Mono.Data.Sqlite;
using UnityEngine;
using System.Data;

namespace Knowdes
{
    public class SqliteHelper : IDisposable
    {
        private const string database_name = "KNOWDESDB2.db";
        public string db_connection_string;
        protected IDbConnection _db_connection;

        public SqliteHelper()
        {
            db_connection_string = "URI=file:" + Application.persistentDataPath + "/" + database_name;
            Debug.Log(nameof(SqliteHelper) + " db_connection_string: " + db_connection_string);
            _db_connection = new SqliteConnection(db_connection_string);
            _db_connection.Open();
        }

        public void Dispose()
        {
            closeDataBaseConnection();
        }

        //helper functions
        protected IDbCommand getDbCommand()
        {
            return _db_connection.CreateCommand();
        }

        protected void executeNonQuery(string command)
		{
            IDbCommand dbcmd = getDbCommand();
            dbcmd.CommandText = command;
            dbcmd.ExecuteNonQuery();
        }

        protected IDataReader executeReader(string command)
		{
            IDbCommand dbcmd = getDbCommand();
            dbcmd.CommandText = command;
            return dbcmd.ExecuteReader();
        }

        public bool tableExists(string table)
        {
            try
            {
                IDbCommand dbcmd = _db_connection.CreateCommand();
                dbcmd.CommandText = "SELECT * FROM " + table;
                dbcmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private void closeDataBaseConnection()
        {
            _db_connection.Close();
        }
	}
}