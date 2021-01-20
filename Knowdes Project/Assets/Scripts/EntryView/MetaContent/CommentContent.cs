using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Knowdes
{
	public class CommentContent : MetaContent<CommentData>
	{
		[SerializeField]
		private TextMeshProUGUI _text;

		private void updateText()
		{
			_text.text = Data.Content.ToString();
		}

		protected override void onDataAdded(CommentData data)
		{
			if (data == null)
				return;
			data.OnContentChanged += updateText;
			updateText();
		}

		protected override void onDataRemoved(CommentData data)
		{
			if (data == null)
				return;
			data.OnContentChanged -= updateText;
		}
	}
}