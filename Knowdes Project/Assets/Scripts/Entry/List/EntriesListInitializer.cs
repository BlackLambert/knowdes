using UnityEngine;

namespace Knowdes
{
    public class EntriesListInitializer : MonoBehaviour
    {
        [SerializeField]
        private EntriesList _entriesList;
        private EntryDataRepository _entryDataRepository;
		private EntryFactory _factory;

        protected virtual void Start()
		{
			_factory = FindObjectOfType<EntryFactory>();
			_entryDataRepository = FindObjectOfType<EntryDataRepository>();
			initEntries();
		}

		private void initEntries()
		{
			foreach (EntryData data in _entryDataRepository.Datas)
				_entriesList.Add(createEntry(data));
		}

		private Entry createEntry(EntryData data)
		{
			return _factory.Create(data);
		}
	}
}