

using TMPro;
using UnityEngine;

namespace Knowdes
{
	public class LastChangeDateEditor : MetaContentEditor<LastChangeDateData>
	{
		[SerializeField]
		private TextMeshProUGUI _text;

		private void updateText()
		{
			_text.text = Data.Date.ToString();
		}

		protected override void onDataAdded(LastChangeDateData data)
		{
			if (data == null)
				return;
			data.OnDateChanged += updateText;
			updateText();
		}

		protected override void onDataRemoved(LastChangeDateData data)
		{
			if (data == null)
				return;
			data.OnDateChanged -= updateText;
		}
	}
}