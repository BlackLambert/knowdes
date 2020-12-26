using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knowdes.Prototype
{
    public class DeleteWorkspaceEntrysOnEntryDelete : MonoBehaviour
    {
        [SerializeField]
        private EntriesList _entries = null;
        [SerializeField]
        private Workspace _workspace = null;

        protected virtual void Start()
		{
            _entries.OnEntryRemoved += onRemoved;

        }

        protected virtual void OnDestroy()
		{
            _entries.OnEntryRemoved -= onRemoved;
        }

		private void onRemoved(Entry entry)
		{
            _workspace.DeleteWith(entry);

        }
	}
}