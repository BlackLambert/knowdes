
using UnityEngine;

namespace Knowdes
{
	public class PreviewImageContent : MetaContent<PreviewImageData>
	{
		private const string _loadingErrorMessage = "Das gewünschte Vorschaubild konnte nicht geladen werden.";
		private const float _loadingErrorNotificationDisplayTime = 3f;

		[SerializeField]
		private RemoteImageView _previewImageView;

		protected virtual void Start()
		{
			_previewImageView.SetErrorMessage(_loadingErrorMessage, _loadingErrorNotificationDisplayTime);
		}


		protected override void onDataAdded(PreviewImageData data)
		{
			loadImage();
			data.OnPathChanged += onPathChanged;
		}

		protected override void onDataRemoved(PreviewImageData data)
		{
			data.OnPathChanged -= onPathChanged;
		}

		private void onPathChanged()
		{
			loadImage();
		}

		private void loadImage()
		{
			_previewImageView.SetPath(Data.Uri == null ? string.Empty : Data.Uri.AbsoluteUri);
		}
	}
}