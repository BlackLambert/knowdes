using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Knowdes
{
    public class ResizeHandle : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
    {
        [SerializeField]
        private RectTransform _target = null;
        [SerializeField]
        private Side _side = Side.Right;
        [SerializeField]
        private float _minSize = 200;
        [SerializeField]
        private float _maxSize = -1;

        private Vector2 _formerMinAnchor;
        private Vector2 _formerMaxAnchor;
        private Vector2 _formerPivot;
        private Vector2 _formerPosition;
        private float _startDistance = 0;
        private float _delta = 0;

        public void OnBeginDrag(PointerEventData eventData)
		{
            _formerMinAnchor = _target.anchorMin;
            _formerMaxAnchor = _target.anchorMax;
            _formerPivot = _target.pivot;

            Vector2 anchor = getAnchor(_side);
            _target.pivot = getPivotPos(_side);
            _target.anchorMin = anchor;
            _target.anchorMax = anchor;
            _formerPosition = _target.anchoredPosition;
            _target.anchoredPosition = Vector2.zero;
            _startDistance = getDistance(_side);
            _delta = 0;
        }

		public void OnEndDrag(PointerEventData eventData)
        {
            _target.pivot = _formerPivot;
            _target.anchorMin = _formerMinAnchor;
            _target.anchorMax = _formerMaxAnchor;
            
            resetPosition(_side);
        }

        public void OnDrag(PointerEventData eventData)
        {
            float distance = getDistance(_side);
            if (_minSize >= 0)
                distance = Mathf.Max(_minSize, distance);
            if (_maxSize >= 0)
                distance = Mathf.Min(_maxSize, distance);
            _target.SetSizeWithCurrentAnchors(getAxis(_side), distance);
            _delta = _startDistance - distance;
        }

		private RectTransform.Axis getAxis(Side side)
		{
			switch(side)
			{
                case Side.Bottom:
                case Side.Top:
                    return RectTransform.Axis.Vertical;
                case Side.Left:
                case Side.Right:
                    return RectTransform.Axis.Horizontal;
                default:
                    throw new NotImplementedException();
			}
		}

        private void resetPosition(Side side)
		{
            switch (side)
            {
                case Side.Bottom:
                    _target.anchoredPosition = new Vector2(_formerPosition.x, _formerPosition.y - _delta / 2);
                    break;
                case Side.Top:
                    _target.anchoredPosition = new Vector2(_formerPosition.x, _formerPosition.y + _delta / 2);
                    break;
                case Side.Left:
                    _target.anchoredPosition = new Vector2(_formerPosition.x + _delta / 2, _formerPosition.y);
                    break;
                case Side.Right:
                    _target.anchoredPosition = new Vector2(_formerPosition.x - _delta / 2, _formerPosition.y);
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

		private float getDistance(Side side)
		{
            Vector2 pointerPos;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(_target.parent as RectTransform, Input.mousePosition, GetComponentInParent<Canvas>().worldCamera, out pointerPos);

            switch (side)
			{
                case Side.Right:
                    return pointerPos.x - _target.localPosition.x;
                case Side.Left:
                    return _target.localPosition.x - pointerPos.x;
                case Side.Bottom:
                    return _target.localPosition.y - pointerPos.y;
                case Side.Top:
                    return pointerPos.y - _target.localPosition.y;
                default:
                    throw new NotImplementedException();
            }
		}

		private Vector2 getAnchor(Side side)
        {
            RectTransform _parentTransform = _target.parent as RectTransform;
            

            switch (side)
            {
                case Side.Right:
                    return Rect.PointToNormalized(_parentTransform.rect, new Vector2(_target.localPosition.x + _target.rect.xMin, _target.localPosition.y));
                case Side.Left:
                    return Rect.PointToNormalized(_parentTransform.rect, new Vector2(_target.localPosition.x + _target.rect.xMax, _target.localPosition.y));
                case Side.Bottom:
                    return Rect.PointToNormalized(_parentTransform.rect, new Vector2(_target.localPosition.x, _target.localPosition.y + _target.rect.yMax));
                case Side.Top:
                    return Rect.PointToNormalized(_parentTransform.rect, new Vector2(_target.localPosition.x, _target.localPosition.y + _target.rect.yMin));
                default:
                    throw new NotImplementedException();
            }
        }

        private Vector2 getPivotPos(Side side)
		{
            switch (side)
            {
                case Side.Right:
                    return new Vector2(0, 0.5f);
                case Side.Left:
                    return new Vector2(1, 0.5f);
                case Side.Bottom:
                    return new Vector2(0.5f, 1f);
                case Side.Top:
                    return new Vector2(0.5f, 0f);
                default:
                    throw new NotImplementedException();
            }
        }


        public enum Side
		{
            Top,
            Right,
            Bottom,
            Left
		}
    }
}