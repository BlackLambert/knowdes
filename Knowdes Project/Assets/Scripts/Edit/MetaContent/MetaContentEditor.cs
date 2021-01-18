using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knowdes
{
    public abstract class MetaContentEditor : MonoBehaviour
    {

        [SerializeField]
        private RectTransform _base;
        public RectTransform Base => _base;

        public abstract MetaData MetaData { get; }

        protected virtual void OnDestroy()
		{

		}
    }

    public abstract class MetaContentEditor<D> : MetaContentEditor where D : MetaData
    {
        private D _data;
        public D Data
        {
            get => _data;
            set
            {
                onDataRemoved(_data);
                _data = value;
                onDataAdded(_data);
            }
        }

		public override MetaData MetaData => _data;

        protected override void OnDestroy()
        {
            base.OnDestroy();
            onDataRemoved(_data);
        }

        protected abstract void onDataAdded(D data);
        protected abstract void onDataRemoved(D data);
    }
}