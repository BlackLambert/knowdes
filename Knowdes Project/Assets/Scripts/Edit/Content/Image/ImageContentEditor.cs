using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knowdes
{
	public class ImageContentEditor : ContentEditor<ImageContentData>
	{
		[SerializeField]
		private UriInput _uriInput;

		private ImageContentData _data;

		protected virtual void Awake()
		{
			_uriInput.Init(new ImageFilePathConverter());
		}

		protected override void onDataAdded(ImageContentData data)
		{
			_data = data;
			_uriInput.SetUriBy(_data.Path);
			_uriInput.OnUriChanged += onUriChanged;
		}

		protected override void onDataRemoved(ImageContentData data)
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