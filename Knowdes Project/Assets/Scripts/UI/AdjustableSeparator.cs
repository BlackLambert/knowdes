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
        [Range(0, 1.0f)]
        private float _firstMinNormalizedSize = 0;
        [SerializeField]
        private RectTransform _second = null;
        [SerializeField]
        [Range(0, 1.0f)]
        private float _secondMinNormalizedSize = 0;
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
            Vector2 normalized = Rect.PointToNormalized(_parentContainer.rect, rectPoint);
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
            float newX = Mathf.Clamp(normalizedXPosition, _firstMinNormalizedSize, 1 - _secondMinNormalizedSize);
            _first.anchorMax = new Vector2(newX, _first.anchorMax.y);
            _second.anchorMin = new Vector2(newX, _second.anchorMin.y);
            _first.sizeDelta = new Vector2(0, 0);
            _second.sizeDelta = new Vector2(0, 0);
        }

        private void updateVerticalPosition(float normalizedYPosition)
        {
            float newY = Mathf.Clamp(normalizedYPosition, _secondMinNormalizedSize, 1 - _firstMinNormalizedSize);
            _first.anchorMin = new Vector2(_first.anchorMin.x, newY);
            _second.anchorMax = new Vector2(_second.anchorMax.x, newY);
            _first.sizeDelta = new Vector2(0, 0);
            _second.sizeDelta = new Vector2(0, 0);
        }

        public enum Orientation
		{
            Horizontal = 0,
            Vertical = 1
		}
    }
}