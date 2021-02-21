using UnityEngine;

namespace Knowdes
{
	public class PdfContentEditor : ContentEditor<PdfContentData>
	{
		[SerializeField]
		private UriInput _uriInput;

		private PdfContentData _data;

		protected virtual void Awake()
		{
			_uriInput.Init(new PdfPathConverter());
		}

		protected override void onDataAdded(PdfContentData data)
		{
			_data = data;
			_uriInput.SetUriBy(_data.Path);
			_uriInput.OnUriChanged += onUriChanged;
		}

		protected override void onDataRemoved(PdfContentData data)
		{
			_uriInput.SetUri(null);
			_uriInput.OnUriChanged -= onUriChanged;
			_data = null;
		}

		private void onUriChanged()
		{
			_data.Path = _uriInput.Uri == null ? string.Empty : _uriInput.Uri.LocalPath;
		}
	}
}