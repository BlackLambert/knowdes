
using System;
using TMPro;
using UnityEngine;

namespace Knowdes
{
    public class AuthorEditor : MonoBehaviour
    {
		[SerializeField]
		private TMP_InputField _input;
		[SerializeField]
		private RectTransform _base;
		public RectTransform Base => _base;

		private Author _author;
		public Author Author
		{
			get => _author;
			set
			{
				_author = value;
				initView();
			}
		}

		public event Action<Author> OnDeleteTriggered;

		public void DeleteAuthor()
		{
			OnDeleteTriggered?.Invoke(Author);
		}

		protected virtual void Start()
		{
			_input.onValueChanged.AddListener(updateAuthorName);
		}

		protected virtual void OnDestroy()
		{
			_input.onValueChanged.RemoveListener(updateAuthorName);
		}

		private void updateAuthorName(string arg0)
		{
			_author.Name = _input.text;
		}

		private void initView()
		{
			_input.text = _author.Name;
		}
	}
}