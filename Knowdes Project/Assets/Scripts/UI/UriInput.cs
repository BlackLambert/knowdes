using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Knowdes
{
    public class UriInput : MonoBehaviour
    {
		[SerializeField]
		private TMP_InputField _uriInput = null;
		[SerializeField]
		private GameObject _errorTextObject = null;
		[SerializeField]
		private TextMeshProUGUI _errorText = null;
		private UriConverter _uriConverter = null;
		private string _error = string.Empty;
		private bool _valid => string.IsNullOrEmpty(_error);

		private Uri _uri;
		public event Action OnUriChanged;
		public Uri Uri
		{
			get => _uri;
			private set
			{
				_uri = value;
				OnUriChanged?.Invoke();
			}
		}

		public bool Empty => string.IsNullOrEmpty(_uriInput.text);

		protected virtual void Start()
		{
			addInputListeners();
		}

		protected virtual void OnDestroy()
		{
			removeInputListeners();
		}

		public void Init(UriConverter converter)
		{
			_uriConverter = converter;
		}

		private void addInputListeners()
		{
			_uriInput.onSubmit.AddListener(onUriStringSubmit);
		}

		private void removeInputListeners()
		{
			_uriInput.onSubmit.RemoveListener(onUriStringSubmit);
		}

		private void onUriStringSubmit(string arg0)
		{
			trimUriInputText();
			validateLinkString();
			updateErrorDisplay();
			tryUpdateURLData();
		}

		public void SetUri(Uri uri)
		{
			_uri = uri;
			updateInputText();
			validateLinkString();
			updateErrorDisplay();
		}

		public void Clear()
		{
			_uriInput.text = string.Empty;
			Uri = null;
		}

		public void SetUriBy(string path)
		{
			if (string.IsNullOrEmpty(path))
				SetUri(null);
			else
				tryToSetUriBy(path);
		}

		private void tryToSetUriBy(string path)
		{
			UriConverter.Result result = _uriConverter.Convert(path);
			if (!result.Successful)
				throw new ArgumentException();
			SetUri(result.Uri);
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
			_uriInput.text = _uri == null ? string.Empty : _uri.AbsoluteUri;
		}

		private void validateLinkString()
		{
			UriConverter.Result result = _uriConverter.Convert(_uriInput.text);
			_error = result.Error;
		}


		private void tryUpdateURLData()
		{
			if (inputIsEmpty())
			{
				Uri = null;
				return;
			}

			if (!_valid)
				return;

			UriConverter.Result result = _uriConverter.Convert(_uriInput.text);

			if (result.Successful)
				Uri = result.Uri;
			else
				throw new InvalidOperationException();
		}

		private bool inputIsEmpty()
		{
			return _uriInput.text == string.Empty;
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

		private void trimUriInputText()
		{
			_uriInput.text = _uriInput.text.Trim();
		}
	}
}