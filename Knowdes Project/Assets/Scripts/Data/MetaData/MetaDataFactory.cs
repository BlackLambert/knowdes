using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knowdes
{
    public class MetaDataFactory
    {
        private const string _defaultTitle = "Titel";
        private const string _defaultAuthor = "Unbekannt";

        public MetaData Create(MetaDataType type)
		{
            Guid iD = Guid.NewGuid();
            switch(type)
			{
                case MetaDataType.Title:
                    return new TitleData(iD, _defaultTitle);
                case MetaDataType.Author:
                    return new AuthorData(iD, _defaultAuthor);
                case MetaDataType.CreationDate:
                    return new CreationDateData(iD);
                case MetaDataType.LastChangedDate:
                    return new LastChangeDateData(iD);
                case MetaDataType.Tags:
                    return new TagsData(iD);
                default:
                    throw new NotImplementedException();
			}
		}
    }
}