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

        public ContentEditor Create(ContentData contentData)
		{
            switch (contentData.Type)
            {
                case ContentDataType.Text:
                    TextContentData data = contentData as TextContentData;
                    TextContentEditor result = Instantiate(_textEditor);
                    result.Data = data;
                    return result;
                case ContentDataType.Weblink:
                    WeblinkContentData weblinkData = contentData as WeblinkContentData;
                    WeblinkContentEditor weblinkEditor = Instantiate(_weblinkEditor);
                    weblinkEditor.Data = weblinkData;
                    return weblinkEditor;
                default:
                    throw new NotImplementedException();
            }
        }
    }
}