using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knowdes.Prototype
{
    public class CreateEditPanelPreview : MonoBehaviour
    {
        [SerializeField]
        private RectTransform _hook = null;
        [SerializeField]
        private bool _extended = true;

        private EditPanel _editPanel;
        private EntryVolume _entryVolume = null;

        private EntryVolumeFactory _volumeFactory;


        protected virtual void Start()
		{
            _editPanel = FindObjectOfType<EditPanel>();
            _editPanel.OnEntryChanged += onEntryChanged;
            _volumeFactory = FindObjectOfType<EntryVolumeFactory>();
            createPreview();
        }

        protected virtual void OnDestroy()
		{
            if(_editPanel != null)
                _editPanel.OnEntryChanged -= onEntryChanged;
        }

		private void onEntryChanged()
		{
            removePreview();
            createPreview();
		}

        private void removePreview()
		{
            if (_entryVolume == null)
                return;
            Destroy(_entryVolume.Base.gameObject);
            _entryVolume = null;
		}

        private void createPreview()
		{
            if (_entryVolume != null)
                throw new InvalidOperationException();
            if (_editPanel.Entry == null)
                return;
            EntryVolume entryContent = _volumeFactory.Create(_editPanel.Entry.Volume);
            entryContent.Base.SetParent(_hook, false);
            entryContent.Base.localScale = Vector3.one;
            entryContent.Expanded = _extended;
            _entryVolume = entryContent;
        }
	}
}