using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knowdes
{
    public class MetaDataFactory
    {
        private const string _defaultTitle = "Titel";
        private const string _defaultComment = "Neuer Kommentar";

        public MetaData CreateNew(MetaDataType type)
		{
            Guid iD = new Guid();
            switch (type)
            {
                case MetaDataType.Title:
                    TitleData title = new TitleData(iD, _defaultTitle);
                    title.ShowInPreview = true;
                    return title;
                case MetaDataType.Author:
                    AuthorData author = new AuthorData(iD);
                    author.ShowInPreview = true;
                    return author;
                case MetaDataType.CreationDate:
                    CreationDateData creationData = new CreationDateData(iD);
                    creationData.ShowInPreview = false;
                    return creationData;
                case MetaDataType.LastChangedDate:
                    LastChangeDateData lastChangeDate = new LastChangeDateData(iD);
                    lastChangeDate.ShowInPreview = false;
                    return lastChangeDate;
                case MetaDataType.Tags:
                    TagsData tags = new TagsData(iD);
                    tags.ShowInPreview = true;
                    return tags;
                case MetaDataType.Comment:
                    CommentData comment = new CommentData(iD, _defaultComment);
                    comment.ShowInPreview = true;
                    return comment;
                default:
                    throw new NotImplementedException();
            }
        }
    }
}