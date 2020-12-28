using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Knowdes.Prototype
{
    public class AddRandomEntryButton : MonoBehaviour
    {
        [SerializeField]
        private Button _addButton = null;
        [SerializeField]
        private Entry _entryPrefab = null;
		[SerializeField]
		private EntriesList _entries = null;

		[SerializeField]
		private List<EntryContent> _contentPrefabs = new List<EntryContent>();

        protected virtual void Start()
		{
            _addButton.onClick.AddListener(onClick);
		}

		protected virtual void OnDestroy()
		{
			_addButton.onClick.RemoveListener(onClick);
		}

		private void onClick()
		{
			Entry newEntry = Instantiate(_entryPrefab, null);
			newEntry.Base.localScale = Vector3.one;
			EntryContent content = createRandomContent();
			newEntry.SetContent(content);
			_entries.Add(newEntry);
		}

		private EntryContent createRandomContent()
		{
			int index = UnityEngine.Random.Range(0, _contentPrefabs.Count );
			return Instantiate(_contentPrefabs[index]);
		}
	}
}