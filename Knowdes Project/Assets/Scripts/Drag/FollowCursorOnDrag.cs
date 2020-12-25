using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Knowdes
{
	public class FollowCursorOnDrag : MonoBehaviour, IDragHandler, IBeginDragHandler
	{

		[SerializeField]
		private RectTransform _base = null;
		public RectTransform Container { get; set; }


		private Vector2 _offset = Vector2.zero;

		public void OnBeginDrag(PointerEventData eventData)
		{
			if (Container == null)
				throw new InvalidOperationException();

			Vector2 rectPoint;
			RectTransformUtility.ScreenPointToLocalPointInRectangle(Container, Input.mousePosition, GetComponentInParent<Canvas>().worldCamera, out rectPoint);
			_offset = new Vector2(_base.localPosition.x, _base.localPosition.y) - rectPoint;
		}

		public void OnDrag(PointerEventData eventData)
		{
			if (Container == null)
				throw new InvalidOperationException();

			Vector2 rectPoint;
			RectTransformUtility.ScreenPointToLocalPointInRectangle(Container, Input.mousePosition, GetComponentInParent<Canvas>().worldCamera, out rectPoint);
			Vector2 normalized = Rect.PointToNormalized(Container.rect, rectPoint + _offset);
			_base.anchorMin = normalized;
			_base.anchorMax = normalized;
			_base.anchoredPosition = Vector2.zero;
		}
	}
}