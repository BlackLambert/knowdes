
using UnityEngine;
using UnityEngine.EventSystems;

namespace Knowdes
{
	public class SetEditTextCursor : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
	{
		private const int _priority = 1;

		private bool _cursorChanged = false;
		private CursorStateMachine _stateMachine = null;

		protected virtual void Start()
		{
			_stateMachine = FindObjectOfType<CursorStateMachine>();
		}

		protected virtual void OnDisable()
		{
			setDefault();
		}

		public void OnPointerEnter(PointerEventData eventData)
		{
			setEditText();
		}

		public void OnPointerExit(PointerEventData eventData)
		{
			setDefault();
		}

		private void setDefault()
		{
			if (!_cursorChanged)
				return;
			_stateMachine.RemoveState(this, CursorStateMachine.State.EditText);
			_cursorChanged = false;
		}

		private void setEditText()
		{
			if (_cursorChanged)
				return;
			_stateMachine.AddState(CursorStateMachine.State.EditText, this, _priority);
			_cursorChanged = true;
		}
	}
}
