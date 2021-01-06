
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

		public Entry LinkedEntry { get; set; }

		private EntryVolume _content = null;
		public EntryVolume Content => _content;


		public void SetContent(EntryVolume content)
		{

			_content = content;
			_content.Base.SetParent(_contentHook, false);
			_content.Base.localPosition = Vector3.one;
		}

		private void updateLayout()
		{
			LayoutRebuilder.ForceRebuildLayoutImmediate(_base);
		}
	}
}
