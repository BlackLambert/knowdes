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

        public Content Create(EntryData entryData)
		{
            ContentData contentData = entryData.Content;
            switch (contentData.Type)
			{
                case ContentDataType.Text:
                    TextContent content = Instantiate(_textContentPrefab);
                    content.EntryData = entryData;
                    return content;
                case ContentDataType.Weblink:
                    WeblinkContent weblinkContent = Instantiate(_weblinkContentPrefab);
                    weblinkContent.EntryData = entryData;
                    return weblinkContent;
                default:
                    throw new NotImplementedException();
			}
		}
    }
}