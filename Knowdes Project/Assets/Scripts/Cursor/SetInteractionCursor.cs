using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Knowdes
{
    public class SetInteractionCursor : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
		private bool _stateSet = false;
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
			if (!_stateSet || _stateMachine.Locked)
				return;
			_stateMachine.SetState(CursorStateMachine.State.Default);
			_stateSet = false;
		}

		private void setInteraction()
		{
			if (_stateMachine.Locked)
				return;
			_stateMachine.SetState(CursorStateMachine.State.Interaction);
			_stateSet = true;
		}
	}
}