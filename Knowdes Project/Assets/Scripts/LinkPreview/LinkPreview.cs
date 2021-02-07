using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace Knowdes
{
    public class LinkPreview : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI _titleText;
        [SerializeField]
        private TextMeshProUGUI _descriptionText;
        [SerializeField]
        private RawImage _previewImage;
        [SerializeField]
        private AspectRatioFitter _imageFitter;
        [SerializeField]
        private GameObject _container;

        private Uri _uri = null;
        public Uri URI
		{
            set
			{
                _uri = value;
                updatePreview();
            }
		}

        private LinkPreviewLoader _linkPreviewLoader = null;
        private ImageLoader _remoteImageLoader = null;
        private LinkPreviewLoader.LoadRequest _fetchPreviewRequest = null;
        private ImageLoader.LoadRequest _loadImageRequest = null;
        private LinkPreviewData _data = null;
        private bool _uriIsEmpty => _uri == null || string.IsNullOrEmpty(_uri.AbsoluteUri);
        private bool _hasPreviewData => _data != null;
        private bool _dataHasImagePath => _hasPreviewData && !string.IsNullOrEmpty(_data.image);

        protected virtual void Awake()
		{
            createLoaders();
            reset();
        }

        protected virtual void OnDestroy()
		{
            destroyLoaders();
        }

        private void updatePreview()
		{
            reset();
            tryLoadingPreview();
        }

		private void onPreviewLoaded(LinkPreviewLoader.Result result)
		{
            _fetchPreviewRequest = null;
            updateDataBasedOn(result);
            updateView();
        }

        private void updateView()
        {
            if (_hasPreviewData)
            {
                updateViewContent();
                tryLoadingPreviewImage();
            }
            updateVisablity();
        }

        private void tryLoadingPreviewImage()
		{
            if (_dataHasImagePath)
                loadPreviewImage();
            else
                clearImage();
        }

        private void onPreviewImageLoaded(ImageLoader.Result result)
        {
            _loadImageRequest = null;
            updateImage(result.Texture);
        }

        private void createLoaders()
		{
            _linkPreviewLoader = LinkPreviewLoader.New();
            _remoteImageLoader = ImageLoader.New();
        }

        private void destroyLoaders()
		{
            if(_linkPreviewLoader != null)
                _linkPreviewLoader.Destroy();
            if (_remoteImageLoader != null)
                _remoteImageLoader.Destroy();
        }

        private void reset()
		{
            _container.SetActive(false);
            _data = null;
            _previewImage.texture = null;
            stopCurrentImageLoading();
            stopCurrentPreviewLoading();
        }

        private void tryLoadingPreview()
        {
            if (_uriIsEmpty)
                return;
            loadPreview();
        }

        private void loadPreview()
		{
            _fetchPreviewRequest = new LinkPreviewLoader.LoadRequest(_uri);
            _linkPreviewLoader.LoadPreview(_fetchPreviewRequest, onPreviewLoaded);
        }

        private void updateVisablity()
        {
            _container.SetActive(_hasPreviewData);
        }

        private void updateImage(Texture texture)
		{
            _previewImage.texture = texture;
            _previewImage.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, _previewImage.texture.width);
            _previewImage.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, _previewImage.texture.height);
            float ratio = (float)_previewImage.texture.width / (float)_previewImage.texture.height;
            _imageFitter.aspectRatio = ratio;
        }

        private void clearImage()
		{
            _previewImage.texture = null;
            _imageFitter.aspectRatio = 0;
        }

        private void updateDataBasedOn(LinkPreviewLoader.Result result)
        {
            if (result.RequestWasSuccessful)
                _data = result.Data;
        }

        private void stopCurrentPreviewLoading()
        {
            if (_fetchPreviewRequest == null)
                return;
            _linkPreviewLoader.StopLoadingPreview(_fetchPreviewRequest);
            _fetchPreviewRequest = null;
        }
        private void loadPreviewImage()
        {
            _loadImageRequest = new ImageLoader.LoadRequest(new Uri(_data.image));
            _remoteImageLoader.LoadImage(_loadImageRequest, onPreviewImageLoaded);
        }

        private void stopCurrentImageLoading()
        {
            if (_loadImageRequest == null)
                return;
            _remoteImageLoader.CancelLoadingImage(_loadImageRequest);
            _loadImageRequest = null;
        }

        private void updateViewContent()
        {
            _titleText.text = _data.title;
            _descriptionText.text = _data.description;
        }
    }
}