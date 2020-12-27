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
        public RectTransform RectTrans => _rect;

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

        public void Add(WorkspaceEntry entry, Vector2 screenPos)
        {
            _entries.Add(entry);
            entry.Base.SetParent(_hook, true);
            Vector2 rectPoint;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(Content, screenPos, GetComponentInParent<Canvas>().worldCamera, out rectPoint);
            Vector2 normalized = Rect.PointToNormalized(Content.rect, rectPoint);
            entry.Base.anchorMin = normalized;
            entry.Base.anchorMax = normalized;
            entry.Base.anchoredPosition = Vector2.zero;
            entry.CursorFollower.Container = Content;

            OnCountChanged?.Invoke();
            OnEntryAdded?.Invoke(entry);
        }



        public void AddToCenter(WorkspaceEntry entry)
		{
            Add(entry, RectTrans.position);
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

        public bool HasEntry(Entry entry)
		{
            return _entries.FirstOrDefault(e => e.LinkedEntry == entry) != null;
        }
    }
}