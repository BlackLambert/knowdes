using System;

namespace Knowdes
{
	public class PdfContentData : ContentData, TextbasedContentData, FilebasedContentData
	{
		private string _path = string.Empty;
		public event Action OnPathChanged;
		public override event Action OnChanged;
		public override ContentType Type => ContentType.PDF;

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

		public PdfContentData(Guid iD, string path) : base(iD)
		{
			_path = path;
		}
	}
}