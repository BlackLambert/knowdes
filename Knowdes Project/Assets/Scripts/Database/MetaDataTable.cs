using System;
using System.Collections.Generic;
using System.Data;

namespace Knowdes
{
	public class MetaDataTable : Table
	{
		private const string TABLE_NAME = "MetaData";
		private const string METADATA_ENTRY_ID = "entryid";
		private const string METADATA_ID = "guid";
		private const string METADATA_INHALT = "inhalt";
		private const string METADATA_SHOWINPREVIEW = "showinpreview";
		private const string METADATA_TYP = "typ";

        private AuthorTable _authorTable;
        private TagsTable _tagsTable;

		protected override string Name => TABLE_NAME;

		public MetaDataTable(IDbConnection dataBaseConnection,
            AuthorTable authorTable,
            TagsTable tagsTable) : base(dataBaseConnection)
		{
            _authorTable = authorTable;
            _tagsTable = tagsTable;
        }


		protected override void createTable()
		{
			string command = "CREATE TABLE IF NOT EXISTS " + TABLE_NAME + " ( " +
				METADATA_ID + " TEXT PRIMARY KEY, " +
				METADATA_ENTRY_ID + " TEXT , " +
				METADATA_INHALT + " TEXT, " +
				METADATA_SHOWINPREVIEW + " INTEGER, " +
				METADATA_TYP + " INTEGER)";
			executeNonQuery(command);
		}

        public void DeleteAllMetaDataOf(EntryData entryData)
        {
            foreach (MetaData data in entryData.MetaDatasCopy.Values)
                deleteMetaDataAdditions(data);
            string command = "DELETE FROM " + TABLE_NAME + " WHERE " + METADATA_ENTRY_ID + " = '" + entryData.Id.ToString() + "'";
            executeNonQuery(command);
        }

        private void deleteMetaDataAdditions(MetaData data)
        {
            if (data is TagsData)
                _tagsTable.DeleteAllTagsOf((TagsData)data);
            if (data is AuthorData)
                _authorTable.DeleteAllAuthorsOf(data);
        }

        public List<MetaData> GetAllMetadataOf(Guid entryGuid)
        {
            List<MetaData> metas = new List<MetaData>();
            IDataReader reader;
            string command = "SELECT * FROM " + TABLE_NAME + " WHERE " + METADATA_ENTRY_ID + " = '" + entryGuid.ToString() + "'";
            reader = executeReader(command);
            while (reader.Read())
                metas.Add(convertMetaDataFrom(reader));
            return metas;
        }

        private MetaData convertMetaDataFrom(IDataReader reader)
        {

            MetaDataType type = (MetaDataType)Convert.ToInt32(reader[METADATA_TYP]);
            switch (type)
            {
                case MetaDataType.Author:
                    return convertAuthorDataFrom(reader);
                case MetaDataType.CreationDate:
                    return convertCreationDateDataFrom(reader);
                case MetaDataType.LastChangedDate:
                    return convertLastChangedDateDataFrom(reader);
                case MetaDataType.Tags:
                    return convertTagsDataFrom(reader);
                case MetaDataType.Title:
                    return convertTitleDataFrom(reader);
                case MetaDataType.Comment:
                    return convertCommentDataFrom(reader);
                case MetaDataType.Description:
                    return convertDescriptionDataFrom(reader);
                case MetaDataType.PreviewImage:
                    return convertPreviewImageDataFrom(reader);
                default:
                    throw new NotImplementedException();
            }
        }

        private AuthorData convertAuthorDataFrom(IDataReader reader)
        {
            Guid metaDataGuid = new Guid(Convert.ToString(reader[METADATA_ID]));
            List<Author> authors = _authorTable.GetAllAuthorsOf(metaDataGuid);
            AuthorData result = new AuthorData(metaDataGuid, authors);
            result.ShowInPreview = Convert.ToBoolean(reader[METADATA_SHOWINPREVIEW]);
            return result;
        }

        private CreationDateData convertCreationDateDataFrom(IDataReader reader)
        {
            Guid metaDataGuid = new Guid(Convert.ToString(reader[METADATA_ID]));
            CreationDateData result = new CreationDateData(metaDataGuid, Convert.ToDateTime(reader[METADATA_INHALT]));
            result.ShowInPreview = Convert.ToBoolean(reader[METADATA_SHOWINPREVIEW]);
            return result;
        }

        private LastChangeDateData convertLastChangedDateDataFrom(IDataReader reader)
        {
            Guid metaDataGuid = new Guid(Convert.ToString(reader[METADATA_ID]));
            LastChangeDateData result = new LastChangeDateData(metaDataGuid, Convert.ToDateTime(reader[METADATA_INHALT]));
            result.ShowInPreview = Convert.ToBoolean(reader[METADATA_SHOWINPREVIEW]);
            return result;
        }

        private TagsData convertTagsDataFrom(IDataReader reader)
        {
            Guid metaDataGuid = new Guid(Convert.ToString(reader[METADATA_ID]));
            List<Tag> tags = _tagsTable.GetAllTagsOf(metaDataGuid);
            TagsData result = new TagsData(metaDataGuid, tags);
            result.ShowInPreview = Convert.ToBoolean(reader[METADATA_SHOWINPREVIEW]);
            return result;
        }

        private TitleData convertTitleDataFrom(IDataReader reader)
        {
            Guid metaDataGuid = new Guid(Convert.ToString(reader[METADATA_ID]));
            TitleData result = new TitleData(metaDataGuid, Convert.ToString(reader[METADATA_INHALT]));
            result.ShowInPreview = Convert.ToBoolean(reader[METADATA_SHOWINPREVIEW]);
            return result;
        }

        private CommentData convertCommentDataFrom(IDataReader reader)
        {
            Guid metaDataGuid = new Guid(Convert.ToString(reader[METADATA_ID]));
            CommentData result = new CommentData(metaDataGuid, Convert.ToString(reader[METADATA_INHALT]));
            result.ShowInPreview = Convert.ToBoolean(reader[METADATA_SHOWINPREVIEW]);
            return result;
        }

        private PreviewImageData convertPreviewImageDataFrom(IDataReader reader)
        {
            Guid metaDataGuid = new Guid(Convert.ToString(reader[METADATA_ID]));
            string content = Convert.ToString(reader[METADATA_INHALT]);
            Uri uri = string.IsNullOrEmpty(content) ? null : new Uri(content);
            PreviewImageData result = new PreviewImageData(metaDataGuid, uri);
            result.ShowInPreview = Convert.ToBoolean(reader[METADATA_SHOWINPREVIEW]);
            return result;
        }

        private DescriptionData convertDescriptionDataFrom(IDataReader reader)
        {
            Guid metaDataGuid = new Guid(Convert.ToString(reader[METADATA_ID]));
            DescriptionData result = new DescriptionData(metaDataGuid, Convert.ToString(reader[METADATA_INHALT]));
            result.ShowInPreview = Convert.ToBoolean(reader[METADATA_SHOWINPREVIEW]);
            return result;
        }

        public void AddOrReplace(MetaData metaData, Guid entryID)
        {
            string inhalt;
            switch (metaData.Type)
            {
                case MetaDataType.CreationDate:
                    CreationDateData cdd = (CreationDateData)metaData;
                    inhalt = cdd.Date.ToString();
                    break;
                case MetaDataType.LastChangedDate:
                    LastChangeDateData lcd = (LastChangeDateData)metaData;
                    inhalt = lcd.Date.ToString();
                    break;
                case MetaDataType.Title:
                    TitleData td = (TitleData)metaData;
                    inhalt = td.Content;
                    break;
                case MetaDataType.Author:
                    AuthorData ad = (AuthorData)metaData;
                    inhalt = string.Empty;
                    List<Author> authors = ad.AuthorsCopy;
                    foreach (Author author in authors)
                        _authorTable.AddOrReplace(author, metaData.ID);
                    break;
                case MetaDataType.Tags:
                    TagsData tad = (TagsData)metaData;
                    inhalt = string.Empty;
                    List<Tag> tags = tad.TagsCopy;
                    foreach (Tag tag in tags)
                        _tagsTable.AddOrReplace(tag, metaData);
                    break;
                case MetaDataType.Comment:
                    CommentData commentData = (CommentData)metaData;
                    inhalt = commentData.Content;
                    break;
                case MetaDataType.Description:
                    DescriptionData descriptionData = (DescriptionData)metaData;
                    inhalt = descriptionData.Content;
                    break;
                case MetaDataType.PreviewImage:
                    PreviewImageData previewData = (PreviewImageData)metaData;
                    inhalt = previewData.Uri.AbsoluteUri;
                    break;
                default:
                    throw new NotImplementedException();
            }
            string command = createAddOrReplaceMetaDataCommand(metaData, inhalt, entryID);
            executeNonQuery(command);
        }

        private string createAddOrReplaceMetaDataCommand(MetaData metaData, string content, Guid entryID)
        {
            string escapedContent = escape(content);
            return "REPLACE INTO " + TABLE_NAME + " ( " + METADATA_ID + ", " + METADATA_INHALT +
                                                                        ", " + METADATA_SHOWINPREVIEW + ", " + METADATA_TYP + ", " + METADATA_ENTRY_ID + " ) " +
                                                                        "VALUES ( '" + metaData.ID.ToString() + "', '" + escapedContent + "', '" +
                                                                                 Convert.ToInt32(metaData.ShowInPreview) + "', '" + ((int)metaData.Type) + "', '" + (entryID.ToString()) + "' )";

        }

        private string escape(string content)
        {
            return content.Replace("'", "''");
        }

        public void AddOrReplaceAllMetadataOf(EntryData entry)
        {
            foreach (MetaData metaData in entry.MetaDatasCopy.Values)
                AddOrReplace(metaData, entry.Id);
        }
    }
}