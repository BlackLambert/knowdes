using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Knowdes
{
    public class LastChangedDateContent : MetaContent
    {
		private LastChangeDateData _data;
		public LastChangeDateData Data
		{
			get => _data;
			set
			{
				removeListeners();
				_data = value;
				addListeners();
				updateText();
			}
		}

		public override MetaDataType Type => _data.Type;

		public override MetaData MetaData => _data;

		[SerializeField]
		private TextMeshProUGUI _text;

		private void updateText()
		{
			_text.text = _data.Date.ToString();
		}

		private void addListeners()
		{
			if (_data == null)
				return;
			_data.OnDateChanged += updateText;
		}

		private void removeListeners()
		{
			if (_data == null)
				return;
			_data.OnDateChanged -= updateText;
		}
	}
}