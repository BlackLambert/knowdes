using System;
using UnityEngine;

namespace Knowdes
{
    public class ContentEditorFactory : MonoBehaviour
    {
        [SerializeField]
        private TextContentEditor _textEditorPrefab = null;
        [SerializeField]
        private WeblinkContentEditor _weblinkEditorPrefab = null;
        [SerializeField]
        private ImageContentEditor _imageEditorPrefab = null;
        [SerializeField]
        private UnknownFileEditor _unknownFileEditorPrefab = null;
        [SerializeField]
        private PdfContentEditor _pdfContentEditorPrefab = null;

        public ContentEditor Create(EntryData entryData)
		{
            ContentData contentData = entryData.Content;
            switch (contentData.Type)
            {
                case ContentType.Text:
                    TextContentEditor result = Instantiate(_textEditorPrefab);
                    result.EntryData = entryData;
                    return result;
                case ContentType.Weblink:
                    WeblinkContentEditor weblinkEditor = Instantiate(_weblinkEditorPrefab);
                    weblinkEditor.EntryData = entryData;
                    return weblinkEditor;
                case ContentType.Image:
                    ImageContentEditor imageContentEditor = Instantiate(_imageEditorPrefab);
                    imageContentEditor.EntryData = entryData;
                    return imageContentEditor;
                case ContentType.File:
                    UnknownFileEditor unknownFileEditor = Instantiate(_unknownFileEditorPrefab);
                    unknownFileEditor.EntryData = entryData;
                    return unknownFileEditor;
                case ContentType.PDF:
                    PdfContentEditor pdfEditor = Instantiate(_pdfContentEditorPrefab);
                    pdfEditor.EntryData = entryData;
                    return pdfEditor;
                default:
                    throw new NotImplementedException();
            }
        }
    }
}