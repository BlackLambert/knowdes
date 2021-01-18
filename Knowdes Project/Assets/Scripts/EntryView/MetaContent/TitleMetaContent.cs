using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Knowdes
{
    public class TitleMetaContent : MetaContent<TitleData>
    {
		[SerializeField]
		private TextMeshProUGUI _text;

		protected override void onDataAdded(TitleData data)
		{
			if (data == null)
				return;

			data.OnContentChanged += updateText;
		}

		protected override void onDataRemoved(TitleData data)
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