using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knowdes.Prototype
{
    public class ContentEditorCreator : MonoBehaviour
    {
        [SerializeField]
        private RectTransform _hook;

        private EditPanel _editPanel;
        private ContentEditorFactory _factory;

        private Entry _current;
        private ContentEditor _editor;

        protected virtual void Start()
		{
            _editPanel = FindObjectOfType<EditPanel>();
            _factory = FindObjectOfType<ContentEditorFactory>();
            _current = _editPanel.Entry;
            initEntry();
            _editPanel.OnEntryChanged += onEntryChanged;
        }

		protected virtual void OnDestroy()
		{
            if(_editPanel != null)
                _editPanel.OnEntryChanged -= onEntryChanged;
            cleanEntry();
        }

        private void onEntryChanged()
        {
            cleanEntry();
            _current = _editPanel.Entry;
            initEntry();
        }

        private void initEntry()
		{
            if (_current == null)
                return;
            createEditor();
            _current.Volume.Data.OnContentChanged += onContentChanged;
        }

		private void cleanEntry()
		{
            if (_current == null)
                return;
            cleanEditor();
            _current.Volume.Data.OnContentChanged -= onContentChanged;
        }

        private void onContentChanged()
        {
            cleanEditor();
            createEditor();
        }

        private void createEditor()
		{
            if (_current.Volume.Data.Content == null)
                return;

            _editor = _factory.Create(_current.Volume.Data.Content);
            _editor.Base.SetParent(_hook, false);
            _editor.Base.localScale = Vector3.one;
        }

        private void cleanEditor()
		{
            if (_editor == null)
                return;
            Destroy(_editor.Base.gameObject);
            _editor = null;
        }
    }
}