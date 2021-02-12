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

        [SerializeField]
        private TagEditor _editorPrefab;
        [SerializeField]
        private RectTransform _hook;
        [SerializeField]
        private Button _addButton;
        [SerializeField]
        private TMP_InputField _tagNameInput;

        [SerializeField]
        private RectTransform _emptyNameErrorTextPanel = null;

		private Dictionary<Tag, TagEditor> _editors = new Dictionary<Tag, TagEditor>();


        protected virtual void Start()
		{
            _addButton.onClick.AddListener(addNewTag);
            _tagNameInput.text = string.Empty;
            _tagNameInput.onSubmit.AddListener(onSubmit);
            _tagNameInput.onValueChanged.AddListener(onSelect);
            hideErrors();
        }

		private void onSubmit(string arg0)
		{
            addNewTag();
        }

		protected override void OnDestroy()
		{
            base.OnDestroy();
            _addButton.onClick.RemoveListener(addNewTag);
            _tagNameInput.onSubmit.RemoveListener(onSubmit);
            _tagNameInput.onSelect.RemoveListener(onSelect);
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
            {
                showEmptyNameError();
                return;
            }
            Data.AddTag(new Tag(Guid.NewGuid(), _tagNameInput.text));
            _tagNameInput.text = string.Empty;
            _tagNameInput.ActivateInputField();
        }

        private void initEditors()
		{
            if (Data == null)
                return;
            Data.OnTagAdded += addEditor;
            Data.OnTagRemoved += removeEditor;
            foreach (Tag tag in Data.TagsCopy)
                addEditor(tag);
		}

        private void cleanEditors()
		{
            if (Data == null)
                return;
            Data.OnTagAdded -= addEditor;
            Data.OnTagRemoved -= removeEditor;
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

        private void showEmptyNameError()
		{
            _emptyNameErrorTextPanel.gameObject.SetActive(true);
        }

        private void onSelect(string arg0)
        {
            hideErrors();
        }

        private void hideErrors()
		{
            _emptyNameErrorTextPanel.gameObject.SetActive(false);

        }
	}
}