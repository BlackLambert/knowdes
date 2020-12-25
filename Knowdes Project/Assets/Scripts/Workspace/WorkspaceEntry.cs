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

	}
}
