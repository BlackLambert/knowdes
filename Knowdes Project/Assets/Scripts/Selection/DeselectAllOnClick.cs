using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Knowdes
{
	public class DeselectAllOnClick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
	{
		private Selector _selector;

		private const float _maxDownTime = 0.5f;
		private const float _maxMouseDelta = 5f;

		private float _downTime = 0;
		private Vector2 _downPosition = Vector2.zero;

		protected virtual void Start()
		{
			_selector = FindObjectOfType<Selector>();
			Assert.IsNotNull(_selector);
		}

		public void OnPointerDown(PointerEventData eventData)
		{
			_downPosition = Input.mousePosition;
			_downTime = Time.time;
		}

		public void OnPointerUp(PointerEventData eventData)
		{
			if (Time.time - _downTime > _maxDownTime || (eventData.position - _downPosition).magnitude > _maxMouseDelta)
				return;
			_selector.DeselectAll();
		}
	}
}