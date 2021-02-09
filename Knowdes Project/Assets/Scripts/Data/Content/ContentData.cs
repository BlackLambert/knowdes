using System;

namespace Knowdes
{
	public abstract class ContentData
	{
		public Guid Id { get; }
		public abstract ContentType Type { get; }
		public abstract event Action OnChanged;

		public ContentData(Guid iD)
		{
			Id = iD;
		}
	}
}