using System;

namespace Knowdes
{
	public class FileContentData : ContentData
	{
		private string _path;
		public event Action OnPathChanged;
		public override event Action OnChanged;

		public string Path
		{
			get => _path;
			set
			{
				_path = value;
				OnPathChanged?.Invoke();
				OnChanged?.Invoke();
			}
		}

		public override ContentType Type => ContentType.File;

		public FileContentData(Guid iD, string path) : base(iD)
		{
			_path = path;
		}
	}
}