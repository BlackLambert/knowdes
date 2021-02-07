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
                default:
                    throw new NotImplementedException();
            }
        }
    }
}