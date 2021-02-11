
using UnityEngine;

namespace Knowdes
{
	public class UnknownFileEditor : ContentEditor<UnknownFileContentData>
	{
		[SerializeField]
		private UriInput _uriInput;

		private UnknownFileContentData _data;

		protected virtual void Awake()
		{
			_uriInput.Init(new UnknownFilePathConverter());
		}

		protected override void onDataAdded(UnknownFileContentData data)
		{
			_data = data;
			_uriInput.SetUriBy(_data.Path);
			_uriInput.OnUriChanged += onUriChanged;
		}

		protected override void onDataRemoved(UnknownFileContentData data)
		{
			_uriInput.SetUri(null);
			_uriInput.OnUriChanged -= onUriChanged;
			_data = null;
		}

		private void onUriChanged()
		{
			_data.Path = _uriInput.Uri == null ? string.Empty : _uriInput.Uri.AbsoluteUri;
		}
	}
}