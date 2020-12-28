using System;
using System.Collections;
using System.Collections.Generic;
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

        private object _lockingObject = null;
        public bool Locked => _lockingObject != null;

        public State CurrentState { get; private set; } = State.Default;


        /// <summary>
        /// Sets requested state if the current cursor state is not locked.
        /// </summary>
        /// <param name="newState"></param>
        public void SetState(State newState)
		{
            if (Locked)
                throw new InvalidOperationException("The StateMachine is Locked");
            if (newState == CurrentState)
                return;
            Cursor.SetCursor(getTexture(newState), getOffset(newState), CursorMode.Auto);
            CurrentState = newState;
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
                    return new Vector2(16,0);
                case State.ResizeHorizontal:
                case State.ResizeVertical:
                case State.Dragging:
                case State.WWW:
                    return new Vector2(16, 16);
                default:
                    throw new NotImplementedException();
            }
        }


        public void Lock(object lockingObject)
		{
            if (Locked)
                throw new InvalidOperationException();
            if (lockingObject == null)
                throw new ArgumentNullException();
            _lockingObject = lockingObject;
        }

        public void Unlock(object lockingObject)
		{
            if (!Locked)
                throw new InvalidOperationException();
            if (_lockingObject != lockingObject)
                throw new ArgumentException();
            _lockingObject = null;
		}



        public enum State
		{
            Default = 0,
            Interaction = 1,
            ResizeHorizontal = 2,
            ResizeVertical = 4,
            Dragging = 8,
            WWW = 16
		}
    }
}