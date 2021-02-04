using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace Knowdes
{
	public class PreviewImageContent : MetaContent<PreviewImageData>
	{
		private const string _loadingErrorMessage = "Das gewünschte Vorschaubild konnte nicht geladen werden.";
		private const float _loadingErrorNotificationDisplayTime = 3f;

		[SerializeField]
		private GameObject _imageObject;
		[SerializeField]
		private RawImage _previewImage;
		[SerializeField]
		private AspectRatioFitter _imageAspectRatioFitter;

		private RemoteImageLoader.LoadRequest _loadRequest = null;
		private RemoteImageLoader _remoteImageLoader;
		private TemporaryNotificationDisplayer _notificationDisplayer;

		protected virtual void Awake()
		{
			_remoteImageLoader = RemoteImageLoader.New();
			_notificationDisplayer = FindObjectOfType<TemporaryNotificationDisplayer>();
		}

		protected override void OnDestroy()
		{
			base.OnDestroy();
			if(_remoteImageLoader != null)
				_remoteImageLoader.Destroy();
		}


		protected override void onDataAdded(PreviewImageData data)
		{
			if (data == null)
				return;
			tryToStopCurrentImageLoading();
			tryToLoadImage();
			data.OnPathChanged += onPathChanged;
		}

		protected override void onDataRemoved(PreviewImageData data)
		{
			tryToStopCurrentImageLoading();
			if (data == null)
				return;
			data.OnPathChanged -= onPathChanged;
		}

		private void onPathChanged()
		{
			clearImage();
			tryToStopCurrentImageLoading();
			tryToLoadImage();
		}

		private void tryToLoadImage()
		{
			if (Data.IsEmpty)
				clearImage();
			else
				loadImage();
		}

		private void tryToStopCurrentImageLoading()
		{
			if (_loadRequest == null)
				return;
			_remoteImageLoader.StopLoadingImage(_loadRequest);
			_loadRequest = null;
		}

		private void loadImage()
		{
			_loadRequest = new RemoteImageLoader.LoadRequest(Data.Uri);
			_remoteImageLoader.LoadImage(_loadRequest, onImageLoaded);
		}

		private void onImageLoaded(RemoteImageLoader.Result result)
		{
			_loadRequest = null;
			if (result.RequestWasSuccessful)
				updateImage(result.Texture);
			else
				displayErrorNotification();
		}

		private void displayErrorNotification()
		{
			TemporaryNotificationDisplayer.Request request =
				new TemporaryNotificationDisplayer.Request(_loadingErrorMessage, _loadingErrorNotificationDisplayTime);
			_notificationDisplayer.Show(request);
		}

		private void updateImage(Texture texture)
		{
			_previewImage.texture = texture;
			_previewImage.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, _previewImage.texture.width);
			_previewImage.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, _previewImage.texture.height);
			float ratio = (float)_previewImage.texture.width / (float)_previewImage.texture.height;
			_imageAspectRatioFitter.aspectRatio = ratio;
			_imageObject.SetActive(true);
		}

		private void clearImage()
		{
			_previewImage.texture = null;
			_imageObject.SetActive(false);
		}
	}
}