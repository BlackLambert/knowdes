using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knowdes.Prototype
{
    public class SetToEditPanelOnSelect : MonoBehaviour
    {
        [SerializeField]
        private Entry _entry = null;
        [SerializeField]
        private UISelectable _selectable = null;

        private EditPanel _editPanel;

        protected virtual void Start()
		{
            _editPanel = FindObjectOfType<EditPanel>();
            _selectable.OnSelected += setEntry;
        }

		protected virtual void OnDestroy()
		{
            _selectable.OnSelected -= setEntry;
        }

        private void setEntry()
        {
            _editPanel.SetEntry(_entry);
        }
    }
}