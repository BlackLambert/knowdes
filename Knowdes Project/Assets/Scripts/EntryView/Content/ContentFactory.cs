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
        [SerializeField]
        private WeblinkContent _weblinkContentPrefab;

        public Content Create(ContentData data)
		{
            switch(data.Type)
			{
                case ContentDataType.Text:
                    TextContentData textData = data as TextContentData;
                    TextContent content = Instantiate(_textContentPrefab);
                    content.Data = textData;
                    return content;
                case ContentDataType.Weblink:
                    WeblinkContentData weblinkData = data as WeblinkContentData;
                    WeblinkContent weblinkContent = Instantiate(_weblinkContentPrefab);
                    weblinkContent.Data = weblinkData;
                    return weblinkContent;
                default:
                    throw new NotImplementedException();
			}
		}
    }
}