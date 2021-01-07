using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Knowdes
{
    public class EntryData 
    {
        public Guid ID { get; }

        private ContentData _content;
        public event Action OnContentChanged;
        public ContentData Content 
        {
            get => _content;
            set
			{
                _content = value;
                OnContentChanged?.Invoke();
                invokeOnChanged();
            }
        }

        private Dictionary<MetaDataType, MetaData> _metaDatas = new Dictionary<MetaDataType, MetaData>();
        public Dictionary<MetaDataType, MetaData> MetaDatasCopy => new Dictionary<MetaDataType, MetaData>(_metaDatas);
        public event Action<MetaData> OnMetaDataAdded;
        public event Action<MetaData> OnMetaDataRemoved;

        public event Action<EntryData> OnChanged;

        public EntryData(Guid iD, ContentData content)
		{
            ID = iD;
            _content = content;

        }

        public EntryData(Guid iD,
            ContentData content,
            Dictionary<MetaDataType, MetaData> metaDatas)
        {
            ID = iD;
            _content = content;
            _metaDatas = metaDatas;
        }

        public void AddMetaData(MetaData metaData)
		{
            if (_metaDatas.ContainsKey(metaData.Type))
                throw new InvalidOperationException();
            _metaDatas.Add(metaData.Type, metaData);
            OnMetaDataAdded?.Invoke(metaData);
            metaData.OnChanged += invokeOnChanged;
            invokeOnChanged();
        }

        public void RemoveMetaData(MetaData metaData)
		{
            if (!_metaDatas.ContainsKey(metaData.Type))
                throw new InvalidOperationException();
            _metaDatas.Remove(metaData.Type);
            OnMetaDataRemoved?.Invoke(metaData);
            metaData.OnChanged -= invokeOnChanged;
            invokeOnChanged();
        }

        public bool Contains(MetaDataType type)
		{
            return _metaDatas.ContainsKey(type);
		}

        public MetaData Get(MetaDataType type)
		{
            return _metaDatas[type];
		}

        private void invokeOnChanged()
		{
            OnChanged?.Invoke(this);
        }
    }
}