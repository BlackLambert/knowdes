using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knowdes
{
    public class ContentFactory : MonoBehaviour
    {
        [SerializeField]
        private TextContent _textContentPrefab;

        public TextContent Create(ContentData data)
		{
            switch(data.Type)
			{
                case ContentDataType.Text:
                    TextContentData textData = data as TextContentData;
                    TextContent content = Instantiate(_textContentPrefab);
                    content.ContentData = textData;
                    return content;
                default:
                    throw new NotImplementedException();
			}
		}
    }
}