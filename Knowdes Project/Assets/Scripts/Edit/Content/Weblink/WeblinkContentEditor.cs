using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace Knowdes
{
	public class WeblinkContentEditor : ContentEditor<WeblinkContentData>
	{
		[SerializeField]
		private UriInput _uriInput;
		private WeblinkContentData _data;

		public event Action OnChanged;

		protected virtual void Awake()
		{
			_uriInput.Init(new WeblinkConverter());
		}

		protected override void onDataAdded(WeblinkContentData data)
		{
			_data = data;
			initInput();
			_uriInput.OnUriChanged += onUriChanged;
		}

		protected override void onDataRemoved(WeblinkContentData data)
		{
			_data.OnUrlChanged -= onUriChanged;
			_data = null;
		}

		private void onUriChanged()
		{
			_data.Url = _uriInput.Uri;
			OnChanged?.Invoke();
		}

		private void initInput()
		{
			_uriInput.SetUri(_data.Url);
		}
	}
}