using System;

namespace Knowdes
{
	public abstract class ContentData
	{
		public Guid ID { get; }

		public abstract ContentType Type { get; }

		public ContentData(Guid iD)
		{
			ID = iD;
		}
	}
}