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
		private TMP_InputField _hostInput = null;
		public TMP_InputField HostInput => _hostInput;
		[SerializeField]
		private TMP_Dropdown _schemeDropdown = null;
		[SerializeField]
		private GameObject _errorTextObject = null;
		[SerializeField]
		private TextMeshProUGUI _errorText = null;

		public event Action OnChanged;

		private URLConverter _urlConverter;

		protected virtual void Start()
		{
			_urlConverter = new URLConverter();
		}

		private void updateText()
		{
			_hostInput.text = Data != null && !Data.Empty ? Data.UrlString : string.Empty;
		}

		private void updateContentLink()
		{
			if(_hostInput.text == string.Empty)
			{
				Data.Url = null;
				OnChanged?.Invoke();
				return;
			}

			string source = $"{_schemeDropdown.options[_schemeDropdown.value].text}://{_hostInput.text}";
			URLConverter.Result result = _urlConverter.Convert(source);

			if (result.Successful)
			{
				Data.Url = result.Url;
				OnChanged?.Invoke();
			}
			else
				showError(result.Error);
		}

		private void removeSchema()
		{
			if (_hostInput.text == string.Empty)
				return;
			List<string> urlParts = _hostInput.text.Split(new string[]{ "://"}, StringSplitOptions.None).ToList();
			_hostInput.text = urlParts[urlParts.Count - 1];
		}

		

		protected override void onDataAdded(WeblinkContentData data)
		{
			if (data == null)
				return;
			updateText();
			_hostInput.onValueChanged.AddListener(onValueChanged);
			_schemeDropdown.onValueChanged.AddListener(onSchemaChanged);
		}

		protected override void onDataRemoved(WeblinkContentData data)
		{
			if (data == null)
				return;
			_hostInput.onValueChanged.RemoveListener(onValueChanged);
			_schemeDropdown.onValueChanged.RemoveListener(onSchemaChanged);
		}


		private void onSchemaChanged(int arg0)
		{
			updateValues();
		}

		private void onValueChanged(string arg0)
		{
			updateValues();
		}

		private void updateValues()
		{
			hideError();
			removeSchema();
			updateContentLink();
		}

		private void showError(string error)
		{
			_errorText.text = error;
			_errorTextObject.SetActive(true);
		}

		private void hideError()
		{
			_errorTextObject.SetActive(false);
		}
	}
}