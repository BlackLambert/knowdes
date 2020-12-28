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
        [SerializeField]
        private RectTransform _first = null;
        [SerializeField]
        private float _firstMinSize = 300;
        [SerializeField]
        private RectTransform _second = null;
        [SerializeField]
        private float _secondMinSize = 300;
        [SerializeField]
        private RectTransform _parentContainer = null;
        [SerializeField]
        private Orientation _orientation = Orientation.Horizontal;

        private bool _dragging = false;

		public void OnPointerDown(PointerEventData eventData)
		{
            _dragging = true;
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
            RectTransformUtility.ScreenPointToLocalPointInRectangle(_parentContainer, Input.mousePosition, GetComponentInParent<Canvas>().worldCamera, out rectPoint);
            Vector2 normalized;

            switch (_orientation)
			{
                case Orientation.Horizontal:
                    if (!changeAllowedHorizontal(rectPoint))
                        return;
                    normalized = Rect.PointToNormalized(_parentContainer.rect, rectPoint);
                    updateHorizontalPosition(normalized.x);
                    break;
                case Orientation.Vertical:
                    if (!changeAllowedVertical(rectPoint))
                        return;
                    normalized = Rect.PointToNormalized(_parentContainer.rect, rectPoint);
                    updateVerticalPosition(normalized.y);
                    break;
			}
		}

		private void updateHorizontalPosition(float normalizedXPosition)
		{
            float firstMinX = Rect.PointToNormalized(_parentContainer.rect, new Vector2(_first.rect.xMin + _first.localPosition.x + _firstMinSize, _first.localPosition.y)).x;
            float secondMinX = Rect.PointToNormalized(_parentContainer.rect, new Vector2(_second.rect.xMax + _second.localPosition.x - _secondMinSize, _first.localPosition.y)).x;
            float newX = Mathf.Clamp(normalizedXPosition, firstMinX, secondMinX);
            _first.anchorMax = new Vector2(newX, _first.anchorMax.y);
            _second.anchorMin = new Vector2(newX, _second.anchorMin.y);
            _first.sizeDelta = new Vector2(0, 0);
            _second.sizeDelta = new Vector2(0, 0);
        }

        private void updateVerticalPosition(float normalizedYPosition)
        {
            float newY = Mathf.Clamp(normalizedYPosition, _secondMinSize, 1 - _firstMinSize);
            _first.anchorMin = new Vector2(_first.anchorMin.x, newY);
            _second.anchorMax = new Vector2(_second.anchorMax.x, newY);
            _first.sizeDelta = new Vector2(0, 0);
            _second.sizeDelta = new Vector2(0, 0);
        }

        private bool changeAllowedVertical(Vector2 point)
		{
            return true;
        }

        private bool changeAllowedHorizontal(Vector2 point)
        {
            float leftSideX = _first.rect.xMin + _first.localPosition.x;
            float leftSize = point.x - leftSideX;
            float rightSideX = _second.rect.xMax + _second.localPosition.x;
            float rightSize = rightSideX - point.x;

            return leftSize >= _firstMinSize && rightSize >= _secondMinSize;
        }

        public enum Orientation
		{
            Horizontal = 0,
            Vertical = 1
		}
    }
}