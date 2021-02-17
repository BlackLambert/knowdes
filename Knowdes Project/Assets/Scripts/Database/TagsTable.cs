using System;
using System.Collections.Generic;
using System.Data;

namespace Knowdes
{
	public class TagsTable : Table
	{
		private const string TABLE_NAME = "Tag";
		private const string TAG_META_ID = "metaid";
		private const string TAG_ID = "guid";
		private const string TAG_NAME = "name";

		protected override string Name => TABLE_NAME;

		public TagsTable(IDbConnection dataBaseConnection) : base(dataBaseConnection)
		{
		}

		protected override void createTable()
		{
			string command = "CREATE TABLE IF NOT EXISTS " + TABLE_NAME + " ( " +
				TAG_ID + " TEXT PRIMARY KEY, " +
				TAG_META_ID + " TEXT, " +
				TAG_NAME + " TEXT)";
			executeNonQuery(command);
		}

		public void AddOrReplace(Tag tag, MetaData metaData)
		{
			string command = $"REPLACE INTO { TABLE_NAME} ({ TAG_ID} ,{TAG_META_ID}, { TAG_NAME}) "
							  + $"VALUES ( '{ tag.ID.ToString()}', '{metaData.ID.ToString()}', '{ tag.Name}' )";
			executeNonQuery(command);
		}

		public void DeleteAllTagsOf(TagsData tagsData)
		{
			string command = "DELETE FROM " + TABLE_NAME + " WHERE " + TAG_META_ID + " = '" + tagsData.ID.ToString() + "'";
			executeNonQuery(command);
		}

		public void Delete(Tag tag)
		{
			string command = "DELETE FROM " + TABLE_NAME + " WHERE " + TAG_ID + " = '" + tag.ID.ToString() + "'";
			executeNonQuery(command);
		}

		public List<Tag> GetAllTagsOf(Guid metaDataID)
		{
			string command = "SELECT * FROM " + TABLE_NAME + " WHERE " + TAG_META_ID + " = '" + metaDataID.ToString() + "'";
			IDataReader reader = executeReader(command);
			return convertTagsFrom(reader);
		}

		private List<Tag> convertTagsFrom(IDataReader reader)
		{
			List<Tag> tags = new List<Tag>();
			while (reader.Read())
				tags.Add(convertTagFrom(reader));
			return tags;
		}

		private Tag convertTagFrom(IDataReader reader)
		{
			return new Tag(new Guid(Convert.ToString(reader[TAG_ID])), Convert.ToString(reader[TAG_NAME]));
		}
	}
}