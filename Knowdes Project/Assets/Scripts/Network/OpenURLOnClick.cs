using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Knowdes
{
    public class OpenURLOnClick : MonoBehaviour
    {
        [SerializeField]
        private Button _button = null;
        [SerializeField]
        private TextMeshProUGUI _text = null;

        protected virtual void Start()
		{
            _button.onClick.AddListener(onClick);

        }

		protected virtual void OnDestroy()
		{
            _button.onClick.RemoveListener(onClick);
        }

        private void onClick()
        {
            Application.OpenURL(_text.text);
        }
    }
}