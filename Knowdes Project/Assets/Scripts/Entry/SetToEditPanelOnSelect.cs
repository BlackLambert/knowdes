using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knowdes
{
    public abstract class SetToEditPanelOnSelect : MonoBehaviour
    {

        protected abstract Entry Entry{get;}
        [SerializeField]
        private UISelectable _selectable = null;

        private EditPanel _editPanel;

        protected virtual void Start()
		{
            _editPanel = FindObjectOfType<EditPanel>(true);
            _selectable.OnSelected += setEntry;
        }

		protected virtual void OnDestroy()
		{
            _selectable.OnSelected -= setEntry;
        }

        private void setEntry()
        {
            _editPanel.InitWith(Entry);
        }
    }
}