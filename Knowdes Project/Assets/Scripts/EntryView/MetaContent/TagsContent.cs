using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Knowdes
{
	public class TagsContent : MetaContent
	{
		[SerializeField]
		private TextMeshProUGUI _text;

		public override MetaDataType Type => Data.Type;

		public override MetaData MetaData => Data;

		private TagsData _data;
		public TagsData Data
		{
			get => _data;
			set
			{
				clean();
				_data = value;
				updateVisibility();
				init();
			}
		}

		protected override void OnDestroy()
		{
			base.OnDestroy();
			clean();
		}

		private void init()
		{
			if (_data == null)
				return;
			foreach (Tag tag in Data.TagsCopy)
				initTag(tag);
			_data.OnTagsAdded += initTag;
			_data.OnTagsRemoved += cleanTag;
			updateText();
		}

		private void clean()
		{
			if (_data == null)
				return;
			foreach (Tag tag in Data.TagsCopy)
				cleanTag(tag);
			_data.OnTagsAdded -= initTag;
			_data.OnTagsRemoved -= cleanTag;
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
			foreach(Tag tag in _data.TagsCopy)
			{
				if (!string.IsNullOrEmpty(text))
					text += " | ";
				text += tag.Name;
			}
			_text.text = text;
		}
	}
}