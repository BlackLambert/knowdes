using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knowdes
{
    public class EntryDataFactory
    {
        private MetaDataFactory _factory;

        public EntryDataFactory()
		{
            _factory = new MetaDataFactory();

        }

        public EntryData CreateNew(ContentData content)
		{
            return CreateNew(Guid.NewGuid(), content);
		}

        public EntryData CreateNew(Guid iD, ContentData content)
		{
            EntryData result = new EntryData(iD, content);
            MetaData creationDateData = _factory.CreateNew(MetaDataType.CreationDate);
            MetaData lastChangeDateData = _factory.CreateNew(MetaDataType.LastChangedDate);
            result.AddMetaData(creationDateData);
            result.AddMetaData(lastChangeDateData);
            result.OnChanged += updateLastChangeDate;
            return result;
        }

        //TODO: Put in own class! But only if there is a repository.
        private void updateLastChangeDate(EntryData data)
		{
            LastChangeDateData lastChangeDateData = data.Get(MetaDataType.LastChangedDate) as LastChangeDateData;
            lastChangeDateData.Date = DateTime.Now;
		}
    }
}