using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Knowdes
{
    public class CursorStateMachine : MonoBehaviour
    {
        [SerializeField]
        private Texture2D _interactionImage = null;
        [SerializeField]
        private Texture2D _resizeHorizontalImage = null;
        [SerializeField]
        private Texture2D _resizeVerticalImage = null;
        [SerializeField]
        private Texture2D _draggingImage = null;
        [SerializeField]
        private Texture2D _wwwImage = null;
        [SerializeField]
        private Texture2D _editTextImage = null;

        public State CurrentState => _states[_states.Count - 1].State;

        private List<StateData> _states = new List<StateData>();


        /// <summary>
        /// Sets requested state if the current cursor state is not locked.
        /// </summary>
        /// <param name="newState"></param>
        public void AddState(State newState, object caller, int priority)
		{
            _states.Add(new StateData(newState, priority, caller));
            reorder();
        }

        public void RemoveState(object caller, State state)
		{
            StateData data = _states.FirstOrDefault(s => s.SetterObject == caller && s.State == state);
            if (data == null)
                throw new InvalidOperationException();
            _states.Remove(data);
            reorder();
		}

        private Texture2D getTexture(State state)
		{
            switch (state)
            {
                case State.Default:
                    return null;
                case State.Interaction:
                    return _interactionImage;
                case State.ResizeHorizontal:
                    return _resizeHorizontalImage;
                case State.ResizeVertical:
                    return _resizeVerticalImage;
                case State.Dragging:
                    return _draggingImage;
                case State.WWW:
                    return _wwwImage;
                case State.EditText:
                    return _editTextImage;
                default:
                    throw new NotImplementedException();
            }
        }

        private Vector2 getOffset(State state)
		{
            switch (state)
            {
                case State.Default:
                    return Vector2.zero;
                case State.Interaction:
                    return new Vector2(32,12);
                case State.ResizeHorizontal:
                case State.ResizeVertical:
                case State.Dragging:
                case State.WWW:
                case State.EditText:
                    return new Vector2(32, 32);
                default:
                    throw new NotImplementedException();
            }
        }

        private void reorder()
		{
            _states = _states.OrderBy(s => s.Priority).ToList();
            setState();
        }

        private void setState()
		{
            State state = _states.Count > 0 ? _states[_states.Count - 1].State : State.Default;
            Cursor.SetCursor(getTexture(state), getOffset(state), CursorMode.Auto);
        }


        public enum State
		{
            Default = 0,
            Interaction = 1,
            ResizeHorizontal = 2,
            ResizeVertical = 4,
            Dragging = 8,
            WWW = 16,
            EditText = 32
		}

        private class StateData
		{
            public State State { get; }
            public int Priority { get; }
            public object SetterObject { get; }

            public StateData(
                State state,
                int priority,
                object setterObject)
			{
                State = state;
                Priority = priority;
                SetterObject = setterObject;
            }
		}
    }
}