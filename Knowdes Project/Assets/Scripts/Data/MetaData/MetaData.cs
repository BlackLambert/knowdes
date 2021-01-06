using System;

namespace Knowdes
{
	public abstract class MetaData
	{
		public Guid ID { get; }
		private bool _showInPreview = false;
		public event Action OnShowInPreviewChanged;
		public bool ShowInPreview
		{
			get => _showInPreview;
			set
			{
				_showInPreview = value;
				OnShowInPreviewChanged?.Invoke();
			}
		}

		public abstract MetaDataType Type { get; }

		public MetaData(Guid iD)
		{
			ID = iD;
		}
	}
}