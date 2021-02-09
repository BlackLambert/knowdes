using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knowdes
{
	public class PreviewImageData : MetaData
	{
		private Uri _path;
		public event Action OnPathChanged;
		public Uri Uri
		{
			get => _path;
			set
			{
				_path = value;
				OnPathChanged?.Invoke();
				invokeOnChanged();
			}
		}

		public override MetaDataType Type => MetaDataType.PreviewImage;

		public override int Priority => 999;

		public override bool Destroyable => true;
		public bool IsEmpty => Uri == null || string.IsNullOrEmpty(Uri.AbsoluteUri);

		public PreviewImageData(Guid iD, Uri path) :base(iD)
		{
			Uri = path;
			ShowInPreview = true;
		}
	}
}