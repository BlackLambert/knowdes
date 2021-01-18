
using TMPro;
using UnityEngine;

namespace Knowdes
{
	public class TagsContent : MetaContent<TagsData>
	{
		[SerializeField]
		private TextMeshProUGUI _text;

		protected override void OnDestroy()
		{
			base.OnDestroy();
			onDataRemoved(Data);
		}

		private void initTag(Tag tag)
		{
			tag.OnNameChanged += updateText;
			updateText();
		}

		private void cleanTag(Tag tag)
		{
			tag.OnNameChanged -= updateText;
			updateText();
		}

		private void updateText()
		{
			string text = string.Empty;
			foreach(Tag tag in Data.TagsCopy)
			{
				if (!string.IsNullOrEmpty(text))
					text += " | ";
				text += tag.Name;
			}
			_text.text = text;
		}

		protected override void onDataAdded(TagsData data)
		{
			if (data == null)
				return;
			foreach (Tag tag in Data.TagsCopy)
				initTag(tag);
			data.OnTagsAdded += initTag;
			data.OnTagsRemoved += cleanTag;
			updateText();
		}

		protected override void onDataRemoved(TagsData data)
		{
			if (data == null)
				return;
			foreach (Tag tag in Data.TagsCopy)
				cleanTag(tag);
			data.OnTagsAdded -= initTag;
			data.OnTagsRemoved -= cleanTag;
		}
	}
}