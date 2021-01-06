using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Knowdes.Prototype
{
    public class PlaceEntryInWorkspaceOnClick : MonoBehaviour
    {
        [SerializeField]
        private Entry _entry = null;
        [SerializeField]
        private Button _button = null;
        [SerializeField]
        private WorkspaceEntry _entryPrefab = null;

        private Workspace _workspace;
        private EntryVolumeFactory _volumeFactory;

        protected virtual void Start()
		{
            _workspace = FindObjectOfType<Workspace>();
            _volumeFactory = FindObjectOfType<EntryVolumeFactory>();
            _button.onClick.AddListener(onClick);
        }

		protected virtual void OnDestroy()
		{
            _button.onClick.RemoveListener(onClick);
        }

        private void onClick()
        {
            WorkspaceEntry newEntry = Instantiate(_entryPrefab);
            newEntry.SetContent(_volumeFactory.Create(_entry.Volume));
            newEntry.LinkedEntry = _entry;
            newEntry.Content.Data = _entry.Volume.Data;
            _workspace.AddToCenter(newEntry);
        }
    }
}