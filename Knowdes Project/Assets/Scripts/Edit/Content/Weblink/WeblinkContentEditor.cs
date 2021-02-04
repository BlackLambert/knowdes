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
		private UriConverter _urlConverter;
		private string _error = string.Empty;
		private string _urlString => $"{_schemeDropdown.options[_schemeDropdown.value].text}://{_hostInput.text}".Trim();
		private bool _valid => string.IsNullOrEmpty(_error);


		protected virtual void Awake()
		{
			_urlConverter = new WeblinkConverter();
		}

		protected override void onDataAdded(WeblinkContentData data)
		{
			if (data == null)
				return;
			initSchemaInput();
			initHostInput();
			validateInputLink();
			updateErrorDisplay();
			addInputListeners();
		}

		protected override void onDataRemoved(WeblinkContentData data)
		{
			if (data == null)
				return;
			removeInputListeners();
		}

		private void initSchemaInput()
		{
			updateSchemaInputBasedOn(Data.UrlString);
		}

		private void initHostInput()
		{
			_hostInput.text = removeSchemaFrom(Data.UrlString);
		}

		private void validateInputLink()
		{
			UriConverter.Result result = _urlConverter.Convert(_urlString);
			_error = result.Error;
		}

		private string removeSchemaFrom(string url)
		{
			string[] urlParts = splitSchemaFromUrl(url);
			return urlParts[1];
		}

		private void updateSchemaInputBasedOn(string url)
		{
			string[] urlParts = splitSchemaFromUrl(url);
			updateSchemaInput(urlParts[0]);
		}

		private void onSchemaChanged(int arg0)
		{
			tryUpdateDataURL();
		}

		private void onHostInputSubmit(string arg0)
		{
			trimHostInputText();
			updateSchemaInputBasedOn(_hostInput.text);
			removeSchemaFromHostInput();
			validateInputLink();
			updateErrorDisplay();
			tryUpdateDataURL();
		}

		private void updateErrorDisplay()
		{
			if (_valid)
				hideError();
			else
				showError(_error);
		}

		private void addInputListeners()
		{
			_schemeDropdown.onValueChanged.AddListener(onSchemaChanged);
			_hostInput.onSubmit.AddListener(onHostInputSubmit);
		}

		private void removeInputListeners()
		{
			_schemeDropdown.onValueChanged.RemoveListener(onSchemaChanged);
			_hostInput.onSubmit.RemoveListener(onHostInputSubmit);
		}

		private void removeSchemaFromHostInput()
		{
			_hostInput.text = removeSchemaFrom(_hostInput.text);
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

		private void updateSchemaInput(string schemaString)
		{
			switch (schemaString)
			{
				case "https":
					_schemeDropdown.value = 0;
					break;
				case "http":
					_schemeDropdown.value = 1;
					break;
				case "":
					break;
				default:
					throw new NotImplementedException();

			}
		}

		private string[] splitSchemaFromUrl(string url)
		{
			if (url == string.Empty)
				return new string[] { string.Empty, url };
			List<string> urlParts = url.Split(new string[] { "://" }, StringSplitOptions.None).ToList();
			if (urlParts.Count == 1)
				return new string[] { string.Empty, url };
			if (urlParts.Count > 2)
				throw new ArgumentException();
			return urlParts.ToArray();
		}

		private void tryUpdateDataURL()
		{
			if (_hostInput.text == string.Empty)
			{
				Data.Url = null;
				OnChanged?.Invoke();
				return;
			}

			if (!_valid)
				return;

			UriConverter.Result result = _urlConverter.Convert(_urlString);

			if (result.Successful)
			{
				Data.Url = result.Uri;
				OnChanged?.Invoke();
			}
		}
		private void trimHostInputText()
		{
			_hostInput.text = _hostInput.text.Trim();
		}
	}
}