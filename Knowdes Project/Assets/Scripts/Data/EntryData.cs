using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Knowdes
{
    public class EntryData : IDisposable
    {
        public Guid Id => _content.Id;

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

        public event Action OnChanged;

        public EntryData(ContentData content)
		{
            _content = content;
            init();
        }

        public EntryData(
            ContentData content,
            Dictionary<MetaDataType, MetaData> metaDatas)
        {
            _content = content;
            _metaDatas = metaDatas;
            init();
        }

        private void init()
		{
            _content.OnChanged += invokeOnChanged;
		}

        public void Dispose()
		{
            _content.OnChanged -= invokeOnChanged;
            foreach (MetaData data in _metaDatas.Values)
                data.OnChanged -= invokeOnChanged;
        }

        public void AddMetaData(MetaData metaData)
		{
            if (_metaDatas.ContainsKey(metaData.Type))
                throw new MetaDataAlreadyAddedException();
            _metaDatas.Add(metaData.Type, metaData);
            OnMetaDataAdded?.Invoke(metaData);
            metaData.OnChanged += invokeOnChanged;
            invokeOnChanged();
        }

        public void RemoveMetaData(MetaData metaData)
		{
            if (!_metaDatas.ContainsKey(metaData.Type))
                throw new MetaDataNotAddedException();
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
            OnChanged?.Invoke();
        }

		public override string ToString()
		{
            return $"EntryData (ID: {Id} | Content: {_content.Type.GetName()} | Meta Data Count: {_metaDatas.Count}";
		}

        public class MetaDataAlreadyAddedException : InvalidOperationException { }
        public class MetaDataNotAddedException : InvalidOperationException { }
    }
}