using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

namespace Knowdes
{
	public class ContentDataTable : Table
	{
		private const string TABLE_NAME = "Content";
		private const string CONTENT_ID = "guid";
		private const string CONTENT_ENTRY_ID = "entryid";
		private const string CONTENT_TYP = "typ";
		private const string CONTENT_INHALT = "inhalt";
		protected override string Name => TABLE_NAME;

		public ContentDataTable(IDbConnection dataBaseConnection) : base(dataBaseConnection)
		{
		}

		protected override void createTable()
		{
			string command = "CREATE TABLE IF NOT EXISTS " + TABLE_NAME + " ( " +
				CONTENT_ENTRY_ID + " TEXT PRIMARY KEY, " +
				CONTENT_ID + " TEXT, " +
				CONTENT_TYP + " INTEGER, " +
				CONTENT_INHALT + " TEXT)";
			executeNonQuery(command);
		}

        public void AddOrReplaceContentOf(EntryData entryData)
        {
            if (entryData.Content is TextbasedContentData)
                addTextbasedContent((TextbasedContentData)entryData.Content, entryData.Id);
            else
                throw new NotImplementedException();
        }

        private void addTextbasedContent(TextbasedContentData content, Guid entryID)
        {
            string command = createAddContentRequest(content, entryID);
            executeNonQuery(command);
        }

        private string createAddContentRequest(TextbasedContentData data, Guid entryID)
        {
            return "REPLACE INTO " + TABLE_NAME + " ( " + CONTENT_ID + ", " + CONTENT_TYP + ", " + CONTENT_INHALT + ", " + CONTENT_ENTRY_ID + " ) " +
                "VALUES ( '" + data.Id.ToString() + "', '" + (int)data.Type + "', '" + @data.Content + "', '" + entryID.ToString() + "' )";
        }

        public void DeleteContentOf(EntryData data)
        {
            string command = "DELETE FROM " + TABLE_NAME + " WHERE " + CONTENT_ENTRY_ID + " = '" + data.Id.ToString() + "'";
            executeNonQuery(command);
        }

        public List<ContentData> GetAllContentData()
        {
            List<ContentData> liste = new List<ContentData>();
            IDataReader reader;
            string command = "SELECT * FROM " + TABLE_NAME;
            reader = executeReader(command);
            while (reader.Read())
                liste.Add(ConvertContentFrom(reader));
            return liste;
        }

        public ContentData ConvertContentFrom(IDataReader reader)
        {
            int typeInt = Convert.ToInt16(reader[CONTENT_TYP]);
            switch (typeInt)
            {
                case (int)ContentType.Text:
                    return new TextContentData(new Guid(Convert.ToString(reader[CONTENT_ID])), Convert.ToString(reader[CONTENT_INHALT]));
                case (int)ContentType.Weblink:
                    string content = Convert.ToString(reader[CONTENT_INHALT]);
                    return new WeblinkContentData(new Guid(Convert.ToString(reader[CONTENT_ID])), string.IsNullOrEmpty(content) ? null : new Uri(content));
                case (int)ContentType.Image:
                    return new ImageContentData(new Guid(Convert.ToString(reader[CONTENT_ID])), Convert.ToString(reader[CONTENT_INHALT]));
                case (int)ContentType.File:
                    return new UnknownFileContentData(new Guid(Convert.ToString(reader[CONTENT_ID])), Convert.ToString(reader[CONTENT_INHALT]));
                default:
                    throw new NotImplementedException();
            }
        }
    }
}