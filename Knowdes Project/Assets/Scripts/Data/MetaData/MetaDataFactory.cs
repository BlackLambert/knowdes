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
        private const string _defaultDescription = "Neue Beschreibung";

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
                case MetaDataType.Description:
                    DescriptionData description = new DescriptionData(iD, _defaultDescription);
                    description.ShowInPreview = true;
                    return description;
                case MetaDataType.PreviewImage:
                    PreviewImageData previewImage = new PreviewImageData(iD, null);
                    previewImage.ShowInPreview = true;
                    return previewImage;
                default:
                    throw new NotImplementedException();
            }
        }
    }
}