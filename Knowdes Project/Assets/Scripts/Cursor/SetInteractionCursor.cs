using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Knowdes
{
    public class SetInteractionCursor : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
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
			setInteraction();
		}

		public void OnPointerExit(PointerEventData eventData)
		{
			setDefault();
		}

		private void setDefault()
		{
			if (!_cursorChanged)
				return;
			_stateMachine.RemoveState(this, CursorStateMachine.State.Interaction);
			_cursorChanged = false;
		}

		private void setInteraction()
		{
			if (_cursorChanged)
				return;
			_stateMachine.AddState(CursorStateMachine.State.Interaction, this, _priority);
			_cursorChanged = true;
		}
	}
}