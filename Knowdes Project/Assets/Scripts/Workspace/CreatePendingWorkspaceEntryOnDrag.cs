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
		[SerializeField]
		private Entry _entry = null;

		private Workspace _workspace;
		private EntryVolumeFactory _volumeFactory;

		protected virtual void Start()
		{
			_workspace = FindObjectOfType<Workspace>();
			_volumeFactory = FindObjectOfType<EntryVolumeFactory>();
		}

		public void OnBeginDrag(PointerEventData eventData)
		{
			if (_workspace.HasEntry(_entry))
				return;

			PendingWorkspaceEntry result = Instantiate(_entryPrefab);
			result.Base.SetParent(GetComponentInParent<Canvas>().transform, false);
			result.Base.localScale = Vector3.one;
			result.WorkspaceEntry.LinkedEntry = _entry;
			result.WorkspaceEntry.Set(_volumeFactory.Create(_entry.Volume));
			result.WorkspaceEntry.Volume.Data = _entry.Volume.Data;
			result.Base.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, _entry.Base.rect.width);
			_workspace.PendingEntry = result;
		}

		public void OnDrag(PointerEventData eventData)
		{
			
		}
	}
}