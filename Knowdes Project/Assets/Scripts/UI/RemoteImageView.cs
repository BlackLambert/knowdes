using System;
using UnityEngine;
using UnityEngine.UI;

namespace Knowdes
{
    public class RemoteImageView : MonoBehaviour
	{
		private const string _defaultLoadingErrorMessage = "Das gewünschte Bild konnte nicht geladen werden.";
		private const float _defaultErrorDisplayTime = 3f;

		[SerializeField]
		private RawImage _image;
		[SerializeField]
		private AspectRatioFitter _imageAspectRatioFitter;
		[SerializeField]
		private GameObject _imageObject;
		private ImageLoader.LoadRequest _loadRequest;
		private string _path;
		private ImageLoader _imageLoader;
		private TemporaryNotificationDisplayer _notificationDisplayer;
		private string _loadingErrorMessage;
		private float _errorDisplayTime;


		protected virtual void Awake()
		{
			_loadingErrorMessage = _defaultLoadingErrorMessage;
			_errorDisplayTime = _defaultErrorDisplayTime; 
			_imageLoader = ImageLoader.New();
			_notificationDisplayer = FindObjectOfType<TemporaryNotificationDisplayer>();
			updateView();
		}

		protected virtual void OnDestroy()
		{
			if(_imageLoader != null)
				_imageLoader.Destroy();
		}


		public void SetPath(string path)
		{
			_path = path;
			updateView();
		}

		public void SetErrorMessage(string message)
		{
			SetErrorMessage(message, _defaultErrorDisplayTime);
		}

		public void SetErrorMessage(string message, float displayTime)
		{
			if (displayTime <= 0)
				throw new ArgumentOutOfRangeException();
			_loadingErrorMessage = message;
			_errorDisplayTime = displayTime;
		}

		private void updateView()
		{
			clearImage();
			if (_loadRequest != null)
				cancelLoadRequest();
			if(!string.IsNullOrEmpty(_path))
				loadImage();
		}

		private void clearImage()
		{
			_imageObject.SetActive(false);
			_image.texture = null;
		}

		private void loadImage()
		{
			Uri uri = getURI();
			_loadRequest = new ImageLoader.LoadRequest(uri);
			_imageLoader.LoadImage(_loadRequest, onImageLoaded);
		}

		private void onImageLoaded(ImageLoader.Result result)
		{
			_loadRequest = null;
			if (result.RequestWasSuccessful)
				updateImage(result.Texture);
			else
				showErrorNotification();
		}

		private void updateImage(Texture result)
		{
			_image.texture = result;
			_imageObject.SetActive(true);
			_imageAspectRatioFitter.aspectRatio = (float)_image.texture.width / (float)_image.texture.height;
		}

		private void showErrorNotification()
		{
			TemporaryNotificationDisplayer.Request request = new TemporaryNotificationDisplayer.Request(
				_loadingErrorMessage, _errorDisplayTime);
			_notificationDisplayer.RequestDisplay(request);
		}

		private Uri getURI()
		{
			Uri result;
			if (!Uri.TryCreate(_path, UriKind.Absolute, out result))
				throw new InvalidOperationException();
			return result;
		}

		private void cancelLoadRequest()
		{
			_imageLoader.CancelLoadingImage(_loadRequest);
		}
	}
}