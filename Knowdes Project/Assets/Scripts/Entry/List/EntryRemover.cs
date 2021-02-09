
using UnityEngine;

namespace Knowdes.Prototype
{
    public class EntryRemover : MonoBehaviour
    {
        [SerializeField]
        private Entry _entry;

        private EntriesList _entries;
        private EntryDataRepository _dataRepository;

        protected virtual void Start()
		{
            _entries = FindObjectOfType<EntriesList>();
            _dataRepository = FindObjectOfType<EntryDataRepository>();
            _entry.OnMarkedForDestruct += destruct;
        }

		protected virtual void OnDestroy()
		{
            _entry.OnMarkedForDestruct -= destruct;
        }

		private void destruct()
		{
            _dataRepository.Delete(_entry.Data);
            _entries.Remove(_entry);
            _entry.Destroy();
        }
	}
}