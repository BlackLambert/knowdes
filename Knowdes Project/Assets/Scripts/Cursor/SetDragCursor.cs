
using UnityEngine;
using UnityEngine.EventSystems;

namespace Knowdes
{
	public class SetDragCursor : MonoBehaviour, IBeginDragHandler
	{
		private CursorStateMachine _stateMachine = null;
		private bool _cursorChanged = false;

		protected virtual void Start()
		{
			_stateMachine = FindObjectOfType<CursorStateMachine>();
		}

		protected virtual void OnDestroy()
		{
			if(_cursorChanged)
				_stateMachine.Unlock(this);
		}

		protected virtual void Update()
		{
			checkPointerUp();
		}

		private void checkPointerUp()
		{
			if (!_cursorChanged || !Input.GetMouseButtonUp(0))
				return;
			_stateMachine.Unlock(this);
			_stateMachine.SetState(CursorStateMachine.State.Default);
			_cursorChanged = false;
		}

		public void OnBeginDrag(PointerEventData eventData)
		{
			if (_stateMachine.Locked)
				return;

			_stateMachine.SetState(CursorStateMachine.State.Dragging);
			_stateMachine.Lock(this);
			_cursorChanged = true;
		}
	}
}
