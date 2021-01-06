using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

namespace Knowdes
{
    public class TextContentEditor : ContentEditor
    {
        [SerializeField]
        private TMP_InputField _input = null;

        private TextContentData _contentData;
        public TextContentData ContentData
		{
            get => _contentData;
            set
			{
                _contentData = value;
                updateText();
            }
		}

        private void updateText()
		{
            _input.text = _contentData != null ? _contentData.Text : string.Empty;
		}

        protected virtual void Start()
		{
            _input.onValueChanged.AddListener(updateContentText);
		}

		protected virtual void OnDestroy()
		{
            _input.onValueChanged.RemoveListener(updateContentText);
        }

        private void updateContentText(string value)
        {
            _contentData.Text = value;
        }
    }
}