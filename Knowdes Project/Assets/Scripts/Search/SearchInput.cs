using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

namespace Knowdes
{
    public class SearchInput : MonoBehaviour
    {
        [SerializeField]
        private TMP_InputField _inputField;
        public event Action OnSubmit;
        public event Action OnChange;
        private SearchFilterExtractor _searchFilterExtractor;

        public bool IsEmpty => string.IsNullOrEmpty(_inputField.text);

		protected virtual void Start()
        {
            _searchFilterExtractor = new SearchFilterExtractor();
            _inputField.onSubmit.AddListener(onSubmit);
            _inputField.onValueChanged.AddListener(onChange);
        }

		protected virtual void OnDestroy()
		{
            _inputField.onSubmit.RemoveListener(onSubmit);
            _inputField.onValueChanged.RemoveListener(onChange);
        }

        private void onChange(string arg0)
        {
            OnChange?.Invoke();

        }

        private void onSubmit(string arg0)
		{
            OnSubmit?.Invoke();
        }

		public SearchFilter ExtractFilter()
		{
            return _searchFilterExtractor.Extract(_inputField.text);
		}

        public void Clear()
        {
            _inputField.text = string.Empty;
            OnSubmit?.Invoke();
        }
    }
}