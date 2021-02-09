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
				invokeOnChanged();
			}
		}

		public override int Priority => int.MaxValue;

		public override MetaDataType Type => MetaDataType.Title;

		public override bool Destroyable => true;

		public TitleData(Guid iD, string content) : base(iD)
		{
			_content = content;
			ShowInPreview = true;
		}
	}
}