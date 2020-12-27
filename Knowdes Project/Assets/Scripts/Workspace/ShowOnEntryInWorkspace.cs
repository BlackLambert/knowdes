using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knowdes.Prototype
{
    public class ShowOnEntryInWorkspace : MonoBehaviour
    {
        [SerializeField]
        private GameObject _target = null;
        [SerializeField]
        private Entry _entry = null;
        [SerializeField]
        private bool _show = true;

        private Workspace _workspace;


        protected virtual void Start()
		{
            _workspace = FindObjectOfType<Workspace>();
            _workspace.OnEntryAdded += updateDisplayOnAdded;
            _workspace.OnEntryRemoved += updateDisplayOnRemoved;
            bool hasEntry = _workspace.HasEntry(_entry);
            _target.SetActive(hasEntry && _show || !hasEntry && !_show);
        }

		protected virtual void OnDestroy()
		{
            if (_workspace != null)
            {
                _workspace.OnEntryAdded -= updateDisplayOnAdded;
                _workspace.OnEntryRemoved -= updateDisplayOnRemoved;
            }
        }

        private void updateDisplayOnAdded(WorkspaceEntry entry)
        {
            if (entry.LinkedEntry != _entry)
                return;
            _target.SetActive(_show);
        }

        private void updateDisplayOnRemoved(WorkspaceEntry entry)
        {
            if (entry.LinkedEntry != _entry)
                return;
            _target.SetActive(!_show);
        }
    }
}