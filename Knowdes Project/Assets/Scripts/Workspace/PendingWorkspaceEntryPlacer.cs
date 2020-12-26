using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Knowdes.Prototype
{
    public class PendingWorkspaceEntryPlacer : MonoBehaviour, IDropHandler
    {
        [SerializeField]
        private PendingWorkspaceEntry _entry;

        private Workspace _workspace;


		protected virtual void Start()
		{
			_workspace = FindObjectOfType<Workspace>();
		}


		private void place()
		{
			_workspace.Add(_entry.WorkspaceEntry);
			Vector2 rectPoint;
			RectTransformUtility.ScreenPointToLocalPointInRectangle(_workspace.Content, Input.mousePosition, GetComponentInParent<Canvas>().worldCamera, out rectPoint);
			Vector2 normalized = Rect.PointToNormalized(_workspace.Content.rect, rectPoint);
			_entry.WorkspaceEntry.Base.anchorMin = normalized;
			_entry.WorkspaceEntry.Base.anchorMax = normalized;
			_entry.WorkspaceEntry.Base.anchoredPosition = Vector2.zero;
			_entry.WorkspaceEntry.CursorFollower.Container = _workspace.Content;
		}

		public void OnDrop(PointerEventData eventData)
		{
			bool shallPlace = RectTransformUtility.RectangleContainsScreenPoint(_workspace.Rect, Input.mousePosition, GetComponentInParent<Canvas>().worldCamera);
			if (shallPlace)
				place();
			_workspace.PendingEntry = null;
			Destroy(_entry.Base.gameObject);
		}
	}
}