using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knowdes.Prototype
{
    public class EntriesList : MonoBehaviour
    {
        public int Count => _entries.Count;
        public event Action OnCountChanged;
        public event Action<Entry> OnEntryAdded;
        public event Action<Entry> OnEntryRemoved;

        private List<Entry> _entries = new List<Entry>();

        [SerializeField]
        private Transform _hook = null;


        public void Add(Entry entry)
		{
            _entries.Add(entry);
            entry.Base.SetParent(_hook, false);
            OnCountChanged?.Invoke();
            OnEntryAdded?.Invoke(entry);
        }

        public void Remove(Entry entry)
		{
            _entries.Remove(entry);
            Destroy(entry.Base.gameObject);
            OnCountChanged?.Invoke();
            OnEntryRemoved?.Invoke(entry);
        }
    }
}