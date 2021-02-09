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

        public EntryData Create(ContentData content)
		{
			EntryData result = new EntryData(content);
			initNewEntryData(result);
			return result;
		}

		private void initNewEntryData(EntryData result)
		{
			MetaData creationDateData = _factory.CreateNew(MetaDataType.CreationDate);
			MetaData lastChangeDateData = _factory.CreateNew(MetaDataType.LastChangedDate);
			result.AddMetaData(creationDateData);
			result.AddMetaData(lastChangeDateData);
		}
    }
}