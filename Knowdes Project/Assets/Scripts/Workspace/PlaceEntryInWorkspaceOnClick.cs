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

        protected virtual void Start()
		{
            _workspace = FindObjectOfType<Workspace>();
            _button.onClick.AddListener(onClick);
        }

		protected virtual void OnDestroy()
		{
            _button.onClick.RemoveListener(onClick);
        }

        private void onClick()
        {
            WorkspaceEntry newEntry = Instantiate(_entryPrefab);
            newEntry.SetContent(Instantiate(_entry.Content));
            newEntry.LinkedEntry = _entry;
            _workspace.AddToCenter(newEntry);
        }
    }
}