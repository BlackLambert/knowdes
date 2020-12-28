
using UnityEngine;
using UnityEngine.EventSystems;

namespace Knowdes
{
	public class SetWWWCursor : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
	{
		private CursorStateMachine _stateMachine = null;
		private bool _cursorChanged = false;

		protected virtual void Start()
		{
			_stateMachine = FindObjectOfType<CursorStateMachine>();
		}

		public void OnPointerEnter(PointerEventData eventData)
		{
			if (_stateMachine.Locked)
				return;
			_stateMachine.SetState(CursorStateMachine.State.WWW);
			_cursorChanged = true;
		}

		public void OnPointerExit(PointerEventData eventData)
		{
			if (!_cursorChanged)
				return;
			_stateMachine.SetState(CursorStateMachine.State.Default);
			_cursorChanged = false;
		}
	}
}
