using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Knowdes
{
	public class PreviewImageEditor : MetaContentEditor<PreviewImageData>
	{
		[SerializeField]
		private UriInput _uriInput;

		protected virtual void Awake()
		{
			_uriInput.Init(new ImageFilePathConverter());
		}

		protected override void onDataAdded(PreviewImageData data)
		{
			_uriInput.SetUri(data.Uri);
			_uriInput.OnUriChanged += onUriChanged;
		}

		protected override void onDataRemoved(PreviewImageData data)
		{
			_uriInput.SetUri(null);
			_uriInput.OnUriChanged -= onUriChanged;
		}


		private void onUriChanged()
		{
			updateDataUri();
		}

		private void updateDataUri()
		{
			Data.Uri = _uriInput.Uri;
		}
	}
}