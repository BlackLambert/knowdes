
using UnityEngine;
using UnityEngine.EventSystems;

namespace Knowdes
{
	public class SetWWWCursor : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
	{
		private const int _priority = 10;

		private CursorStateMachine _stateMachine = null;
		private bool _cursorChanged = false;

		protected virtual void Start()
		{
			_stateMachine = FindObjectOfType<CursorStateMachine>();
		}
		protected virtual void OnDisable()
		{
			if (_cursorChanged)
				_stateMachine.RemoveState(this, CursorStateMachine.State.WWW);
			_cursorChanged = false;
		}

		public void OnPointerEnter(PointerEventData eventData)
		{
			if (_cursorChanged)
				return;
			_stateMachine.AddState(CursorStateMachine.State.WWW, this, _priority);
			_cursorChanged = true;
		}

		public void OnPointerExit(PointerEventData eventData)
		{
			if (!_cursorChanged)
				return;
			_stateMachine.RemoveState(this, CursorStateMachine.State.WWW);
			_cursorChanged = false;
		}
	}
}
