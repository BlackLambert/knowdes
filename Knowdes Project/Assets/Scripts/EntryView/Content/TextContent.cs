using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Knowdes
{
    public class TextContent : Content<TextContentData>
    {
        [SerializeField]
        private TextMeshProUGUI _text;

		private void updateText()
		{
            _text.text = Data != null ? Data.Text : string.Empty;
        }

        protected override void onDataAdded(TextContentData data)
        {
            if (Data != null)
                Data.OnTextChanged += updateText;
        }

        protected override void onDataRemoved(TextContentData data)
        {
            if (Data != null)
                Data.OnTextChanged -= updateText;
        }
    }
}