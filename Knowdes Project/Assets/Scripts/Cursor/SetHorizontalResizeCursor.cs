using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Knowdes
{
    public class SetHorizontalResizeCursor : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler, IPointerExitHandler
    {
		private bool _pressed = false;
		private bool _stateChanged = false;
		private CursorStateMachine _stateMachine = null;

		protected virtual void Start()
		{
			_stateMachine = FindObjectOfType<CursorStateMachine>();
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
			if (!_stateChanged)
				return;
			_stateMachine.Unlock(this);
			_stateMachine.SetState(CursorStateMachine.State.Default);
			_stateChanged = false;
		}

		private void setResize()
		{
			if (_stateMachine.Locked)
				return;
			_stateMachine.SetState(CursorStateMachine.State.ResizeHorizontal);
			_stateMachine.Lock(this);
			_stateChanged = true;
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