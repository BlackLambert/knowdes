using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Knowdes.Prototype
{
	public class CreatePendingWorkspaceEntryOnDrag : MonoBehaviour, IBeginDragHandler, IDragHandler
	{
		[SerializeField]
		private PendingWorkspaceEntry _entryPrefab = null;

		public void OnBeginDrag(PointerEventData eventData)
		{
			PendingWorkspaceEntry result = Instantiate(_entryPrefab);
			result.transform.SetParent(GetComponentInParent<Canvas>().transform, false);
			result.transform.localScale = Vector3.one;
		}

		public void OnDrag(PointerEventData eventData)
		{
			
		}
	}
}