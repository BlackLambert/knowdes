using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knowdes
{
    public class TextContentData : ContentData
    {
        private string _text = string.Empty;

		public event Action OnTextChanged;
        public string Text
		{
            get => _text;
            set
			{
                _text = value;
                OnTextChanged?.Invoke();
			}
        }

		public override ContentDataType Type => ContentDataType.Text;

		public TextContentData(Guid iD, string text) : base(iD)
        {
            _text = text;
        }
    }
}