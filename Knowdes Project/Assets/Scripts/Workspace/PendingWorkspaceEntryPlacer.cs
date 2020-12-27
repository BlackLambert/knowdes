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
			_workspace.Add(_entry.WorkspaceEntry, Input.mousePosition);
		}

		public void OnDrop(PointerEventData eventData)
		{
			bool shallPlace = RectTransformUtility.RectangleContainsScreenPoint(_workspace.RectTrans, Input.mousePosition, GetComponentInParent<Canvas>().worldCamera);
			if (shallPlace)
				place();
			_workspace.PendingEntry = null;
			Destroy(_entry.Base.gameObject);
		}
	}
}