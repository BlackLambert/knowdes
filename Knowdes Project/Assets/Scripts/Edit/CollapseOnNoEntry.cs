using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knowdes.Prototype
{
    public class CollapseOnNoEntry : MonoBehaviour
    {
        [SerializeField]
        private Panel _panel = null;

        private EntriesList _entries;

		protected virtual void Start()
		{
			_entries = FindObjectOfType<EntriesList>();
			_entries.OnCountChanged += onCountChanged;
		}

		protected virtual void OnDestroy()
		{
			_entries.OnCountChanged -= onCountChanged;
		}

		private void onCountChanged()
		{
			if (!_panel.Collapsed && _entries.Count == 0)
				_panel.Collapse();
		}
	}
}