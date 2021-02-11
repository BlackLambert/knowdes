using System;

namespace Knowdes
{
    public class ContentDataFactory
    {
        private const string _defaultText = "Neuer Text";


        public ContentData Create(ContentType type)
		{
            Guid iD = Guid.NewGuid();
            switch(type)
			{
                case ContentType.Text:
                    return new TextContentData(iD, _defaultText);
                case ContentType.Weblink:
                    return new WeblinkContentData(iD);
                case ContentType.Image:
                    return new ImageContentData(iD, string.Empty);
                case ContentType.File:
                    return new UnknownFileContentData(iD, string.Empty);
                default:
                    throw new NotImplementedException();
			}
		}
    }
}