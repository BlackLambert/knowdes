using System;
using UnityEngine;

namespace Knowdes
{
    public class ContentEditorFactory : MonoBehaviour
    {
        [SerializeField]
        private TextContentEditor _textEditor = null;
        [SerializeField]
        private WeblinkContentEditor _weblinkEditor = null;

        public ContentEditor Create(EntryData entryData)
		{
            ContentData contentData = entryData.Content;
            switch (contentData.Type)
            {
                case ContentDataType.Text:
                    TextContentEditor result = Instantiate(_textEditor);
                    result.EntryData = entryData;
                    return result;
                case ContentDataType.Weblink:
                    WeblinkContentEditor weblinkEditor = Instantiate(_weblinkEditor);
                    weblinkEditor.EntryData = entryData;
                    return weblinkEditor;
                default:
                    throw new NotImplementedException();
            }
        }
    }
}