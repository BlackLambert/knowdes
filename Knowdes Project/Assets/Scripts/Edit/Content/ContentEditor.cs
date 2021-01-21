using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knowdes
{
    public abstract class ContentEditor : MonoBehaviour
    {
        [SerializeField]
        private RectTransform _base;
        public RectTransform Base => _base;

        public abstract ContentData ContentData { get; }
        public abstract ContentDataType Type { get; }
    }

    public abstract class ContentEditor<C> : ContentEditor where C : ContentData
	{
        public override ContentData ContentData => _data;
        public override ContentDataType Type => ContentData.Type;

        private C _data;
        public C Data
        {
            get => _data;
            set
            {
                onDataRemoved(_data);
                _data = value;
                onDataAdded(_data);
            }
        }

        protected virtual void OnDestroy()
        {
            onDataRemoved(_data);
        }

        protected abstract void onDataAdded(C data);

        protected abstract void onDataRemoved(C data);
    }
}