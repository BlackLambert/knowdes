using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Knowdes
{
    public class AuthorsEditor : MetaContentEditor<AuthorData>
    {
        [SerializeField]
        private AuthorEditor _editorPrefab;
        [SerializeField]
        private RectTransform _hook;
        [SerializeField]
        private Button _addButton;
        [SerializeField]
        private TMP_InputField _authorNameInput;

        [SerializeField]
        private RectTransform _emptyNameErrorTextPanel = null;

        private Dictionary<Author, AuthorEditor> _editors = new Dictionary<Author, AuthorEditor>();


        protected virtual void Start()
        {
            _addButton.onClick.AddListener(addNewTag);
            _authorNameInput.text = string.Empty;
            _authorNameInput.onSubmit.AddListener(onSubmit);
            _authorNameInput.onValueChanged.AddListener(onSelect);
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
            _authorNameInput.onSubmit.RemoveListener(onSubmit);
            _authorNameInput.onSelect.RemoveListener(onSelect);
        }

        protected override void onDataAdded(AuthorData data)
        {
            initEditors();
        }

        protected override void onDataRemoved(AuthorData data)
        {
            cleanEditors();
        }

        private void addNewTag()
        {
            if (string.IsNullOrEmpty(_authorNameInput.text))
            {
                showEmptyNameError();
                return;
            }
            Data.Add(new Author(Guid.NewGuid(), _authorNameInput.text));
            _authorNameInput.text = string.Empty;
            _authorNameInput.ActivateInputField();
        }

        private void initEditors()
        {
            if (Data == null)
                return;
            Data.OnAuthorAdded += addEditor;
            Data.OnAuthorRemoved += removeEditor;
            foreach (Author author in Data.AuthorsCopy)
                addEditor(author);
        }

        private void cleanEditors()
        {
            if (Data == null)
                return;
            Data.OnAuthorAdded -= addEditor;
            Data.OnAuthorRemoved -= removeEditor;
            foreach (KeyValuePair<Author, AuthorEditor> pair in new Dictionary<Author, AuthorEditor>(_editors))
                removeEditor(pair.Key);
        }

        private void addEditor(Author author)
        {
            AuthorEditor editor = Instantiate(_editorPrefab);
            editor.Author = author;
            editor.Base.SetParent(_hook);
            editor.Base.localScale = Vector3.one;
            editor.OnDeleteTriggered += onDestructAuthor;
            _editors.Add(author, editor);
        }

		private void removeEditor(Author author)
        {
            AuthorEditor editor = _editors[author];
            _editors.Remove(editor.Author);
            editor.OnDeleteTriggered -= onDestructAuthor;
            Destroy(editor.Base.gameObject);
        }

        private void onDestructAuthor(Author author)
        {
            Data.Remove(author);
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