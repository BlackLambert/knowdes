using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Knowdes
{
    [RequireComponent(typeof(Graphics))]
    public class AdjustableSeparator : MonoBehaviour, IPointerDownHandler
    {
        private Panel _first = null;
        public Panel First
		{
            get => _first;
            set
			{
                _first = value;
            }
		}

        private Panel _second = null;
        public Panel Second
        {
            get => _second;
            set
            {
                _second = value;
            }
        }

        [SerializeField]
        private Orientation _orientation = Orientation.Horizontal;
        [SerializeField]
        private RectTransform _base;
        public RectTransform Base => _base;

        private bool _dragging = false;
        private float _minNormalized = 0;
        private float _maxNormalized = 0;
        private float _flexSum = 0;


		public void OnPointerDown(PointerEventData eventData)
		{
            _dragging = true;

            switch (_orientation)
            {
                case Orientation.Horizontal:
                    _minNormalized = Rect.PointToNormalized(_first.Parent.rect, 
                        new Vector2(_first.Rect.xMin + _first.RectTransform.localPosition.x + _first.LayoutElement.minWidth, 0)).x;
                    _maxNormalized = Rect.PointToNormalized(_first.Parent.rect,
                        new Vector2(_second.Rect.xMax + _second.RectTransform.localPosition.x - _second.LayoutElement.minWidth, 0)).x;
                    _flexSum = _first.LayoutElement.flexibleWidth + _second.LayoutElement.flexibleWidth;
                    break;
                case Orientation.Vertical:
                    _minNormalized = Rect.PointToNormalized(_first.Parent.rect, 
                        new Vector2(0, _second.Rect.yMin + _second.RectTransform.localPosition.y + _second.LayoutElement.minHeight)).y;
                    _maxNormalized = Rect.PointToNormalized(_first.Parent.rect, 
                        new Vector2(0, _first.Rect.yMax + _first.RectTransform.localPosition.y - _first.LayoutElement.minHeight)).y;
                    _flexSum = _first.LayoutElement.flexibleHeight + _second.LayoutElement.flexibleHeight;
                    break;
            }
        }

        protected virtual void Update()
		{   
            checkMouseButtonUp();
            updatePosition();
        }

        private void checkMouseButtonUp()
		{
            if (_dragging && Input.GetMouseButtonUp(0))
                _dragging = false;
        }

        private void updatePosition()
		{
            if (!_dragging)
                return;
            Vector2 rectPoint;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(_first.Parent, Input.mousePosition, GetComponentInParent<Canvas>().worldCamera, out rectPoint);
            Vector2 normalized = Rect.PointToNormalized(_first.Parent.rect, rectPoint);

            switch (_orientation)
			{
                case Orientation.Horizontal:
                    updateHorizontalPosition(normalized.x);
                    break;
                case Orientation.Vertical:
                    updateVerticalPosition(normalized.y);
                    break;
			}
		}

		private void updateHorizontalPosition(float normalizedXPosition)
		{
            float percent = Mathf.Clamp01((normalizedXPosition - _minNormalized) / (_maxNormalized - _minNormalized));
            float lerp = Mathf.Lerp(0.001f, _flexSum, percent);
            _first.LayoutElement.flexibleWidth = lerp;
            _second.LayoutElement.flexibleWidth = _flexSum - lerp;
        }

        private void updateVerticalPosition(float normalizedYPosition)
        {
            float percent = Mathf.Clamp01((normalizedYPosition - _minNormalized) / (_maxNormalized - _minNormalized));
            float lerp = Mathf.Lerp(0.001f, _flexSum, percent);
            _second.LayoutElement.flexibleHeight = lerp;
            _first.LayoutElement.flexibleHeight = _flexSum - lerp;
        }
    }
}