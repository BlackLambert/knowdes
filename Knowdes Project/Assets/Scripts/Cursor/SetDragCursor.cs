
using UnityEngine;
using UnityEngine.EventSystems;

namespace Knowdes
{
	public class SetDragCursor : MonoBehaviour, IBeginDragHandler
	{
		private const int _priority = 100;

		private CursorStateMachine _stateMachine = null;
		private bool _cursorChanged = false;

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
			_stateMachine.RemoveState(this, CursorStateMachine.State.Dragging);
			_cursorChanged = false;
		}

		public void OnBeginDrag(PointerEventData eventData)
		{
			if (_cursorChanged)
				return;
			_stateMachine.AddState(CursorStateMachine.State.Dragging, this, _priority);
			_cursorChanged = true;
		}
	}
}
