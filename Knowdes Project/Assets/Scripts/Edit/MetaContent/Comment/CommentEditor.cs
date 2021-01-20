using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Knowdes
{
    public class CommentEditor : MetaContentEditor<CommentData>
    {
		[SerializeField]
		private TMP_InputField _input;

		protected virtual void Start()
		{
			_input.onValueChanged.AddListener(onValueChanged);
		}

		protected override void OnDestroy()
		{
			base.OnDestroy();
			_input.onValueChanged.RemoveListener(onValueChanged);
		}

		private void updateText()
		{
			_input.text = Data.Content;
		}

		private void updateData()
		{
			Data.Content = _input.text;
		}

		protected override void onDataAdded(CommentData data)
		{
			if (data == null)
				return;
			updateText();
			data.OnContentChanged += updateText;
		}

		protected override void onDataRemoved(CommentData data)
		{
			if (data == null)
				return;
			data.OnContentChanged -= updateText;
		}

		private void onValueChanged(string arg0)
		{
			updateData();
		}
	}
}