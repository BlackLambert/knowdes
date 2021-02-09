using System;

namespace Knowdes
{
    public class TextContentData : ContentData, TextbasedContentData
    {
        private string _text = string.Empty;

		public event Action OnTextChanged;
		public override event Action OnChanged;

		public string Text
		{
            get => _text;
            set
			{
                _text = value;
                OnTextChanged?.Invoke();
                OnChanged?.Invoke();
            }
        }

		public override ContentType Type => ContentType.Text;

		public string Content => Text;

		public TextContentData(Guid iD, string text) : base(iD)
        {
            _text = text;
        }
    }
}