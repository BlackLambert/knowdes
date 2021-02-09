using System;

namespace Knowdes
{
	public class ImageContentData : ContentData, TextbasedContentData
	{
		private string _path = string.Empty;
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


		public override ContentType Type => ContentType.Image;

		public string Content => Path;


		public ImageContentData(Guid iD, string path) : base(iD)
		{
			_path = path;
		}
	}
}
