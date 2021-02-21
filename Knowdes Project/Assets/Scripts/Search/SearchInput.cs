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
        private SearchFilterExtractor _searchFilterExtractor;

        protected virtual void Start()
        {
            _searchFilterExtractor = new SearchFilterExtractor();
            _inputField.onSubmit.AddListener(onSubmit);
        }

        protected virtual void OnDestroy()
		{
            _inputField.onSubmit.RemoveListener(onSubmit);
        }

		private void onSubmit(string arg0)
		{
            OnSubmit?.Invoke();
        }

		public SearchFilter ExtractFilter()
		{
            return _searchFilterExtractor.Extract(_inputField.text);
		}
    }
}