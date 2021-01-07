using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knowdes
{
	public class TagsData : MetaData
	{
		private List<Tag> _tags = new List<Tag>();
		public List<Tag> TagsCopy => new List<Tag>(_tags);

		public override MetaDataType Type => MetaDataType.Tags;
		public override int Priority => 0;

		public event Action<Tag> OnTagsAdded;
		public event Action<Tag> OnTagsRemoved;

		public TagsData(Guid iD, List<Tag> initialTags) : base(iD, true)
		{
			_tags = initialTags;
			init();
		}

		private void init()
		{
			foreach(Tag tag in _tags)
			{
				tag.OnNameChanged += invokeOnChanged;
			}
		}

		public void AddTag(Tag tag)
		{
			if (_tags.Contains(tag))
				throw new InvalidOperationException();
			_tags.Add(tag);
			OnTagsAdded?.Invoke(tag);
			tag.OnNameChanged += invokeOnChanged;
			invokeOnChanged();
		}

		public void RemoveTag(Tag tag)
		{
			if (!_tags.Contains(tag))
				throw new InvalidOperationException();
			_tags.Remove(tag);
			OnTagsRemoved?.Invoke(tag);
			tag.OnNameChanged -= invokeOnChanged;
			invokeOnChanged();
		}
	}
}