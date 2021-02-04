using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Knowdes
{
	public class DescriptionContent : MetaContent<DescriptionData>
	{
		[SerializeField]
		private TextMeshProUGUI _text;

		protected override void onDataAdded(DescriptionData data)
		{
			if (data == null)
				return;

			data.OnContentChanged += updateText;
			updateText();
		}

		protected override void onDataRemoved(DescriptionData data)
		{
			if (data == null)
				return;

			data.OnContentChanged -= updateText;
		}

		private void updateText()
		{
			_text.text = Data.Content;
		}
	}
}