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
        private const String AUTHOR_AUTHOR = "auhtor";

        private const String CONTENT_KEY_ID = "id";
        private const String CONTENT_ID = "guid";
        private const String CONTENT_TYP = "typ";
        private const String CONTENT_INHALT = "inhalt";

        private const String METADATA_KEY_ID = "id";
        private const String METADATA_ENTRY_ID = "entryid";
        private const String METADATA_ID = "guid";
        private const String METADATA_INHALT = "inhalt";
        private const String METADATA_DESTROY = "destroy";
        private const String METADATA_PRIORITY = "priority";
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
                AUTHOR_KEY_ID + " INTEGER PRIMARY KEY AUTOINCREMENT, " + //AUTHOR_ID
                AUTHOR_ID + " TEXT, " +
                //AUTHOR_META_ID + " TEXT, " +
                AUTHOR_AUTHOR + " TEXT)";
                dbcmd.ExecuteNonQuery();
            }

            if (!tableExists(TABLE_NAME_CONTENT))
            {
                dbcmd.CommandText = "CREATE TABLE IF NOT EXISTS " + TABLE_NAME_CONTENT + " ( " +
                CONTENT_KEY_ID + " INTEGER PRIMARY KEY AUTOINCREMENT, " +
                CONTENT_ID + " TEXT, " +
                CONTENT_TYP + " INTEGER, " +
                CONTENT_INHALT + " TEXT)";
                dbcmd.ExecuteNonQuery();
            }

            if (!tableExists(TABLE_NAME_METADATA))
            {
                dbcmd.CommandText = "CREATE TABLE IF NOT EXISTS " + TABLE_NAME_METADATA + " ( " +
                METADATA_KEY_ID + " INTEGER PRIMARY KEY AUTOINCREMENT, " +
                METADATA_ENTRY_ID + " TEXT, " +
                METADATA_ID + " TEXT, " +
                METADATA_INHALT + " TEXT, " +
                METADATA_SHOWINPREVIEW + " INTEGER, " +
                METADATA_TYP + " INTEGER)";
                dbcmd.ExecuteNonQuery();
            }

            if (!tableExists(TABLE_NAME_TAG))
            {
                dbcmd.CommandText = "CREATE TABLE IF NOT EXISTS " + TABLE_NAME_TAG + " ( " +
                TAG_KEY_ID + " INTEGER PRIMARY KEY AUTOINCREMENT, " +
                TAG_ID + " TEXT, " +
                TAG_NAME + " TEXT)";
                dbcmd.ExecuteNonQuery();
            }
        }

        // TAGS
        //____________________________________________________________

        public void addTag(int meta_id, String guid, String name)
        {
            IDbCommand dbcmd = getDbCommand();
            dbcmd.CommandText = "INSERT INTO " + TABLE_NAME_TAG + " ( " + TAG_META_ID + ", " + TAG_ID + ", " + TAG_NAME + " ) "
                              + "VALUES ( '" + meta_id + "', '" + guid + "', '" + name + "' )";
            dbcmd.ExecuteNonQuery();
        }

        public void addTag(Tag tag)
        {
            IDbCommand dbcmd = getDbCommand();
            dbcmd.CommandText = "INSERT INTO " + TABLE_NAME_TAG + " ( " + TAG_ID + ", " + TAG_NAME + " ) "
                              + "VALUES ( '" + tag.ID.ToString() + "', '" + tag.Name + "' )";
            dbcmd.ExecuteNonQuery();
        }

        public void deleteTag(String guid, String name) // todo: angaben notwendig?
        {
            IDbCommand dbcmd = getDbCommand();
            dbcmd.CommandText = "DELETE FROM " + TABLE_NAME_TAG + " WHERE " + TAG_ID + " = '" + guid + "' AND " + TAG_NAME + " = '" + name + "'";
            dbcmd.ExecuteNonQuery();
        }
        public void deleteAllTags(Guid guid)
        {
            IDbCommand dbcmd = getDbCommand();
            dbcmd.CommandText = "DELETE FROM " + TABLE_NAME_TAG + " WHERE " + TAG_ID + " = '" + guid.ToString() + "'";
            dbcmd.ExecuteNonQuery();
        }

        public void updateTag(int meta_id, Tag oldTag, Tag newTag)
        {
            deleteTag(oldTag.ID.ToString(), oldTag.Name);
            addTag(meta_id, newTag.ID.ToString(), newTag.Name);
        }

        public List<Tag> getAllTags(Guid id)
        {
            List<Tag> tags = new List<Tag>();
            Tag t;
            IDataReader reader;
            IDbCommand dbcmd = getDbCommand();
            dbcmd.CommandText = "SELECT * FROM " + TABLE_NAME_TAG + " WHERE " + TAG_ID + " = '" + id.ToString() + "'";
            reader = dbcmd.ExecuteReader();
            while (reader.Read())
            {
                t = new Tag(new Guid(Convert.ToString(reader[TAG_ID])), Convert.ToString(reader[TAG_NAME]));
                tags.Add(t);
            }
            return tags;
        }

        public List<Tag> getAllTagsByID(Guid meta_id)
        {
            List<Tag> tags = new List<Tag>();
            Tag t;
            IDataReader reader;
            IDbCommand dbcmd = getDbCommand();
            dbcmd.CommandText = "SELECT * FROM " + TABLE_NAME_TAG + " WHERE " + TAG_ID + " = '" + meta_id.ToString() + "'";
            reader = dbcmd.ExecuteReader();
            while (reader.Read())
            {
                t = new Tag(new Guid(Convert.ToString(reader[TAG_ID])), Convert.ToString(reader[TAG_NAME]));
                tags.Add(t);
            }
            return tags;
        }

        // META DATA
        //____________________________________________________________

        public void deleteAllMetaData(Guid guid)
        {
            IDbCommand dbcmd = getDbCommand();
            dbcmd.CommandText = "DELETE FROM " + TABLE_NAME_METADATA + " WHERE " + METADATA_ID + " = '" + guid.ToString() + "'";
            dbcmd.ExecuteNonQuery();
            deleteAllAuthors(guid);
            deleteAllTags(guid);
        }

        public List<MetaData> getAllMetadata(Guid entryGuid)
        {
            List<MetaData> metas = new List<MetaData>();

            IDataReader reader;
            IDbCommand dbcmd = getDbCommand();

            dbcmd.CommandText = "SELECT * FROM " + TABLE_NAME_METADATA + " WHERE " + METADATA_ID + " = '" + entryGuid.ToString() + "'";
            reader = dbcmd.ExecuteReader();
            while (reader.Read())
            {
                Guid metaDataGuid = new Guid(Convert.ToString(reader[METADATA_ID]));
                switch ((MetaDataType)Convert.ToInt32(reader[METADATA_TYP]))
                {
                    case MetaDataType.Author:
                        List<Author> authors = getAllAuthors(entryGuid);
                        AuthorData ad = new AuthorData(metaDataGuid, authors);
                        ad.ShowInPreview = Convert.ToBoolean(reader.GetString(5));
                        metas.Add(ad);
                        break;
                    case MetaDataType.CreationDate:
                        CreationDateData cdd = new CreationDateData(metaDataGuid, Convert.ToDateTime(reader[METADATA_INHALT]));
                        cdd.ShowInPreview = Convert.ToBoolean(reader.GetString(5));
                        metas.Add(cdd);
                        break;
                    case MetaDataType.LastChangedDate:
                        LastChangeDateData lcdd = new LastChangeDateData(metaDataGuid, Convert.ToDateTime(reader[METADATA_INHALT]));
                        lcdd.ShowInPreview = Convert.ToBoolean(reader.GetString(5));
                        metas.Add(lcdd);
                        break;
                    case MetaDataType.Tags:
                        List<Tag> tags = getAllTagsByID(entryGuid);
                        TagsData tagData = new TagsData(metaDataGuid, tags);
                        metas.Add(tagData);
                        break;
                    case MetaDataType.Title:
                        TitleData td = null;
                        td = new TitleData(metaDataGuid, Convert.ToString(reader[METADATA_INHALT]));
                        td.ShowInPreview = Convert.ToBoolean(reader.GetString(5));
                        metas.Add(td);
                        break;
                    default:
                        //Debug.Log("default: " + reader[METADATA_TYP] + " | " + MetaDataType.Title + " | " + metas.Count);
                        throw new NotImplementedException();
                }

            }
            return metas;
        }

        public void addMetaData(MetaData meta)
        {
            String inhalt = null;
            //Debug.Log("addMetaData: " + meta.Type );

            switch (meta.Type)
            {
                case MetaDataType.CreationDate:
                    CreationDateData cdd = (CreationDateData)meta;
                    inhalt = cdd.Date.ToString();
                    break;
                case MetaDataType.LastChangedDate:
                    LastChangeDateData lcd = (LastChangeDateData)meta;
                    inhalt = lcd.Date.ToString();
                    break;
                case MetaDataType.Title:
                    TitleData td = (TitleData)meta;
                    inhalt = td.Content;
                    break;
                case MetaDataType.Author:
                    AuthorData ad = (AuthorData)meta;
                    inhalt = string.Empty;
                    List<Author> authors = ad.AuthorsCopy;
                    foreach (Author author in authors)
                    {
                        addAuthor(author, meta.ID);
                    }
                    break;
                case MetaDataType.Tags:
                    TagsData tad = (TagsData)meta;
                    inhalt = string.Empty;
                    List<Tag> tags = tad.TagsCopy;
                    foreach (Tag tag in tags)
                    {
                        addTag(tag);
                    }
                    break;
                default:
                    throw new NotImplementedException();
            }
            IDbCommand dbcmd = getDbCommand();
            dbcmd.CommandText = "INSERT INTO " + TABLE_NAME_METADATA + " ( " + METADATA_ID + ", " + METADATA_INHALT +
                                                                        ", " + METADATA_DESTROY + ", " + METADATA_PRIORITY + ", " + METADATA_SHOWINPREVIEW +
                                                                        ", " + METADATA_TYP + " ) " +
                                                                        "VALUES ( '" + meta.ID.ToString() + "', '" + inhalt + "', '" +
                                                                                  meta.ShowInPreview + "', '" + ((int)meta.Type) + "' )";
            dbcmd.ExecuteNonQuery();
        }

        public void addAllMetadata(Dictionary<MetaDataType, MetaData> meta)
        {
            foreach (var item in meta)
            {
                addMetaData(item.Value);
            }
        }

        // CONTENT
        //____________________________________________________________

        public void addTextContentData(TextContentData cd)
        {
            IDbCommand dbcmd = getDbCommand();
            dbcmd.CommandText = "INSERT INTO " + TABLE_NAME_CONTENT + " ( " + CONTENT_ID + ", " + CONTENT_TYP + ", " + CONTENT_INHALT + " ) "
                              + "VALUES ( '" + cd.ID.ToString() + "', '" + ((int)cd.Type) + "', '" + cd.Text + "' )";
            dbcmd.ExecuteNonQuery();
        }

        public void addDataContentData(DataContentData cd)
        {
            IDbCommand dbcmd = getDbCommand();
            dbcmd.CommandText = "INSERT INTO " + TABLE_NAME_CONTENT + " ( " + CONTENT_ID + ", " + CONTENT_TYP + ", " + CONTENT_INHALT + " ) "
                              + "VALUES ( '" + cd.ID.ToString() + "', '" + ((int)cd.Type) + "', '" + cd.Path + "' )";
            dbcmd.ExecuteNonQuery();
        }
        public void addContent(ContentData content)
        {
            IDbCommand dbcmd = getDbCommand();
            if (content.Type == ContentDataType.Text)
            {
                TextContentData textcontent = (TextContentData)content;
                dbcmd.CommandText = "INSERT INTO " + TABLE_NAME_CONTENT + " ( " + CONTENT_ID + ", " + CONTENT_TYP + ", " + CONTENT_INHALT + " ) " +
                                                                            "VALUES ( '" + textcontent.ID.ToString() + "', '" + textcontent.Type + "', '" + textcontent.Text + "' )";
            }
            else
            {
                DataContentData datacontent = (DataContentData)content;
                dbcmd.CommandText = "INSERT INTO " + TABLE_NAME_CONTENT + " ( " + CONTENT_ID + ", " + CONTENT_TYP + ", " + CONTENT_INHALT + " ) " +
                                                                            "VALUES ( '" + datacontent.ID.ToString() + "', '" + datacontent.Type + "', '" + datacontent.Path + "' )";
            }
            dbcmd.ExecuteNonQuery();
        }

        public void deleteContent(Guid guid)
        {
            IDbCommand dbcmd = getDbCommand();
            dbcmd.CommandText = "DELETE FROM " + TABLE_NAME_CONTENT + " WHERE " + CONTENT_ID + " = '" + guid.ToString() + "'";
            dbcmd.ExecuteNonQuery();
        }

        public List<ContentData> getAllContentData(Guid guid)
        {
            List<ContentData> liste = new List<ContentData>();
            ContentData cd = null;
            IDataReader reader;
            IDbCommand dbcmd = getDbCommand();
            dbcmd.CommandText = "SELECT * FROM " + TABLE_NAME_CONTENT + " WHERE " + CONTENT_ID + " ='" + guid.ToString() + "'";
            reader = dbcmd.ExecuteReader();
            while (reader.Read())
            {
                switch (Convert.ToInt16(reader[CONTENT_TYP]))
                {
                    case (int)ContentDataType.Text:
                        cd = new TextContentData(new Guid(Convert.ToString(reader[CONTENT_ID])), Convert.ToString(reader[CONTENT_INHALT]));
                        break;
                    default:
                        cd = new DataContentData(new Guid(Convert.ToString(reader[CONTENT_ID])), Convert.ToString(reader[CONTENT_INHALT]));
                        break;
                }
                liste.Add(cd);
            }
            return liste;
        }



        public void deleteAllAuthors(Guid guid)
        {
            IDbCommand dbcmd = getDbCommand();
            dbcmd.CommandText = "DELETE FROM " + TABLE_NAME_AUTHOR + " WHERE " + AUTHOR_ID + " = '" + guid.ToString() + "'";
            dbcmd.ExecuteNonQuery();
        }

        public void deleteEntry(Guid guid)
        {
            deleteContent(guid);
            deleteAllMetaData(guid);
        }

        // AUTHORS
        //____________________________________________________________

        public List<Author> getAllAuthors(Guid id)
        {
            List<Author> authors = new List<Author>();
            Author author;
            IDataReader reader;
            IDbCommand dbcmd = getDbCommand();
            dbcmd.CommandText = "SELECT * FROM " + TABLE_NAME_AUTHOR + " WHERE " + AUTHOR_ID + " = '" + id.ToString() + "'";
            reader = dbcmd.ExecuteReader();
            while (reader.Read())
            {
                author = new Author(Convert.ToString(reader[AUTHOR_AUTHOR]));
                authors.Add(author);
            }
            return authors;
        }

        public void addAuthor(Author author, Guid guid)
        {
            IDbCommand dbcmd = getDbCommand();
            dbcmd.CommandText = "INSERT INTO " + TABLE_NAME_AUTHOR + " ( " + AUTHOR_ID + ", " + AUTHOR_AUTHOR + " ) "
                              + "VALUES ( '" + guid.ToString() + "', '" + author.Name + "' )";
            dbcmd.ExecuteNonQuery();
        }

        // ENTRIES
        //____________________________________________________________

        public void addEntryData(EntryData entry)
        {
            addContent(entry.Content);
            addAllMetadata(entry.MetaDatasCopy);
        }

        public List<EntryData> getAllEntrys()
        {
            List<EntryData> entries = new List<EntryData>();
            EntryData ed = null;            
            List<MetaData> meta = new List<MetaData>();
            Guid guid;
            IDataReader reader;
            IDbCommand dbcmd = getDbCommand();
            dbcmd.CommandText = "SELECT * FROM " + TABLE_NAME_CONTENT;
            reader = dbcmd.ExecuteReader();
            while (reader.Read())
                {
                    guid = new Guid(Convert.ToString(reader[CONTENT_ID]));
                    //Debug.Log("getAllEntrys : " + guid.ToString());
                        if (Convert.ToString(reader[CONTENT_TYP]) == "Text")
                        {
                            TextContentData content_text = new TextContentData(guid, Convert.ToString(reader[CONTENT_INHALT]));
                            ed = new EntryData(guid, content_text);
                        } else
                        {
                            DataContentData content_data = new DataContentData(guid, Convert.ToString(reader[CONTENT_INHALT]));
                            ed = new EntryData(guid, content_data);
                        }
                    meta = getAllMetadata(guid);
                    foreach (MetaData item in meta)
                    {
                        ed.AddMetaData(item);
                    }    
                entries.Add(ed);
                }
            
            return entries;
        }

        public EntryData getEntry(Guid guid)
        {
            EntryData ed = null;
            List<MetaData> meta = new List<MetaData>();
            IDataReader reader;
            IDbCommand dbcmd = getDbCommand();
            dbcmd.CommandText = "SELECT * FROM " + TABLE_NAME_CONTENT + " WHERE " + CONTENT_ID + " ='" + guid.ToString() + "'" ;
            reader = dbcmd.ExecuteReader();
            while (reader.Read())
            {
                if (Convert.ToString(reader[CONTENT_TYP]) == "Text")
                {
                    TextContentData content_text = new TextContentData(guid, Convert.ToString(reader[CONTENT_INHALT]));
                    ed = new EntryData(guid, content_text);
                }
                else
                {
                    DataContentData content_data = new DataContentData(guid, Convert.ToString(reader[CONTENT_INHALT]));
                    ed = new EntryData(guid, content_data);
                }
                meta = getAllMetadata(guid);
                foreach (MetaData item in meta)
                {
                    ed.AddMetaData(item);
                }
            }
            return ed;
        }
    }
}
