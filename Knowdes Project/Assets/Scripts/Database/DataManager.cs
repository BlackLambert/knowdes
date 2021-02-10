using System.Collections.Generic;

namespace Knowdes
{
    public class DataManager : SqliteHelper
    {
        private AuthorTable _authorTable;
        private TagsTable _tagsTable;
        private MetaDataTable _metaDataTable;
        private ContentDataTable _contentDataTable;

        public DataManager() : base()
		{
			initTables();
		}

		private void initTables()
		{
            initAuthorTable();
            initTagTable();
            initMetaDataTable();
            initContentTable();
		}

		private void initAuthorTable()
		{
            _authorTable = new AuthorTable(_db_connection);
            _authorTable.Init();
        }

        private void initContentTable()
        {
            _contentDataTable = new ContentDataTable(_db_connection);
            _contentDataTable.Init();
        }

        private void initMetaDataTable()
        {
            _metaDataTable = new MetaDataTable(_db_connection, _authorTable, _tagsTable);
            _metaDataTable.Init();
        }

        private void initTagTable()
        {
            _tagsTable = new TagsTable(_db_connection);
            _tagsTable.Init();
        }

        public void AddOrReplaceEntryData(EntryData entry)
        {
            _contentDataTable.AddOrReplaceContentOf(entry);
            _metaDataTable.AddOrReplaceAllMetadataOf(entry);
        }

        public void DeleteEntry(EntryData entryData)
        {
            _contentDataTable.DeleteContentOf(entryData);
            _metaDataTable.DeleteAllMetaDataOf(entryData);
        }

        public List<EntryData> GetAllEntrys()
        {
            List<EntryData> entries = new List<EntryData>();
            List<ContentData> content = _contentDataTable.GetAllContentData();
            foreach (ContentData data in content)
                entries.Add(createEntryDataFrom(data));
            return entries;
        }

		private EntryData createEntryDataFrom(ContentData data)
        {
            EntryData result = new EntryData(data);
            List<MetaData> metaData = _metaDataTable.GetAllMetadataOf(data.Id);
            foreach (MetaData item in metaData)
                result.AddMetaData(item);
            return result;
        }
    }
}
