using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knowdes
{
    public class ContentEditorFactory : MonoBehaviour
    {
        [SerializeField]
        private TextContentEditor _textEditor = null;

        public ContentEditor Create(ContentData contentData)
		{
            switch (contentData.Type)
            {
                case ContentDataType.Text:
                    TextContentData data = contentData as TextContentData;
                    TextContentEditor result = Instantiate(_textEditor);
                    result.ContentData = data;
                    return result;
                default:
                    throw new NotImplementedException();
            }
        }
    }
}