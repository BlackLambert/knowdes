using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knowdes
{
    public class ExpandOnStart : MonoBehaviour
    {
        [SerializeField]
        private Panel _panel = null;
        [SerializeField]
        private bool _expand = true;

        protected virtual void Start()
		{
            if (!_panel.Collapsed && !_expand)
                _panel.Collapse();
            else if (_panel.Collapsed && _expand)
                _panel.Expand();
		}

        protected virtual void Reset()
		{
            _panel = GetComponent<Panel>();

        }
    }
}