using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knowdes
{
    public abstract class MetaContent : MonoBehaviour
	{
        [SerializeField]
        private RectTransform _base;
        public RectTransform Base => _base;

        private EntryVolume _volume;
        public EntryVolume Volume
        {
            get => _volume;
            set
            {
                removeVolumeListeners();
                _volume = value;
                updateVisibility();
                addVolumeListeners();
            }
        }

        public MetaDataType Type { get => MetaData.Type; }

        public abstract MetaData MetaData { get; }

        protected virtual void OnDestroy()
        {
            removeVolumeListeners();
        }



        protected void updateVisibility()
        {
            if (_volume == null || MetaData == null)
                return;
            _base.gameObject.SetActive(_volume != null && (_volume.Expanded || this.MetaData.ShowInPreview));
        }

        private void addVolumeListeners()
        {
            if (_volume != null)
                _volume.OnExpanded += updateVisibility;
        }

        private void removeVolumeListeners()
        {
            if (_volume != null)
                _volume.OnExpanded -= updateVisibility;
        }
    }

    public abstract class MetaContent<D> : MetaContent where D : MetaData
    {
		public override MetaData MetaData => Data;

		private D _data;
        public D Data
        {
            get => _data;
            set
			{
                if (_data != null)
                {
                    removeVisabilityListeners();
                    onDataRemoved(_data);
                }
                _data = value;
                if (_data != null)
                {
                    onDataAdded(_data);
                    updateVisibility();
                    addVisabilityListeners();
                }
            }
        }

		protected override void OnDestroy()
		{
            base.OnDestroy();
            onDataRemoved(_data);
        }


        protected abstract void onDataAdded(D data);
        protected abstract void onDataRemoved(D data);


        private void addVisabilityListeners()
		{
            if (_data == null)
                return;
            _data.OnShowInPreviewChanged += updateVisibility;

        }

        private void removeVisabilityListeners()
		{
            if (_data == null)
                return;
            _data.OnShowInPreviewChanged -= updateVisibility;
        }
    }
}