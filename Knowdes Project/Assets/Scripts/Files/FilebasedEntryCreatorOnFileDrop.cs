using System;
using UnityEngine;

namespace Knowdes
{
    public class FilebasedEntryCreatorOnFileDrop : MonoBehaviour
    {
        [SerializeField]
        private FileDropReceiver _dropReceiver;

        private FileContentDataFactory _contentDataFactory;
        private EntryCreator _entryCreator;
        private FileManager _fileManager;

        protected virtual void Start()
		{
            _fileManager = new FileManager();
            _contentDataFactory = new FileContentDataFactory();
            _entryCreator = FindObjectOfType<EntryCreator>();
            _dropReceiver.OnFileDropped += onFileDropped;
            //Uncomment for testing the file dropping
            //onFileDropped(new Uri(@"W:\Studium\WissenschaftlichesProjekt\Entwicklung\Knowdes_Merkmale201201.pdf"), Vector2.zero);
        }

		protected virtual void OnDestroy()
		{
            _dropReceiver.OnFileDropped -= onFileDropped;
        }

        private void onFileDropped(Uri uri, Vector2 point)
        {
            string managedFilePath = _fileManager.SaveFile(uri.AbsolutePath);
            ContentData content = _contentDataFactory.Create(managedFilePath);
            _entryCreator.Create(content);
        }
    }
}