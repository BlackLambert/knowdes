using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Knowdes
{
    public class EntriesList : MonoBehaviour
    {
        public int Count => _entries.Count;
        public int VisableCount => _entries.Where(e => e.Visable).Count();
        public event Action OnCountChanged;
        public event Action<Entry> OnEntryAdded;
        public event Action<Entry> OnEntryRemoved;
        public event Action OnVisableCountChanged;

        private List<Entry> _entries = new List<Entry>();
        public List<Entry> EntriesCopy => new List<Entry>(_entries);

        [SerializeField]
        private Transform _hook = null;

        public void Add(Entry entry)
		{
            _entries.Add(entry);
            entry.Base.SetParent(_hook, false);
            entry.OnVisableChanged += invokeOnVisableCountChanged;
            OnCountChanged?.Invoke();
            OnEntryAdded?.Invoke(entry);
        }

		public void Remove(Entry entry)
		{
            _entries.Remove(entry);
            OnCountChanged?.Invoke();
            OnEntryRemoved?.Invoke(entry);
            entry.OnVisableChanged -= invokeOnVisableCountChanged;
            invokeOnVisableCountChanged();
        }

        private void invokeOnVisableCountChanged()
        {
            OnVisableCountChanged?.Invoke();
        }
    }
}