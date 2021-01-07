using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Knowdes
{
    public class CreationDateContent : MetaContent
    {
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

		public override MetaDataType Type => _data.Type;

		public override MetaData MetaData => _data;

		[SerializeField]
		private TextMeshProUGUI _text;

		private void updateText()
		{
			_text.text = _data.Date.ToString();
		}
	}
}