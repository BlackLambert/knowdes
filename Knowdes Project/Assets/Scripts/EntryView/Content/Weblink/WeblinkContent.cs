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

		private void updateText()
        {
            _textObject.SetActive(!Data.Empty);
            _text.text = Data != null ? Data.UrlString : string.Empty;
        }

		protected override void onDataAdded(WeblinkContentData data)
		{
            updateText();
            data.OnUrlChanged += updateText;
        }

		protected override void onDataRemoved(WeblinkContentData data)
		{
            data.OnUrlChanged -= updateText;
        }
	}
}