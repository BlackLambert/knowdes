using System;

namespace Knowdes
{
	public class ImageContentData : ContentData
	{
		private string _path = string.Empty;
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



		public ImageContentData(Guid iD, string path) : base(iD)
		{
			_path = path;
		}

		public override ContentType Type => ContentType.Image;
	}
}
