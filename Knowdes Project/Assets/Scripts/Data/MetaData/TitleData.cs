using System;

namespace Knowdes
{
	public class TitleData : MetaData
	{
		private string _content;
		public event Action OnContentChanged;
		public string Content
		{
			get => _content;
			set
			{
				_content = value;
				OnContentChanged?.Invoke();
			}
		}

		public override MetaDataType Type => MetaDataType.Title;

		public TitleData(Guid iD, string content) : base(iD)
		{
			_content = content;
		}
	}
}