using System;

namespace Knowdes
{
	public class DataContentData : ContentData
	{
		private string _path;
		public event Action OnPathChanged;
		public string Path
		{
			get => _path;
			set
			{
				_path = value;
				OnPathChanged?.Invoke();
			}
		}

		public override ContentType Type => ContentType.Data;

		public DataContentData(Guid iD, string path) : base(iD)
		{
			_path = path;
		}
	}
}