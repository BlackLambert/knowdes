using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Knowdes
{
    public class WeblinkContent : Content<WeblinkContentData>
    {
        [SerializeField]
        private TextMeshProUGUI _text;
        [SerializeField]
        private GameObject _textObject;
        [SerializeField]
        private LinkPreview _linkPreview;

		private void updateText()
        {
            _textObject.SetActive(!Data.Empty);
            _text.text = Data != null ? Data.UrlString : string.Empty;
            _linkPreview.URL = Data != null ? Data.Url : null;
        }

		protected override void onDataAdded(WeblinkContentData data)
		{
            if (data == null)
                return;
            updateText();
            data.OnUrlChanged += updateText;
        }

		protected override void onDataRemoved(WeblinkContentData data)
		{
            if (data == null)
                return;
            data.OnUrlChanged -= updateText;
        }
	}
}