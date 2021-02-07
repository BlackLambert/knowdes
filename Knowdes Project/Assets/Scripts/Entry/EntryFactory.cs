using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knowdes.Prototype
{
    public class EntryFactory : MonoBehaviour
    {
        [SerializeField]
        private Entry _prefab;
        private EntryDataFactory _entryDataFactory;
        private ContentDataFactory _contentDataFactory;
        private EntriesList _entries;

        protected virtual void Start()
		{
            _entryDataFactory = new EntryDataFactory();
            _contentDataFactory = new ContentDataFactory();
            _entries = FindObjectOfType<EntriesList>();
        }

        public Entry CreateNew(ContentType type)
		{
            ContentData content = _contentDataFactory.Create(type);
            EntryData data = _entryDataFactory.CreateNew(content);
            Entry result = Instantiate(_prefab);
            result.Volume.Data = data;
            result.Selectable.TrySelect();
            _entries.Add(result);
            return result;
        }
	}
}