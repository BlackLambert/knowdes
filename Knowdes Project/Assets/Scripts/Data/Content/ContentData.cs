using System;

namespace Knowdes
{
	public abstract class ContentData
	{
		public Guid ID { get; }

		public abstract ContentDataType Type { get; }

		public ContentData(Guid iD)
		{
			ID = iD;
		}
	}
}