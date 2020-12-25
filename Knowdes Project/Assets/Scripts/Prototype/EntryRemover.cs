using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knowdes.Prototype
{
    public class EntryRemover : MonoBehaviour
    {
        [SerializeField]
        private EntriesList _entries = null;

        protected virtual void Start()
		{
            _entries.OnEntryAdded += onAdded;
        }

		protected virtual void OnDestroy()
		{
            _entries.OnEntryAdded -= onAdded;
        }

        private void onAdded(Entry entry)
        {
            entry.OnDestruct += destruct;
        }

		private void destruct(Entry entry)
		{
            entry.OnDestruct -= destruct;
            _entries.Remove(entry);
		}
	}
}