using System;

namespace Knowdes
{
    public class UnknownFileContentData : ContentData, FilebasedContentData, TextbasedContentData
	{
		private string _path = string.Empty;
		public event Action OnPathChanged;
		public override ContentType Type => ContentType.File;

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
		public string Content => @Path;

		public override event Action OnChanged;

		public UnknownFileContentData(Guid iD, string path) : base(iD)
		{
		}

		
    }
}