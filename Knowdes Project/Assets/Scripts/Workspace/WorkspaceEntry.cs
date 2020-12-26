using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

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
		private RectTransform _contentHook;

		public Entry LinkedEntry { get; set; }


		public void SetContent(EntryContent content)
		{
			content.Base.SetParent(_contentHook, false);
			content.Base.localPosition = Vector3.one;
		}
	}
}
