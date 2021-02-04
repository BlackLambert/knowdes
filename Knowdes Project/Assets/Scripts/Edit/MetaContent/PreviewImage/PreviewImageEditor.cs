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
		private TMP_InputField _uriInput = null;
		[SerializeField]
		private GameObject _errorTextObject = null;
		[SerializeField]
		private TextMeshProUGUI _errorText = null;
		private UriConverter _urlConverter;

		private string _error = string.Empty;
		private bool _valid => string.IsNullOrEmpty(_error);

		protected virtual void Awake()
		{
			_urlConverter = new ImageFilePathConverter();
		}

		protected override void onDataAdded(PreviewImageData data)
		{
			if (data == null)
				return;
			updateInputText();
			validateLinkString();
			updateErrorDisplay();
			addInputListeners();
		}

		protected override void onDataRemoved(PreviewImageData data)
		{
			if (data == null)
				return;
			removeInputListeners();
		}

		private void onUriStringSubmit(string arg0)
		{
			trimUriInputText();
			validateLinkString();
			updateErrorDisplay();
			tryUpdateURLData();
		}

		private void updateErrorDisplay()
		{
			if (_valid)
				hideError();
			else
				showError(_error);
		}

		private void updateInputText()
		{
			_uriInput.text = Data == null || Data.IsEmpty ? string.Empty : Data.Uri.AbsoluteUri;
		}

		private void validateLinkString()
		{
			UriConverter.Result result = _urlConverter.Convert(_uriInput.text);
			_error = result.Error;
		}


		private void tryUpdateURLData()
		{
			if (_uriInput.text == string.Empty)
			{
				Data.Uri = null;
				return;
			}

			if (!_valid)
				return;

			UriConverter.Result result = _urlConverter.Convert(_uriInput.text);

			if (result.Successful)
				Data.Uri = result.Uri;
			else
				throw new InvalidOperationException();
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

		private void addInputListeners()
		{
			_uriInput.onSubmit.AddListener(onUriStringSubmit);
		}

		private void removeInputListeners()
		{
			_uriInput.onSubmit.RemoveListener(onUriStringSubmit);
		}

		private void trimUriInputText()
		{
			_uriInput.text = _uriInput.text.Trim();
		}
	}
}