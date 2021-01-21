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

		private string urlString => $"{_schemeDropdown.options[_schemeDropdown.value].text}://{_hostInput.text}";

		protected virtual void Awake()
		{
			_urlConverter = new URLConverter();
		}

		private void initHost()
		{
			_hostInput.text = removeSchema(Data != null && !Data.Empty ? Data.UrlString : string.Empty);
		}

		private void updateSchema(string schemaString)
		{
			switch(schemaString)
			{
				case "https":
					_schemeDropdown.value = 0;
					break;
				case "http":
					_schemeDropdown.value = 1;
					break;
				default:
					throw new NotImplementedException();
			}
		}

		private void validateLink()
		{
			URLConverter.Result result = _urlConverter.Convert(urlString);

			if (!result.Successful)
				showError(result.Error);
		}


		private void createURL()
		{
			if (_hostInput.text == string.Empty)
			{
				Data.Url = null;
				OnChanged?.Invoke();
				return;
			}

			URLConverter.Result result = _urlConverter.Convert(urlString);

			if (result.Successful)
			{
				Data.Url = result.Url;
				OnChanged?.Invoke();
			}
		}

		private string removeSchema(string url)
		{
			if (url == string.Empty)
				return url;
			List<string> urlParts = url.Split(new string[]{"://"}, StringSplitOptions.None).ToList();
			if (urlParts.Count == 1)
				return url;
			if (urlParts.Count > 2)
				throw new ArgumentException();
			updateSchema(urlParts[0]);
			return urlParts[urlParts.Count - 1];
		}

		

		protected override void onDataAdded(WeblinkContentData data)
		{
			if (data == null)
				return;
			initHost();
			hideError();
			validateLink();
			_hostInput.onValueChanged.AddListener(onValueChanged);
			_schemeDropdown.onValueChanged.AddListener(onSchemaChanged);
			_hostInput.onSubmit.AddListener(onSubmit);
		}

		protected override void onDataRemoved(WeblinkContentData data)
		{
			if (data == null)
				return;
			_hostInput.onValueChanged.RemoveListener(onValueChanged);
			_schemeDropdown.onValueChanged.RemoveListener(onSchemaChanged);
			_hostInput.onSubmit.RemoveListener(onSubmit);
		}


		private void onSchemaChanged(int arg0)
		{
			updateValues();
			createURL();
		}

		private void onValueChanged(string arg0)
		{
			updateValues();
		}

		private void onSubmit(string arg0)
		{
			createURL();
		}

		private void updateValues()
		{
			hideError();
			_hostInput.text = removeSchema(_hostInput.text);
			validateLink();
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