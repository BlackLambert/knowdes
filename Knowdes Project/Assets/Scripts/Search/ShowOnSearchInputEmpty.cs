using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knowdes
{
    public class ShowOnSearchInputEmpty : MonoBehaviour
    {
        [SerializeField]
        private GameObject _objectToContoll;
        [SerializeField]
        private SearchInput _searchInput;
        [SerializeField]
        private bool _show = true;

        protected virtual void Start()
		{
            updateView();
            _searchInput.OnChange += updateView;

        }

        protected virtual void OnDestroy()
		{
            _searchInput.OnChange -= updateView;
        }

		private void updateView()
		{
            bool setActive = _searchInput.IsEmpty == _show;
            _objectToContoll.SetActive(setActive);
		}
	}
}