using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knowdes
{
    public class MetaContentFactory : MonoBehaviour
    {
        [SerializeField]
        private TitleMetaContent _titleContentPrefab;
        [SerializeField]
        private TagsContent _tagsContentPrefab;
        [SerializeField]
        private CreationDateContent _creationDateContentPrefab;
        [SerializeField]
        private LastChangedDateContent _lastChangedDateContentPrefab;

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
                    TitleMetaContent result = Instantiate(_titleContentPrefab);
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
                default:
                    throw new NotImplementedException();
            }
        }
    }
}