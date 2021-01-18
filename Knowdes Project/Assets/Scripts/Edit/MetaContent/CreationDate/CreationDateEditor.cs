using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Knowdes
{
	public class CreationDateEditor : MetaContentEditor<CreationDateData>
	{
		[SerializeField]
		private TextMeshProUGUI _text;

		protected override void onDataAdded(CreationDateData data)
		{
			updateText();
		}

		protected override void onDataRemoved(CreationDateData data)
		{
			
		}

		private void updateText()
		{
			_text.text = Data.Date.ToString();
		}
	}
}