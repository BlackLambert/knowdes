using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knowdes.Prototype
{
    public class ContentCreator : MonoBehaviour
    {
        [SerializeField]
        private RectTransform _hook;
        [SerializeField]
        private EntryVolume _entryVolume;

        private ContentFactory _factory;

        private ContentData ContentData => _entryVolume.Data.Content;

        protected virtual void Start()
		{
            _factory = FindObjectOfType<ContentFactory>();
            _entryVolume.Data.OnContentChanged += onContentChanged;
            createContent();
        }

		protected virtual void OnDestroy()
        {
            _entryVolume.Data.OnContentChanged -= onContentChanged;
            cleanContent();
        }

        private void createContent()
        {
            if (_entryVolume.Content != null)
                throw new InvalidOperationException();
            if (ContentData == null)
                return;

            Content content = _factory.Create(ContentData);
            content.Base.SetParent(_hook, false);
            content.Base.localScale = Vector3.one;
            _entryVolume.Content = content;
        }

        private void cleanContent()
        {
            if (_entryVolume.Content == null)
                return;
            Destroy(_entryVolume.Content.Base.gameObject);
            _entryVolume.Content = null;
        }



        private void onContentChanged()
        {
            cleanContent();
            createContent();
        }
    }
}