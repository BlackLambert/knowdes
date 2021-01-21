using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knowdes
{
    public class ContentDataFactory
    {
        private const string _defaultText = "Neuer Text";
        private const string _defaultPath = "Pfad eingeben";


        public ContentData Create(ContentDataType type)
		{
            Guid iD = Guid.NewGuid();
            switch(type)
			{
                case ContentDataType.Text:
                    return new TextContentData(iD, _defaultText);
                case ContentDataType.Weblink:
                    return new WeblinkContentData(iD);
                default:
                    throw new NotImplementedException();
			}
		}
    }
}