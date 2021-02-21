using System;

namespace Knowdes
{
	public class PreviewImageData : MetaData, TextBasedMetaData
	{
		private Uri _path;
		public event Action OnPathChanged;
		public Uri Uri
		{
			get => _path;
			set
			{
				_path = value;
				OnPathChanged?.Invoke();
				invokeOnChanged();
			}
		}

		public override MetaDataType Type => MetaDataType.PreviewImage;

		public override int Priority => 999;

		public override bool Destroyable => true;
		public bool IsEmpty => Uri == null || string.IsNullOrEmpty(Uri.AbsoluteUri);

		public string Content => Uri.OriginalString;

		public PreviewImageData(Guid iD, Uri path) :base(iD)
		{
			Uri = path;
			ShowInPreview = true;
		}
	}
}