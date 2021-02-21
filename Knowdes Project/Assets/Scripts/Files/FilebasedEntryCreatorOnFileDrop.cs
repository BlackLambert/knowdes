using System;
using System.IO;
using UnityEngine;

namespace Knowdes
{
    public class FilebasedEntryCreatorOnFileDrop : MonoBehaviour
    {
        private const string _fileNotFoundError = "Die Datei '{0}' konnte nicht gefunden werden";
        private const string _fileSavingFailed = "Die Datei '{0}' konnte nicht kopiert werden";
        private const float _errorDisplayTime = 3;

        [SerializeField]
        private FileDropReceiver _dropReceiver;
        private TemporaryNotificationDisplayer _notificationDisplayer;

        private FileContentDataFactory _contentDataFactory;
        private EntryCreator _entryCreator;
        private FileManager _fileManager;

        protected virtual void Start()
		{
            _fileManager = new FileManager();
            _contentDataFactory = new FileContentDataFactory();
            _entryCreator = FindObjectOfType<EntryCreator>();
            _notificationDisplayer = FindObjectOfType<TemporaryNotificationDisplayer>();
            _dropReceiver.OnFileDropped += onFileDropped;
            //Uncomment for testing the file dropping
            //onFileDropped(new Uri(@"W:\Studium\WissenschaftlichesProjekt\Entwicklung\Knowdes_Merkmale201201.pdf"), Vector2.zero);
            //onFileDropped(new Uri(@"W:\Studium\UserExperience\6_UX-Entwicklungsprozess\Schön-2016-Kanban.pdf"), Vector2.zero);
        }

        protected virtual void OnDestroy()
		{
            _dropReceiver.OnFileDropped -= onFileDropped;
        }

        private void onFileDropped(Uri uri, Vector2 point)
        {
            string managedFilePath = tryToSaveFile(uri.LocalPath);
            if (string.IsNullOrEmpty(managedFilePath))
                return;
            ContentData content = _contentDataFactory.Create(managedFilePath);
            _entryCreator.Create(content);
        }

        private string tryToSaveFile(string uriFilePath)
        {
            try
            {
                return _fileManager.SaveFile(uriFilePath);
            }
            catch (FileNotFoundException)
			{
                string error = string.Format(_fileNotFoundError, uriFilePath);
                _notificationDisplayer.RequestDisplay(new TemporaryNotificationDisplayer.Request(error, _errorDisplayTime));
            }
            catch (IOException)
			{
                string error = string.Format(_fileSavingFailed, uriFilePath);
                _notificationDisplayer.RequestDisplay(new TemporaryNotificationDisplayer.Request(error, _errorDisplayTime));
            }
            return string.Empty;
        }
    }
}