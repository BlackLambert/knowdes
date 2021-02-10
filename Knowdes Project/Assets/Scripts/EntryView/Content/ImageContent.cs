
using TMPro;
using UnityEngine;

namespace Knowdes
{
	public class ImageContent : Content<ImageContentData>
	{
		private const string _loadingErrorMessage = "Das gewünschte Bild konnte nicht geladen werden.";
		private const float _loadingErrorNotificationDisplayTime = 3f;

		[SerializeField]
		private RemoteImageView _image;
		[SerializeField]
		private TextMeshProUGUI _pathText;

		protected virtual void Start()
		{
			_image.SetErrorMessage(_loadingErrorMessage, _loadingErrorNotificationDisplayTime);
			updateView();
		}

		protected override void onDataAdded(ImageContentData data)
		{
			data.OnPathChanged += onPathChanged;
		}

		protected override void onDataRemoved(ImageContentData data)
		{
			data.OnPathChanged -= onPathChanged;
		}

		private void onPathChanged()
		{
			updateView();
		}

		private void updateView()
		{
			updateImage();
			updatePathText();
		}

		private void updatePathText()
		{
			_pathText.text = Data.Path;
		}

		private void updateImage()
		{
			_image.SetPath(Data.Path);
		}
	}
}