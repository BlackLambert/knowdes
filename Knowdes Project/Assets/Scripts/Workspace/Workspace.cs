using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Knowdes.Prototype
{
    public class Workspace : MonoBehaviour
    {
        [SerializeField]
        private RectTransform _rect;
        public RectTransform Rect => _rect;

        [SerializeField]
        private RectTransform _content;
        public RectTransform Content => _content;

        [SerializeField]
        private RectTransform _hook;

        private List<WorkspaceEntry> _entries = new List<WorkspaceEntry>();
        public int Count => _entries.Count;
        public event Action OnCountChanged;
        public event Action<WorkspaceEntry> OnEntryAdded;
        public event Action<WorkspaceEntry> OnEntryRemoved;

        private PendingWorkspaceEntry _pendingEntry;
        public PendingWorkspaceEntry PendingEntry
		{
			get { return _pendingEntry; }
            set
			{
                _pendingEntry = value;
                OnPendingEntryChanged?.Invoke();
            }
		}
        public event Action OnPendingEntryChanged;

        public void Add(WorkspaceEntry entry)
        {
            _entries.Add(entry);
            entry.Base.SetParent(_hook, true);
            OnCountChanged?.Invoke();
            OnEntryAdded?.Invoke(entry);
        }

        public void Remove(WorkspaceEntry entry)
        {
            _entries.Remove(entry);
            Destroy(entry.Base.gameObject);
            OnCountChanged?.Invoke();
            OnEntryRemoved?.Invoke(entry);
        }

        public void DeleteWith(Entry entry)
		{
            IEnumerable<WorkspaceEntry> entriesToDelete = _entries.Where(e => e.LinkedEntry == entry);
            foreach(WorkspaceEntry entryToDelete in new List<WorkspaceEntry>(entriesToDelete))
			{
                Remove(entryToDelete);
            }
		}
    }
}