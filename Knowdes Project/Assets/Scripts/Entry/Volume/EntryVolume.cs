using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knowdes
{
    public class EntryVolume : MonoBehaviour
    {
        [SerializeField]
        private RectTransform _base;
        public RectTransform Base => _base;

        public EntryData Data { get; set; }

        private bool _expanded = false;
        public event Action OnExpanded;
        public bool Expanded
		{
            get => _expanded;
            set
			{
                _expanded = value;
                OnExpanded?.Invoke();
			}
		}

        private Content _content = null;
        public event Action OnContentChanged;
        public Content Content
		{
            get => _content;
			set
			{
                _content = value;
                OnContentChanged?.Invoke();
			}
		}

        private Dictionary<MetaDataType, MetaContent> _metaContent = new Dictionary<MetaDataType, MetaContent>();
        public Dictionary<MetaDataType, MetaContent> MetaContentCopy => new Dictionary<MetaDataType, MetaContent>(_metaContent);
        public event Action<MetaContent> OnMetaContentAdded;
        public event Action<MetaContent> OnMetaContentRemoved;

        public void AddMetaContent(MetaContent content)
		{
            if (_metaContent.ContainsKey(content.Type))
                throw new InvalidOperationException();
            _metaContent.Add(content.Type, content);
            OnMetaContentAdded?.Invoke(content);
		}

        public void RemoveMetaContent(MetaContent content)
		{
            if (!_metaContent.ContainsKey(content.Type))
                throw new InvalidOperationException();
            _metaContent.Remove(content.Type);
            OnMetaContentRemoved?.Invoke(content);
        }

        public MetaContent Get(MetaDataType type)
		{
            return _metaContent[type];
		}
    }
}