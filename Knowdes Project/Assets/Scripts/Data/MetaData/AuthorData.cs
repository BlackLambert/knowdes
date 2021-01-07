using System;
using System.Collections.Generic;

namespace Knowdes
{
	public class AuthorData : MetaData
	{
		private List<Author> _authors = new List<Author>();
		public List<Author> AuthorsCopy => new List<Author>(_authors);


		public override MetaDataType Type => MetaDataType.Author;
		public event Action<Author> OnAdded;
		public event Action<Author> OnRemoved;

		public AuthorData(Guid iD) : base(iD)
		{
			
		}

		public void Add(Author author)
		{
			if (_authors.Contains(author))
				throw new InvalidOperationException();
			_authors.Add(author);
		}

		public void Remove(Author author)
		{
			if (!_authors.Contains(author))
				throw new InvalidOperationException();
			_authors.Remove(author);
		}
	}
}