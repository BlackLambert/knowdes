using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knowdes
{
	public class CommentData : MetaData
	{
		public event Action OnContentChanged;
		private string _content;
		public string Content
		{
			get => _content;
			set
			{
				_content = value;
				invokeOnChanged();
				OnContentChanged?.Invoke();
			}
		}


		public override MetaDataType Type => MetaDataType.Comment;

		public override int Priority => 100;

		public override bool Destroyable => true;


		public CommentData(Guid iD) : base(iD)
		{
			_content = string.Empty;
			init();
		}

		public CommentData(Guid iD, string defaultText) : base(iD)
		{
			_content = defaultText;
			init();
		}

		private void init()
		{
			ShowInPreview = true;
		}
	}
}