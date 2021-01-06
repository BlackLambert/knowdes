using System;

namespace Knowdes
{
	public class AuthorData : MetaData
	{
		private string _name;
		public event Action OnNameChanged;
		public string Name
		{
			get => _name;
			set
			{
				_name = value;
				OnNameChanged?.Invoke();
			}
		}

		public override MetaDataType Type => MetaDataType.Author;

		public AuthorData(Guid iD, string name) : base(iD)
		{
			_name = name;
		}
	}
}