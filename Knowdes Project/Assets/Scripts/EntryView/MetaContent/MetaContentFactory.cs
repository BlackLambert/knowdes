using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knowdes
{
    public class MetaContentFactory : MonoBehaviour
    {
        [SerializeField]
        private TitleContent _titleContentPrefab;
        [SerializeField]
        private TagsContent _tagsContentPrefab;
        [SerializeField]
        private CreationDateContent _creationDateContentPrefab;
        [SerializeField]
        private LastChangedDateContent _lastChangedDateContentPrefab;
        [SerializeField]
        private CommentContent _commentContentPrefab;
        [SerializeField]
        private DescriptionContent _descriptionContentPrefab;
        [SerializeField]
        private PreviewImageContent _previewImagePrefab;

        public MetaContent Create(MetaData data, EntryVolume volume)
		{
            MetaContent result = create(data);
            result.Volume = volume;
            volume.AddMetaContent(result);
            return result;
		}

        private MetaContent create(MetaData data)
        {
            switch (data.Type)
            {
                case MetaDataType.Title:
                    TitleContent result = Instantiate(_titleContentPrefab);
                    result.Data = data as TitleData;
                    return result;
                case MetaDataType.Tags:
                    TagsContent tags = Instantiate(_tagsContentPrefab);
                    tags.Data = data as TagsData;
                    return tags;
                case MetaDataType.CreationDate:
                    CreationDateContent creationDate = Instantiate(_creationDateContentPrefab);
                    creationDate.Data = data as CreationDateData;
                    return creationDate;
                case MetaDataType.LastChangedDate:
                    LastChangedDateContent lastChangeDate = Instantiate(_lastChangedDateContentPrefab);
                    lastChangeDate.Data = data as LastChangeDateData;
                    return lastChangeDate;
                case MetaDataType.Comment:
                    CommentContent commentContent = Instantiate(_commentContentPrefab);
                    commentContent.Data = data as CommentData;
                    return commentContent;
                case MetaDataType.Description:
                    DescriptionContent descriptionContent = Instantiate(_descriptionContentPrefab);
                    descriptionContent.Data = data as DescriptionData;
                    return descriptionContent;
                case MetaDataType.PreviewImage:
                    PreviewImageContent previewImageContent = Instantiate(_previewImagePrefab);
                    previewImageContent.Data = data as PreviewImageData;
                    return previewImageContent;
                default:
                    throw new NotImplementedException();
            }
        }
    }
}