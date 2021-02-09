using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Knowdes
{
    public class DataManager : SqliteHelper
    {
        private const String TABLE_NAME_AUTHOR = "Author";
        private const String TABLE_NAME_CONTENT = "Content";
        private const String TABLE_NAME_METADATA = "MetaData";
        private const String TABLE_NAME_TAG = "Tag";

        private const String AUTHOR_KEY_ID = "id";
        private const String AUTHOR_ID = "guid";
        private const String AUTHOR_META_ID = "metaid";
        private const String AUTHOR_NAME = "name";

        private const String CONTENT_KEY_ID = "id";
        private const String CONTENT_ID = "guid";
        private const String CONTENT_ENTRY_ID = "entryid";
        private const String CONTENT_TYP = "typ";
        private const String CONTENT_INHALT = "inhalt";

        private const String METADATA_KEY_ID = "id";
        private const String METADATA_ENTRY_ID = "entryid";
        private const String METADATA_ID = "guid";
        private const String METADATA_INHALT = "inhalt";
        private const String METADATA_SHOWINPREVIEW = "showinpreview";
        private const String METADATA_TYP = "typ";

        private const String TAG_KEY_ID = "id";
        private const String TAG_META_ID = "metaid";
        private const String TAG_ID = "guid";
        private const String TAG_NAME = "name";


        public DataManager() : base()
        {
            IDbCommand dbcmd = getDbCommand();

            if (!tableExists(TABLE_NAME_AUTHOR))
            {
                dbcmd.CommandText = "CREATE TABLE IF NOT EXISTS " + TABLE_NAME_AUTHOR + " ( " +
                //AUTHOR_KEY_ID + " INTEGER PRIMARY KEY AUTOINCREMENT, " +
                AUTHOR_ID + " TEXT PRIMARY KEY, " +
                AUTHOR_META_ID + " TEXT , " +
                AUTHOR_NAME + " TEXT)";
                dbcmd.ExecuteNonQuery();
            }

            if (!tableExists(TABLE_NAME_CONTENT))
            {
                dbcmd.CommandText = "CREATE TABLE IF NOT EXISTS " + TABLE_NAME_CONTENT + " ( " +
                //CONTENT_KEY_ID + " INTEGER PRIMARY KEY AUTOINCREMENT, " +
                CONTENT_ENTRY_ID + " TEXT PRIMARY KEY, " +
                CONTENT_ID + " TEXT, " +
                CONTENT_TYP + " INTEGER, " +
                CONTENT_INHALT + " TEXT)";
                dbcmd.ExecuteNonQuery();
            }

            if (!tableExists(TABLE_NAME_METADATA))
            {
                dbcmd.CommandText = "CREATE TABLE IF NOT EXISTS " + TABLE_NAME_METADATA + " ( " +
                //METADATA_KEY_ID + " INTEGER PRIMARY KEY AUTOINCREMENT, " +
                METADATA_ID + " TEXT PRIMARY KEY, " +
                METADATA_ENTRY_ID + " TEXT , " +
                METADATA_INHALT + " TEXT, " +
                METADATA_SHOWINPREVIEW + " INTEGER, " +
                METADATA_TYP + " INTEGER)";
                dbcmd.ExecuteNonQuery();
            }

            if (!tableExists(TABLE_NAME_TAG))
            {
                dbcmd.CommandText = "CREATE TABLE IF NOT EXISTS " + TABLE_NAME_TAG + " ( " +
                //TAG_KEY_ID + " INTEGER PRIMARY KEY AUTOINCREMENT, " +
                TAG_ID + " TEXT PRIMARY KEY, " +
                TAG_META_ID + " TEXT, " +
                TAG_NAME + " TEXT)";
                dbcmd.ExecuteNonQuery();
            }
        }

        // TAGS
        //____________________________________________________________

        public void addOrReplace(Tag tag, MetaData metaData)
        {
            IDbCommand dbcmd = getDbCommand();
            dbcmd.CommandText = $"REPLACE INTO { TABLE_NAME_TAG} ({ TAG_ID} ,{TAG_META_ID}, { TAG_NAME}) "
                              + $"VALUES ( '{ tag.ID.ToString()}', '{metaData.ID.ToString()}', '{ tag.Name}' )";
            dbcmd.ExecuteNonQuery();
        }

        public void delete(Tag tag) 
        {
            IDbCommand dbcmd = getDbCommand();
            dbcmd.CommandText = "DELETE FROM " + TABLE_NAME_TAG + " WHERE " + TAG_ID + " = '" + tag.ID.ToString() + "'";
            dbcmd.ExecuteNonQuery();
        }

        public void deleteAllTagsOf(MetaData metaData)
        {
            IDbCommand dbcmd = getDbCommand();
            dbcmd.CommandText = "DELETE FROM " + TABLE_NAME_TAG + " WHERE " + TAG_META_ID + " = '" + metaData.ID.ToString() + "'";
            dbcmd.ExecuteNonQuery();
        }

        public List<Tag> getAllTagsOf(Guid metaDataID)
        {
            List<Tag> tags = new List<Tag>();
            IDataReader reader;
            IDbCommand dbcmd = getDbCommand();
            dbcmd.CommandText = "SELECT * FROM " + TABLE_NAME_TAG + " WHERE " + TAG_META_ID + " = '" + metaDataID.ToString() + "'";
            reader = dbcmd.ExecuteReader();
            while (reader.Read())
                tags.Add(convertTagFrom(reader));
            return tags;
        }

        private Tag convertTagFrom(IDataReader reader)
		{
            return new Tag(new Guid(Convert.ToString(reader[TAG_ID])), Convert.ToString(reader[TAG_NAME]));
        }

        // META DATA
        //____________________________________________________________

        public void deleteAllMetaDataOf(EntryData entryData)
        {
            foreach (MetaData data in entryData.MetaDatasCopy.Values)
                deleteMetaDataAdditions(data);
            IDbCommand dbcmd = getDbCommand();
            dbcmd.CommandText = "DELETE FROM " + TABLE_NAME_METADATA + " WHERE " + METADATA_ENTRY_ID + " = '" + entryData.Id.ToString() + "'";
            dbcmd.ExecuteNonQuery();
        }

        private void deleteMetaDataAdditions(MetaData data)
		{
            if(data is TagsData)
                deleteAllTagsOf(data);
            if (data is AuthorData)
                deleteAllAuthorsOf(data);
        }

        public List<MetaData> getAllMetadataOf(Guid entryGuid)
        {
            List<MetaData> metas = new List<MetaData>();

            IDataReader reader;
            IDbCommand dbcmd = getDbCommand();

            dbcmd.CommandText = "SELECT * FROM " + TABLE_NAME_METADATA + " WHERE " + METADATA_ENTRY_ID + " = '" + entryGuid.ToString() + "'";
            reader = dbcmd.ExecuteReader();
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
            List<Author> authors = getAllAuthorsOf(metaDataGuid);
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
            List<Tag> tags = getAllTagsOf(metaDataGuid);
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

        public void addOrReplace(MetaData metaData, Guid entryID)
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
                        addOrReplace(author, metaData.ID);
                    break;
                case MetaDataType.Tags:
                    TagsData tad = (TagsData)metaData;
                    inhalt = string.Empty;
                    List<Tag> tags = tad.TagsCopy;
                    foreach (Tag tag in tags)
                        addOrReplace(tag, metaData);
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
            IDbCommand dbcmd = getDbCommand();
            dbcmd.CommandText = "REPLACE INTO " + TABLE_NAME_METADATA + " ( " + METADATA_ID + ", " + METADATA_INHALT +
                                                                        ", " + METADATA_SHOWINPREVIEW + ", " + METADATA_TYP + ", " + METADATA_ENTRY_ID + " ) " +
                                                                        "VALUES ( '" + metaData.ID.ToString() + "', '" + inhalt + "', '" +
                                                                                  metaData.ShowInPreview + "', '" + ((int)metaData.Type) + "', '" + (entryID.ToString()) + "' )";
            dbcmd.ExecuteNonQuery();
        }

        public void addOrReplaceAllMetadataOf(EntryData entry)
        {
            foreach (MetaData metaData in entry.MetaDatasCopy.Values)
                addOrReplace(metaData, entry.Id);
        }

        // CONTENT
        //____________________________________________________________

        public void AddOrReplaceContentOf(EntryData entryData)
        {
            if (entryData.Content is TextbasedContentData)
                addTextbasedContent((TextbasedContentData)entryData.Content, entryData.Id);
            else
                throw new NotImplementedException();
        }

		private void addTextbasedContent(TextbasedContentData content, Guid entryID)
		{
            IDbCommand dbcmd = getDbCommand();
            dbcmd.CommandText = createAddContentRequest(content, entryID);
            dbcmd.ExecuteNonQuery();
        }

        private string createAddContentRequest(TextbasedContentData data, Guid entryID)
		{
            return "REPLACE INTO " + TABLE_NAME_CONTENT + " ( " + CONTENT_ID + ", " + CONTENT_TYP + ", " + CONTENT_INHALT + ", " + CONTENT_ENTRY_ID + " ) " +
                "VALUES ( '" + data.Id.ToString() + "', '" + (int) data.Type + "', '" + data.Content + "', '" + entryID.ToString() + "' )";
        }

        private void deleteContentOf(EntryData data)
        {
            IDbCommand dbcmd = getDbCommand();
            dbcmd.CommandText = "DELETE FROM " + TABLE_NAME_CONTENT + " WHERE " + CONTENT_ENTRY_ID + " = '" + data.Id.ToString() + "'";
            dbcmd.ExecuteNonQuery();
        }

        public List<ContentData> GetAllContentData(Guid guid)
        {
            List<ContentData> liste = new List<ContentData>();
            IDataReader reader;
            IDbCommand dbcmd = getDbCommand();
            dbcmd.CommandText = "SELECT * FROM " + TABLE_NAME_CONTENT + " WHERE " + CONTENT_ID + " ='" + guid.ToString() + "'";
            reader = dbcmd.ExecuteReader();
            while (reader.Read())
                liste.Add(convertContentFrom(reader));
            return liste;
        }

        private ContentData convertContentFrom(IDataReader reader)
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
                default:
                    throw new NotImplementedException();
            }
        }

        // AUTHORS
        //____________________________________________________________

        public List<Author> getAllAuthorsOf(Guid metaID)
        {
            List<Author> authors = new List<Author>();
            Author author;
            IDataReader reader;
            IDbCommand dbcmd = getDbCommand();
            dbcmd.CommandText = "SELECT * FROM " + TABLE_NAME_AUTHOR + " WHERE " + AUTHOR_META_ID + " = '" + metaID.ToString() + "'";
            reader = dbcmd.ExecuteReader();
            while (reader.Read())
            {
                Guid iD = new Guid(Convert.ToString(reader[AUTHOR_ID]));
                author = new Author(iD, Convert.ToString(reader[AUTHOR_NAME]));
                authors.Add(author);
            }
            return authors;
        }

        public void addOrReplace(Author author, Guid metaDataID)
        {
            IDbCommand dbcmd = getDbCommand();
            dbcmd.CommandText = "REPLACE INTO " + TABLE_NAME_AUTHOR + " ( " + AUTHOR_ID + ", " + AUTHOR_NAME + ", " + AUTHOR_META_ID + " ) "
                              + "VALUES ( '" + author.Id.ToString() + "', '" + author.Name + "', '" + metaDataID.ToString() + "' )";
            dbcmd.ExecuteNonQuery();
        }

        public void deleteAllAuthorsOf(MetaData metaData)
        {
            IDbCommand dbcmd = getDbCommand();
            dbcmd.CommandText = "DELETE FROM " + TABLE_NAME_AUTHOR + " WHERE " + AUTHOR_META_ID + " = '" + metaData.ID.ToString() + "'";
            dbcmd.ExecuteNonQuery();
        }

        // ENTRIES
        //____________________________________________________________

        public void AddOrReplaceEntryData(EntryData entry)
        {
            AddOrReplaceContentOf(entry);
            addOrReplaceAllMetadataOf(entry);
        }

        public List<EntryData> GetAllEntrys()
        {
            List<EntryData> entries = new List<EntryData>();
            IDataReader reader;
            IDbCommand dbcmd = getDbCommand();
            dbcmd.CommandText = "SELECT * FROM " + TABLE_NAME_CONTENT;
            reader = dbcmd.ExecuteReader();
            
            while (reader.Read())
                entries.Add(convertEntryFrom(reader));
            
            return entries;
        }

        public void DeleteEntry(EntryData entryData)
        {
            deleteContentOf(entryData);
            deleteAllMetaDataOf(entryData);
        }

        public EntryData getEntry(Guid guid)
        {
            IDataReader reader;
            IDbCommand dbcmd = getDbCommand();
            dbcmd.CommandText = "SELECT * FROM " + TABLE_NAME_CONTENT + " WHERE " + CONTENT_ID + " ='" + guid.ToString() + "'" ;
            reader = dbcmd.ExecuteReader();
            EntryData result = null;
            while (reader.Read())
                result = convertEntryFrom(reader);
            return result;
        }

        private EntryData convertEntryFrom(IDataReader reader)
		{
            ContentData contentData = convertContentFrom(reader);
            EntryData ed = new EntryData(contentData);
            List<MetaData> meta = getAllMetadataOf(contentData.Id);
            foreach (MetaData item in meta)
                ed.AddMetaData(item);
            return ed;
        }
    }
}
