using System;
using UnityEngine;

namespace Knowdes
{
    public class ImageEntryCreatorOnFileDrop : MonoBehaviour
    {
        [SerializeField]
        private FileDropReceiver _dropReceiver;

        private ImageFilePathConverter _converter;
        private ContentDataFactory _contentDataFactory;
        private EntryCreator _entryCreator;
        private FileManager _fileManager;

        protected virtual void Start()
		{
            _converter = new ImageFilePathConverter();
            _fileManager = new FileManager();
            _dropReceiver.OnFileDropped += onFileDropped;
            _contentDataFactory = new ContentDataFactory();
            _entryCreator = FindObjectOfType<EntryCreator>();
        }

		protected virtual void OnDestroy()
		{
            _dropReceiver.OnFileDropped -= onFileDropped;
        }

        private void onFileDropped(Uri uri, Vector2 point)
        {
            ImageFilePathConverter.Result result = _converter.Convert(uri.AbsoluteUri);
            if (!result.Successful)
                return;
            Debug.LogError($"Image created {uri.AbsoluteUri}");
            ImageContentData content = _contentDataFactory.Create(ContentType.Image) as ImageContentData;
            content.Path = uri.AbsoluteUri;
            string managedFilePath = _fileManager.SaveDatei(result.Uri.AbsolutePath);
            content.Path = managedFilePath;
            _entryCreator.Create(content);
        }
    }
}