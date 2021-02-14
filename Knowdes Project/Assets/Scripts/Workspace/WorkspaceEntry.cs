
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Knowdes
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
		public EntryVolume Volume { get; private set; } = null;

		public event Action OnVolumeSet;


		public void Set(EntryVolume content)
		{
			Volume = content;
			Volume.Base.SetParent(_contentHook, false);
			Volume.Base.localPosition = Vector3.one;
			OnVolumeSet?.Invoke();
		}
	}
}
