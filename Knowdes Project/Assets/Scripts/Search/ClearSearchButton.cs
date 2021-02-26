using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Knowdes
{
    public class ClearSearchButton : MonoBehaviour
    {
        [SerializeField]
        private Button _button;
        [SerializeField]
        private SearchInput _input;

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
            _input.Clear();
        }
    }
}