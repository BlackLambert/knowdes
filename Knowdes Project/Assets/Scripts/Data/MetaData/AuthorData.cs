using System;
using System.Collections.Generic;

namespace Knowdes
{
	public class AuthorData : MetaData
	{
		private List<Author> _authors = new List<Author>();
		public List<Author> AuthorsCopy => new List<Author>(_authors);


		public override MetaDataType Type => MetaDataType.Author;

		public override int Priority => 100;

		public event Action<Author> OnAdded;
		public event Action<Author> OnRemoved;

		public AuthorData(Guid iD, List<Author> authors) : base(iD, true)
		{
			_authors = authors;
			init();
		}
		private void init()
		{
			foreach (Author author in _authors)
			{
				author.OnNameChanged += invokeOnChanged;
			}
		}

		public void Add(Author author)
		{
			if (_authors.Contains(author))
				throw new InvalidOperationException();
			_authors.Add(author);
			author.OnNameChanged += invokeOnChanged;
			invokeOnChanged();
		}

		public void Remove(Author author)
		{
			if (!_authors.Contains(author))
				throw new InvalidOperationException();
			_authors.Remove(author);
			author.OnNameChanged -= invokeOnChanged;
			invokeOnChanged();
		}
	}
}