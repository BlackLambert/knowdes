using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Knowdes
{
    public class TitleEditor : MetaContentEditor
    {
        [SerializeField]
        private TMP_InputField _input;

		public override MetaData MetaData => _data;

        private TitleData _data;
        public TitleData Data
		{
            get => _data;
            set
			{
				_input.onValueChanged.RemoveListener(onTextChanged);
				_data = value;
				updateText();
				_input.onValueChanged.AddListener(onTextChanged);
			}
		}

		protected override void OnDestroy()
		{
			_input.onValueChanged.RemoveListener(onTextChanged);
		}

		private void onTextChanged(string text)
		{
			_data.Content = text;
		}

		private void updateText()
		{
			_input.text = _data.Content;
		}
	}
}