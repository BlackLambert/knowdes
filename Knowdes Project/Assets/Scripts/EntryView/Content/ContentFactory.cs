using System;
using UnityEngine;

namespace Knowdes
{
    public class ContentFactory : MonoBehaviour
    {
        [SerializeField]
        private TextContent _textContentPrefab;
        [SerializeField]
        private WeblinkContent _weblinkContentPrefab;
        [SerializeField]
		private ImageContent _imageContentPrefab;
        [SerializeField]
        private UnknownFileContent _unknownFileContentPrefab;
        [SerializeField]
        private PdfContent _pdfContentPrefab;

        public Content Create(EntryData entryData)
		{
            ContentData contentData = entryData.Content;
            switch (contentData.Type)
			{
                case ContentType.Text:
                    TextContent content = Instantiate(_textContentPrefab);
                    content.EntryData = entryData;
                    return content;
                case ContentType.Weblink:
                    WeblinkContent weblinkContent = Instantiate(_weblinkContentPrefab);
                    weblinkContent.EntryData = entryData;
                    return weblinkContent;
                case ContentType.Image:
                    ImageContent imageContent = Instantiate(_imageContentPrefab);
                    imageContent.EntryData = entryData;
                    return imageContent;
                case ContentType.File:
                    UnknownFileContent unknownFileContent = Instantiate(_unknownFileContentPrefab);
                    unknownFileContent.EntryData = entryData;
                    return unknownFileContent;
                case ContentType.PDF:
                    PdfContent pdfContent = Instantiate(_pdfContentPrefab);
                    pdfContent.EntryData = entryData;
                    return pdfContent;
                default:
                    throw new NotImplementedException();
			}
		}
    }
}