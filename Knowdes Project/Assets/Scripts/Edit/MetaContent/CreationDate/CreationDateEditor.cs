using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Knowdes
{
	public class CreationDateEditor : MetaContentEditor
	{
		public override MetaData MetaData => _data;

		private CreationDateData _data;
		public CreationDateData Data
		{
			get => _data;
			set
			{
				_data = value;
				updateText();
			}
		}

		[SerializeField]
		private TextMeshProUGUI _text;

		private void updateText()
		{
			_text.text = _data.Date.ToString();
		}
	}
}