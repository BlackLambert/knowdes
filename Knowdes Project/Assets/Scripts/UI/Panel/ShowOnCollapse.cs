using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knowdes
{
    public class ShowOnCollapse : MonoBehaviour
    {
        [SerializeField]
        private bool _show = true;

        [SerializeField]
        private Panel _panel = null;

        [SerializeField]
        private GameObject _target = null;


        protected virtual void Reset()
		{
            _panel = GetComponent<Panel>();
		}

        protected virtual void Start()
		{
            _panel.OnCollapsedChanged += checkDisplay;
            checkDisplay(null);
		}

        protected virtual void OnDestroy()
        {
            _panel.OnCollapsedChanged -= checkDisplay;
        }

        private void checkDisplay(Panel _)
		{
            _target.SetActive(_show && _panel.Collapsed || !_show && !_panel.Collapsed);
		}
	}
}