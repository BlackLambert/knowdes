using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knowdes
{
    public abstract class Content : MonoBehaviour
    {
        [SerializeField]
        private RectTransform _base;
        public RectTransform Base => _base;

        public abstract EntryData EntryData { get; set; }
        public abstract ContentData ContentData { get; }
        public abstract ContentDataType Type { get; }

    }

    public abstract class Content<C> : Content where C : ContentData
	{
        public override ContentData ContentData => Data;
        public override ContentDataType Type => ContentData.Type;

        public C Data => _entryData.Content as C;

        private EntryData _entryData;
        public override EntryData EntryData
		{
            get => _entryData;
            set
			{
                if(_entryData != null)
                    onDataRemoved(Data);
                _entryData = value;
                if (_entryData != null)
                    onDataAdded(Data);
            }
		}

        protected virtual void OnDestroy()
        {
            onDataRemoved(Data);
        }

        protected abstract void onDataAdded(C data);

        protected abstract void onDataRemoved(C data);
    }
}