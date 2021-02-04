using System;

namespace Knowdes
{
	public class DescriptionData : MetaData
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

		public override MetaDataType Type => MetaDataType.Description;

		public override int Priority => 1000;

		public override bool Destroyable => true;

		public DescriptionData(Guid iD, string content) : base(iD)
		{
			_content = content;
		}
	}
}