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
				OnChanged?.Invoke();
			}
		}

		public abstract MetaDataType Type { get; }
		public abstract int Priority { get; }

		public bool Destroyable { get; }

		public event Action OnChanged;

		public MetaData(Guid iD, bool destroyable)
		{
			ID = iD;
			Destroyable = destroyable;
		}

		protected void invokeOnChanged()
		{
			OnChanged?.Invoke();
		}
	}
}