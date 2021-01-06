using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knowdes
{
    public class MetaContentCreator : MonoBehaviour
    {
        [SerializeField]
        private RectTransform _titleHook;
        [SerializeField]
        private RectTransform _metaDatasHook;
        [SerializeField]
        private EntryVolume _volume;

        private MetaContentFactory _factory;

        protected virtual void Start()
		{
            _factory = FindObjectOfType<MetaContentFactory>();
            _volume.Data.OnMetaDataAdded += addMetaContent;
            _volume.Data.OnMetaDataRemoved += removeMetaContent;
            init();
        }

		protected virtual void OnDestroy()
		{

            _volume.Data.OnMetaDataAdded -= addMetaContent;
            _volume.Data.OnMetaDataRemoved -= removeMetaContent;
        }

        private void init()
		{
            foreach (KeyValuePair<MetaDataType, MetaData> data in _volume.Data.MetaDatasCopy)
			{
                addMetaContent(data.Value);
			}
		}



        private void addMetaContent(MetaData data)
        {
            MetaContent content = _factory.Create(data, _volume);
            switch(content.MetaData.Type)
			{
                case MetaDataType.Title:
                    content.Base.SetParent(_titleHook);
                    break;
                default:
                    content.Base.SetParent(_metaDatasHook);
                    break;
			}
            content.Base.localScale = Vector3.one;
        }

        private void removeMetaContent(MetaData data)
        {
            MetaContent content = _volume.Get(data.Type);
            _volume.RemoveMetaContent(content);
            Destroy(content.Base);
        }
    }
}