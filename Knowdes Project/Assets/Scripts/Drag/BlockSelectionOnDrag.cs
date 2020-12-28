using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Knowdes.Prototype
{
	public class BlockSelectionOnDrag : MonoBehaviour, IBeginDragHandler, IEndDragHandler
	{
		[SerializeField]
		private UISelectable _selectable = null;


		public void OnBeginDrag(PointerEventData eventData)
		{
			_selectable.Blocked = true;
		}

		public void OnEndDrag(PointerEventData eventData)
		{
			_selectable.Blocked = false;
		}

		protected virtual void OnDestroy()
		{
			_selectable.Blocked = false;
		}
	}
}
