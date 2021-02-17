using System;
using System.Data;

namespace Knowdes
{
    public abstract class Table
    {
        private IDbConnection _dataBaseConnection;
        protected abstract string Name { get; }

        public Table(IDbConnection dataBaseConnection)
		{
            _dataBaseConnection = dataBaseConnection;
        }

        public void Init()
		{
            if (!tableExists())
                createTable();
        }

        protected abstract void createTable();

        private bool tableExists()
        {
            try
            {
                executeNonQuery("SELECT * FROM " + Name);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
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

        private IDbCommand getDbCommand()
        {
            return _dataBaseConnection.CreateCommand();
        }
    }
}