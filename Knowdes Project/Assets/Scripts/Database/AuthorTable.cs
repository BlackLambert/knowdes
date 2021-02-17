using System;
using System.Collections.Generic;
using System.Data;

namespace Knowdes
{
	public class AuthorTable : Table
	{
		private const string TABLE_NAME = "Author"; 
		private const string AUTHOR_ID = "guid";
		private const string AUTHOR_META_ID = "metaid";
		private const string AUTHOR_NAME = "name";

		public AuthorTable(IDbConnection dataBaseConnection) : base(dataBaseConnection)
		{
		}

		protected override string Name => TABLE_NAME;

		protected override void createTable()
		{
			string command = "CREATE TABLE IF NOT EXISTS " + TABLE_NAME + " ( " +
				AUTHOR_ID + " TEXT PRIMARY KEY, " +
				AUTHOR_META_ID + " TEXT , " +
				AUTHOR_NAME + " TEXT)";
			executeNonQuery(command);
		}

        public List<Author> GetAllAuthorsOf(Guid metaID)
        {
            List<Author> authors = new List<Author>();
            Author author;
            string command = "SELECT * FROM " + TABLE_NAME + " WHERE " + AUTHOR_META_ID + " = '" + metaID.ToString() + "'";
            IDataReader reader = executeReader(command);
            while (reader.Read())
            {
                Guid iD = new Guid(Convert.ToString(reader[AUTHOR_ID]));
                author = new Author(iD, Convert.ToString(reader[AUTHOR_NAME]));
                authors.Add(author);
            }
            return authors;
        }

        public void AddOrReplace(Author author, Guid metaDataID)
        {
            string command = "REPLACE INTO " + TABLE_NAME + " ( " + AUTHOR_ID + ", " + AUTHOR_NAME + ", " + AUTHOR_META_ID + " ) "
                              + "VALUES ( '" + author.Id.ToString() + "', '" + author.Name + "', '" + metaDataID.ToString() + "' )";
            executeNonQuery(command);
        }

		internal void Delete(Author author)
		{
            string command = "DELETE FROM " + TABLE_NAME + " WHERE " + AUTHOR_ID + " = '" + author.Id.ToString() + "'";
            executeNonQuery(command);
        }

		public void DeleteAllAuthorsOf(MetaData metaData)
        {
            string command = "DELETE FROM " + TABLE_NAME + " WHERE " + AUTHOR_META_ID + " = '" + metaData.ID.ToString() + "'";
            executeNonQuery(command);
        }
    }
}