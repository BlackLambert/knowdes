using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Knowdes
{
	public class LastChangeDateEditor : MetaContentEditor
	{
		public override MetaData MetaData => _data;

		private LastChangeDateData _data;
		public LastChangeDateData Data
		{
			get => _data;
			set
			{
				removeListeners();
				_data = value;
				updateText();
				addListeners();
			}
		}

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