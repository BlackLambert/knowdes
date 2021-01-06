using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knowdes.Prototype
{
    public class CollapseOnEntryDeleted : MonoBehaviour
    {
        [SerializeField]
        private ContextPanel _contextPanel = null;

        [SerializeField]
        private EditPanel _editPanel = null;

        protected virtual void Start()
		{
            _editPanel.OnEntryChanged += onEntryChanged;
		}

		protected virtual void OnDestroy()
		{
			_editPanel.OnEntryChanged -= onEntryChanged;
		}

		private void onEntryChanged()
		{
			if (_editPanel.Entry == null)
				_contextPanel.Hide();
		}
	}
}