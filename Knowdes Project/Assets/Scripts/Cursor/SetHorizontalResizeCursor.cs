using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Knowdes
{
    public class SetHorizontalResizeCursor : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler, IPointerExitHandler
	{
		private const int _priority = 100;

		private bool _pressed = false;
		private bool _cursorChanged = false;
		private CursorStateMachine _stateMachine = null;

		protected virtual void Start()
		{
			_stateMachine = FindObjectOfType<CursorStateMachine>();
		}

		protected virtual void OnDisable()
		{
			if (_cursorChanged)
				_stateMachine.RemoveState(this, CursorStateMachine.State.Dragging);
			_cursorChanged = false;
		}

		protected virtual void Update()
		{
			checkPointerUp();
		}

		private void checkPointerUp()
		{
			if (!_pressed || !Input.GetMouseButtonUp(0))
				return;
			setDefault();
			_pressed = false;
		}

		private void setDefault()
		{
			if (!_cursorChanged)
				return;
			_stateMachine.RemoveState(this, CursorStateMachine.State.ResizeHorizontal);
			_cursorChanged = false;
		}

		private void setResize()
		{
			if (_cursorChanged)
				return;
			_stateMachine.AddState(CursorStateMachine.State.ResizeHorizontal, this, _priority);
			_cursorChanged = true;
		}

		public void OnPointerEnter(PointerEventData eventData)
		{
			setResize();
		}

		public void OnPointerExit(PointerEventData eventData)
		{
			if (_pressed)
				return;
			setDefault();
		}

		public void OnPointerDown(PointerEventData eventData)
		{
			_pressed = true;
		}
	}
}