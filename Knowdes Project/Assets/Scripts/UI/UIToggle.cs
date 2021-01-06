using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Knowdes
{
    public class UIToggle : MonoBehaviour
    {
        [SerializeField]
        private Button _showButton = null;
        [SerializeField]
        private Button _hideButton = null;
        [SerializeField]
        private GameObject _target = null;
        [SerializeField]
        private bool _showOnStart = false;

        private bool _shown = false;

        protected virtual void Start()
		{
            _showButton.onClick.AddListener(show);
            _hideButton.onClick.AddListener(hide);
            _shown = _showOnStart;
            updateUI();
        }

		protected virtual void OnDestroy()
		{
            _showButton.onClick.RemoveListener(show);
            _hideButton.onClick.RemoveListener(hide);
        }

        private void show()
        {
            _shown = true;
            updateUI();
        }

        private void hide()
		{
            _shown = false;
            updateUI();
        }

        private void updateUI()
		{
            _hideButton.gameObject.SetActive(_shown);
            _showButton.gameObject.SetActive(!_shown);
            _target.SetActive(_shown);
		}
    }
}