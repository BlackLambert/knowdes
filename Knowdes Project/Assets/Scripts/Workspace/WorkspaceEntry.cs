
using UnityEngine;
using UnityEngine.UI;

namespace Knowdes.Prototype
{
	public class WorkspaceEntry : MonoBehaviour
	{
		[SerializeField]
		private FollowCursorOnDrag _follower = null;
		public FollowCursorOnDrag CursorFollower => _follower;

		[SerializeField]
		private RectTransform _base;
		public RectTransform Base => _base;

		[SerializeField]
		private RectTransform _contentHook = null;

		[SerializeField]
		private UISelectable _selectable = null;

		public Entry LinkedEntry { get; set; }

		private EntryContent _content = null;


		public void SetContent(EntryContent content)
		{
			if (_content != null)
				_content.OnLayoutChanged -= updateLayout;
			_content = content;
			_content.Base.SetParent(_contentHook, false);
			_content.Base.localPosition = Vector3.one;
			_content.Selectable = _selectable;
			_content.OnLayoutChanged += updateLayout;
		}

		private void updateLayout()
		{
			LayoutRebuilder.ForceRebuildLayoutImmediate(_base);
		}
	}
}
