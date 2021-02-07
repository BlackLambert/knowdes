using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

namespace Knowdes
{
    public class TextContentEditor : ContentEditor<TextContentData>
    {
        [SerializeField]
        private TMP_InputField _input = null;

        private void updateText()
		{
            _input.text = Data != null ? Data.Text : string.Empty;
		}

        private void updateContentText(string value)
        {
            Data.Text = value;
        }

		protected override void onDataAdded(TextContentData data)
		{
			if (data == null)
				return;
			updateText();
			_input.onValueChanged.AddListener(updateContentText);
		}

		protected override void onDataRemoved(TextContentData data)
		{
			if (data == null)
				return;
			_input.onValueChanged.RemoveListener(updateContentText);
		}
	}
}