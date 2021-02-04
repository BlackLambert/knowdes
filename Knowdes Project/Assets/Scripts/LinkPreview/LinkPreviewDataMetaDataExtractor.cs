using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knowdes
{
    public class LinkPreviewDataMetaDataExtractor : MonoBehaviour
    {
        private const string _loadingText = "Lade Metadaten";

        [SerializeField]
        private WeblinkContentEditor _weblinkEditor = null;
        private LoadingOverlay _loadingOverlay = null;
        private LinkPreviewLoader _linkPreviewLoader;
        private MetaDataFactory _metaDataFactory;
        private ImageFilePathConverter _imageFilePathConverter;
        private LinkPreviewLoader.LoadRequest _loadRequest;

        protected virtual void Start()
		{
            _loadingOverlay = FindObjectOfType<LoadingOverlay>();
            _linkPreviewLoader = LinkPreviewLoader.New();
            _metaDataFactory = new MetaDataFactory();
            _imageFilePathConverter = new ImageFilePathConverter();
            _weblinkEditor.OnChanged += onContentChanged;
        }

		protected virtual void OnDestroy()
		{
            tryToStopLoadingPreview();
            if (_linkPreviewLoader != null)
                _linkPreviewLoader.Destroy();
            _weblinkEditor.OnChanged -= onContentChanged;
        }

        private void onContentChanged()
        {
            tryToStopLoadingPreview();
            if (!_weblinkEditor.Data.Empty)
                loadPreview();
        }

		private void loadPreview()
		{
            _loadRequest = new LinkPreviewLoader.LoadRequest(_weblinkEditor.Data.Url);
            _linkPreviewLoader.LoadPreview(_loadRequest, onPreviewLoaded);
            _loadingOverlay.Show(_loadingText);
        }

		private void onPreviewLoaded(LinkPreviewLoader.Result result)
		{
            disposeRequest();
            if (!result.RequestWasSuccessful)
                return;
            List<MetaData> extractedMetaData = extractMetaDataFrom(result.Data);
            addMissingMetaDataTo(_weblinkEditor.EntryData, extractedMetaData);
        }

		private void addMissingMetaDataTo(EntryData entryData, List<MetaData> extractedMetaData)
		{
			foreach(MetaData metaData in extractedMetaData)
			{
                if (entryData.Contains(metaData.Type))
                    continue;
                entryData.AddMetaData(metaData);
            }
		}

		private List<MetaData> extractMetaDataFrom(LinkPreviewData data)
        {
            List<MetaData> extractedMetaData = new List<MetaData>();
            if (!string.IsNullOrEmpty(data.description))
                extractedMetaData.Add(extractDescriptionFrom(data));
            if(!string.IsNullOrEmpty(data.image) && imagePathIsValid(data.image))
                extractedMetaData.Add(extractPreviewImageFrom(data));
            if(!string.IsNullOrEmpty(data.title))
                extractedMetaData.Add(extractTitleFrom(data));
            return extractedMetaData;
        }

		private MetaData extractTitleFrom(LinkPreviewData data)
		{
            TitleData result = _metaDataFactory.CreateNew(MetaDataType.Title) as TitleData;
            result.Content = data.title;
            return result;
        }

        private bool imagePathIsValid(string path)
		{
            return _imageFilePathConverter.Convert(path).Successful;
        }

		private MetaData extractPreviewImageFrom(LinkPreviewData data)
		{
            PreviewImageData result = _metaDataFactory.CreateNew(MetaDataType.PreviewImage) as PreviewImageData;
            result.Uri = _imageFilePathConverter.Convert(data.image).Uri;
            return result;
        }

		private MetaData extractDescriptionFrom(LinkPreviewData data)
		{
            DescriptionData result = _metaDataFactory.CreateNew(MetaDataType.Description) as DescriptionData;
            result.Content = data.description;
            return result;
        }

		private void tryToStopLoadingPreview()
		{
            if (_loadRequest == null)
                return;
            cancelRequest();
        }

        private void cancelRequest()
		{
            _linkPreviewLoader.StopLoadingPreview(_loadRequest);
            disposeRequest();
        }

        private void disposeRequest()
		{
            _loadRequest = null;
            _loadingOverlay.Hide();
        }
    }
}