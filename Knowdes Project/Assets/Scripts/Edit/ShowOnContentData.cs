using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knowdes.Prototype
{
    public class ShowOnContentData : MonoBehaviour
    {
        [SerializeField]
        private GameObject _target;
        [SerializeField]
        private bool _show = true;

        private EditPanel _editPanel;

        private Entry _current;

        protected virtual void Start()
		{
            _editPanel = FindObjectOfType<EditPanel>();
            _editPanel.OnEntryChanged += onEntryChanged;
            initEntry();
        }

        protected virtual void OnDestroy()
		{
            if(_editPanel != null)
                _editPanel.OnEntryChanged -= onEntryChanged;
        }

		private void onEntryChanged()
		{
            cleanEntry();
            initEntry();
		}

        private void cleanEntry()
		{
            if (_current == null)
                return;
            _current.Data.OnContentChanged -= onContentChanged;
		}

		private void initEntry()
		{
            _current = _editPanel.Entry;
            checkShow();
            if (_current == null)
                return;
            _current.Data.OnContentChanged += onContentChanged;
        }

        private void onContentChanged()
        {
            checkShow();
        }

        private void checkShow()
		{
            if (_current == null)
                _target.SetActive(!_show);
            else
                _target.SetActive(_show && _current.Data.Content != null || !_show && _current.Data.Content == null);
        }
    }
}