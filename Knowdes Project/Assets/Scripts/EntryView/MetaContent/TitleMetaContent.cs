using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Knowdes
{
    public class TitleMetaContent : MetaContent
    {
		[SerializeField]
		private TextMeshProUGUI _text;

        private TitleData _data;
		public TitleData Data
		{
			get => _data;
			set
			{
				removeDataListeners();
				_data = value;
				updateVisibility();
				updateText();
				addDataListeners();
			}
		}

		public override MetaDataType Type => _data.Type;

		public override MetaData MetaData => _data;

		protected virtual void OnDestroy()
		{
			
		}

		private void addDataListeners()
		{
			if (_data == null)
				return;

			_data.OnContentChanged += updateText;
			_data.OnShowInPreviewChanged += updateVisibility;
		}


		private void removeDataListeners()
		{
			if (_data == null)
				return;
			
			_data.OnContentChanged -= updateText;
			_data.OnShowInPreviewChanged -= updateVisibility;
			
		}

		private void updateText()
		{
			_text.text = _data.Content;
		}
	}
}