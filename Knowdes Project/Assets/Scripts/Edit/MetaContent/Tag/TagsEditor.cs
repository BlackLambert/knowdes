using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Knowdes.Prototype
{
    public class TagsEditor : MetaContentEditor
    {
        private const string _defaultTagName = "Neues Schlagwort";

        [SerializeField]
        private TagEditor _editorPrefab;
        [SerializeField]
        private RectTransform _hook;
        [SerializeField]
        private Button _addButton;

        private TagsData _data;
        public TagsData Data
        {
            get => _data;
			set
			{
                cleanEditors();
                _data = value;
                initEditors();

            }
        }

		public override MetaData MetaData => _data;

		private Dictionary<Tag, TagEditor> _editors = new Dictionary<Tag, TagEditor>();


        protected virtual void Start()
		{
            _addButton.onClick.AddListener(addNewTag);
        }

        protected virtual void OnDestroy()
		{
            _addButton.onClick.RemoveListener(addNewTag);
        }

		private void addNewTag()
		{
            _data.AddTag(new Tag(Guid.NewGuid(), _defaultTagName)); 
		}

		private void initEditors()
		{
            if (_data == null)
                return;
            _data.OnTagsAdded += addEditor;
            _data.OnTagsRemoved += removeEditor;
            foreach (Tag tag in _data.TagsCopy)
                addEditor(tag);
		}

        private void cleanEditors()
		{
            if (_data == null)
                return;
            _data.OnTagsAdded -= addEditor;
            _data.OnTagsRemoved -= removeEditor;
            foreach (KeyValuePair<Tag, TagEditor> pair in new Dictionary<Tag, TagEditor>(_editors))
                removeEditor(pair.Key);
        }

        private void addEditor(Tag tag)
		{
            TagEditor editor = Instantiate(_editorPrefab);
            editor.Tag = tag;
            editor.Data = Data;
            editor.Base.SetParent(_hook);
            editor.Base.localScale = Vector3.one;
            _editors.Add(tag, editor);
		}

        private void removeEditor(Tag tag)
		{
            TagEditor editor = _editors[tag];
            _editors.Remove(editor.Tag);
            Destroy(editor.Base.gameObject);
		}
    }
}