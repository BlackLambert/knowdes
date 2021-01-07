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

        public abstract MetaDataType Type {get;}
        public abstract MetaData MetaData { get; }


        protected virtual void OnDestroy()
		{
            removeVolumeListeners();

        }


        protected void updateVisibility()
        {
            _base.gameObject.SetActive(_volume != null && (_volume.Expanded || MetaData.ShowInPreview));
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
}