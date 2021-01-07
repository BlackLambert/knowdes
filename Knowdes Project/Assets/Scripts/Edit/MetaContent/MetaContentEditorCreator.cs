using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Knowdes.Prototype
{
    public class MetaContentEditorCreator : MonoBehaviour
    {
        [SerializeField]
        private RectTransform _editorsHook;
        private EditPanel _editPanel;
        private MetaContentEditorFactory _factory;
        private Dictionary<MetaDataType, MetaContentEditorShell> _typeToShells = new Dictionary<MetaDataType, MetaContentEditorShell>();

        private Entry _current;

        protected virtual void Start()
        {
            _factory = FindObjectOfType<MetaContentEditorFactory>();
            _editPanel = FindObjectOfType<EditPanel>();
            _current = _editPanel.Entry;
            initEntry();
            _editPanel.OnEntryChanged += onEntryChanged;
        }

        protected virtual void OnDestroy()
        {
            if (_editPanel != null)
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
            init();
            _current.Volume.Data.OnMetaDataAdded += addMetaEditor;
            _current.Volume.Data.OnMetaDataRemoved += removeMetaContent;
        }

        private void cleanEntry()
        {
            if (_current == null)
                return;
            clean();
            _current.Volume.Data.OnMetaDataAdded -= addMetaEditor;
            _current.Volume.Data.OnMetaDataRemoved -= removeMetaContent;
        }

        private void init()
        {
            foreach (KeyValuePair<MetaDataType, MetaData> data in _current.Data.MetaDatasCopy)
                addMetaEditor(data.Value);
            reorder();
        }

        private void clean()
		{
            foreach (KeyValuePair<MetaDataType, MetaContentEditorShell> data in new Dictionary<MetaDataType, MetaContentEditorShell>(_typeToShells))
                removeMetaContent(data.Value.Data);
		}



        private void addMetaEditor(MetaData data)
        {
            MetaContentEditorShell editor = _factory.Create(data);
            editor.Base.SetParent(_editorsHook);
            editor.Base.localScale = Vector3.one;
            _typeToShells.Add(data.Type, editor);
            reorder();
        }

        private void removeMetaContent(MetaData data)
        {
            if (!_typeToShells.ContainsKey(data.Type))
                throw new InvalidOperationException();
            MetaContentEditorShell editor = _typeToShells[data.Type];
            _typeToShells.Remove(data.Type);
            Destroy(editor.Base.gameObject);
            reorder();
        }

        private void reorder()
        {
            IEnumerable<MetaContentEditorShell> editorShells = _typeToShells.Values;
            editorShells = editorShells.OrderBy(e => e.Data.Priority);
            foreach (MetaContentEditorShell shell in editorShells)
                shell.Base.SetAsFirstSibling();
        }
    }
}