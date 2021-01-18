using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Knowdes.Prototype
{
    public class TagsEditor : MetaContentEditor<TagsData>
    {
        private const string _defaultTagName = "Neues Schlagwort";

        [SerializeField]
        private TagEditor _editorPrefab;
        [SerializeField]
        private RectTransform _hook;
        [SerializeField]
        private Button _addButton;
        [SerializeField]
        private TMP_InputField _tagNameInput;

		private Dictionary<Tag, TagEditor> _editors = new Dictionary<Tag, TagEditor>();


        protected virtual void Start()
		{
            _addButton.onClick.AddListener(addNewTag);
            _tagNameInput.text = _defaultTagName;
        }

        protected override void OnDestroy()
		{
            base.OnDestroy();
            _addButton.onClick.RemoveListener(addNewTag);
        }

        protected override void onDataAdded(TagsData data)
        {
            initEditors();
        }

        protected override void onDataRemoved(TagsData data)
        {
            cleanEditors();
        }

        private void addNewTag()
        {
            if (string.IsNullOrEmpty(_tagNameInput.text))
                return;
            Data.AddTag(new Tag(Guid.NewGuid(), _tagNameInput.text));
            _tagNameInput.text = string.Empty;
        }

        private void initEditors()
		{
            if (Data == null)
                return;
            Data.OnTagsAdded += addEditor;
            Data.OnTagsRemoved += removeEditor;
            foreach (Tag tag in Data.TagsCopy)
                addEditor(tag);
		}

        private void cleanEditors()
		{
            if (Data == null)
                return;
            Data.OnTagsAdded -= addEditor;
            Data.OnTagsRemoved -= removeEditor;
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