using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Knowdes
{
    public class CreationDateContent : MetaContent<CreationDateData>
    {

		[SerializeField]
		private TextMeshProUGUI _text;

		private void updateText()
		{
			_text.text = Data.Date.ToString();
		}

		protected override void onDataAdded(CreationDateData data)
		{
			updateText();
		}

		protected override void onDataRemoved(CreationDateData data)
		{
			
		}
	}
}