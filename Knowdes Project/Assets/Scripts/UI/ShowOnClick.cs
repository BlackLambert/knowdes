using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Knowdes
{ 
    public class ShowOnClick : MonoBehaviour
    {
        [SerializeField]
        private Button _button = null;
        [SerializeField]
        private GameObject _target = null;
        [SerializeField]
        private bool _show = false;

        protected virtual void Start()
		{
            _button.onClick.AddListener(onClick);
		}

		protected virtual void OnDestroy()
		{
            _button.onClick.RemoveListener(onClick);
        }

        protected virtual void Reset()
		{
            _button = GetComponent<Button>();
        }


        private void onClick()
        {
            _target.SetActive(_show);
        }
    }
}