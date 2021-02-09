using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mono.Data.Sqlite;
using UnityEngine;
using System.Data;

namespace Knowdes
{
    public class SqliteHelper : IDisposable
    {
        private const string database_name = "KNOWDESDB2.db";
        public string db_connection_string;
        public IDbConnection db_connection;

        public SqliteHelper()
        {
            db_connection_string = "URI=file:" + Application.persistentDataPath + "/" + database_name;
            Debug.Log(nameof(SqliteHelper) + " db_connection_string: " + db_connection_string);
            db_connection = new SqliteConnection(db_connection_string);
            db_connection.Open();
        }

        public void Dispose()
        {
            db_connection.Close();
        }

        //vitual functions
        public virtual IDataReader getDataById(int id)
        {
            Debug.Log(nameof(SqliteHelper) + "This function is not implemnted");
            throw null;
        }

        public virtual IDataReader getDataByString(string str)
        {
            Debug.Log(nameof(SqliteHelper) + "This function is not implemnted");
            throw null;
        }

        public virtual void deleteDataById(int id)
        {
            Debug.Log(nameof(SqliteHelper) + "This function is not implemented");
            throw null;
        }

        public virtual void deleteDataByString(string id)
        {
            Debug.Log(nameof(SqliteHelper) + "This function is not implemented");
            throw null;
        }

        public virtual IDataReader getAllData()
        {
            Debug.Log(nameof(SqliteHelper) + "This function is not implemented");
            throw null;
        }

        public virtual void deleteAllData()
        {
            Debug.Log(nameof(SqliteHelper) + "This function is not implemnted");
            throw null;
        }

        public virtual IDataReader getNumOfRows()
        {
            Debug.Log(nameof(SqliteHelper) + "This function is not implemnted");
            throw null;
        }

        //helper functions
        public IDbCommand getDbCommand()
        {
            return db_connection.CreateCommand();
        }

        public IDataReader getAllData(string table_name)
        {
            IDbCommand dbcmd = db_connection.CreateCommand();
            dbcmd.CommandText = "SELECT * FROM " + table_name;
            IDataReader reader = dbcmd.ExecuteReader();
            return reader;
        }

        public void deleteAllData(string table_name)
        {
            IDbCommand dbcmd = db_connection.CreateCommand();
            dbcmd.CommandText = "DROP TABLE IF EXISTS " + table_name;
            dbcmd.ExecuteNonQuery();
        }

        public IDataReader getNumOfRows(string table_name)
        {
            IDbCommand dbcmd = db_connection.CreateCommand();
            dbcmd.CommandText = "SELECT COALESCE(MAX(id)+1, 0) FROM " + table_name;
            IDataReader reader = dbcmd.ExecuteReader();
            return reader;
        }

        public bool tableExists(string table)
        {
            try
            {
                IDbCommand dbcmd = db_connection.CreateCommand();
                dbcmd.CommandText = "SELECT * FROM " + table;
                dbcmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception _)
            {
                return false;
            }
        }

        public void close()
        {
            db_connection.Close();
        }
	}
}