using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Knowdes
{
    public class TextContent : Content
    {
        [SerializeField]
        private TextMeshProUGUI _text;

        private TextContentData _contentData;
        public TextContentData ContentData
        {
            get => _contentData;
            set
            {

                removeListener();
                _contentData = value;
                updateText();
                addListener();
            }
        }

        protected virtual void Start()
		{
            
            updateText();
		}

        protected virtual void OnDestroy()
		{
            removeListener();
        }

        private void updateText()
		{
            _text.text = _contentData != null ? _contentData.Text : string.Empty;
        }

        private void addListener()
		{
            if (_contentData != null)
                _contentData.OnTextChanged += updateText;
        }

        private void removeListener()
		{
            if (_contentData != null)
                _contentData.OnTextChanged -= updateText;
        }
    }
}