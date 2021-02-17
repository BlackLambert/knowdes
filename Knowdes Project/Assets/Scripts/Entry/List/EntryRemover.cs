
using System;
using UnityEngine;

namespace Knowdes.Prototype
{
    public class EntryRemover : MonoBehaviour
    {
        [SerializeField]
        private Entry _entry;

        private EntriesList _entries;
        private EntryDataRepository _dataRepository;
        private FileManager _fileManager;

        protected virtual void Start()
		{
            _entries = FindObjectOfType<EntriesList>();
            _dataRepository = FindObjectOfType<EntryDataRepository>();
            _fileManager = new FileManager();
            _entry.OnMarkedForDestruct += destruct;
        }

		protected virtual void OnDestroy()
		{
            _entry.OnMarkedForDestruct -= destruct;
        }

		private void destruct()
        {
            removedManagedContent(_entry.Data);
            _entries.Remove(_entry);
            _dataRepository.Delete(_entry.Data);
            _entry.Destroy();
        }

		private void removedManagedContent(EntryData data)
		{
            if (!(data.Content is FilebasedContentData))
                return;
            FilebasedContentData filebasedContent = data.Content as FilebasedContentData;
            if (!_fileManager.IsManagedFile(filebasedContent.Path))
                return;
            _fileManager.DeleteFile(filebasedContent.Path);
        }
	}
}