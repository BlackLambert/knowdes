using System;

namespace Knowdes
{
	public interface TextbasedContentData
	{
		Guid Id { get; }
		ContentType Type { get; }
		string Content { get; }
	}
}