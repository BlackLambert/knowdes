using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Knowdes
{
    public abstract class CursorOnDragSetter : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
		private CursorStateMachine _stateMachine = null;
		private bool _cursorChanged = false;

		protected abstract CursorStateMachine.State CursorState { get; }
		protected abstract int Priority { get; }

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
			if (!_cursorChanged || !Input.GetMouseButtonUp(0))
				return;
			_stateMachine.RemoveState(this, CursorState);
			_cursorChanged = false;
		}

		public void OnBeginDrag(PointerEventData eventData)
		{
			if (_cursorChanged)
				return;
			_stateMachine.AddState(CursorState, this, Priority);
			_cursorChanged = true;

			// Blocking the event is not intended.
			// Therefore this "rethrows" the event to the next IBeginDragHandler
			rethrowBeginDrag(eventData);
		}

		public void OnDrag(PointerEventData eventData)
		{
			// Blocking the event is not intended.
			// Therefore this "rethrows" the event to the next IDragHandler
			rethrowDrag(eventData);
		}

		public void OnEndDrag(PointerEventData eventData)
		{
			// Blocking the event is not intended.
			// Therefore this "rethrows" the event to the next IEndDragHandler
			rethrowEndDrag(eventData);
		}

		private void rethrowBeginDrag(PointerEventData eventData)
		{
			if (GetComponents<IBeginDragHandler>().Count() > 1)
				return;
			foreach (IBeginDragHandler handler in GetComponentsInParent<IBeginDragHandler>())
			{
				if (handler == (IBeginDragHandler)this)
					continue;
				handler.OnBeginDrag(eventData);
				break;
			}
		}

		private void rethrowDrag(PointerEventData eventData)
		{
			if (GetComponents<IDragHandler>().Count() > 1)
				return;
			foreach (IDragHandler handler in GetComponentsInParent<IDragHandler>())
			{
				if (handler == (IDragHandler)this)
					continue;
				handler.OnDrag(eventData);
				break;
			}
		}

		private void rethrowEndDrag(PointerEventData eventData)
		{
			if (GetComponents<IEndDragHandler>().Count() > 1)
				return;
			foreach (IEndDragHandler handler in GetComponentsInParent<IEndDragHandler>())
			{
				if (handler == (IEndDragHandler)this)
					continue;
				handler.OnEndDrag(eventData);
				break;
			}
		}
	}
}