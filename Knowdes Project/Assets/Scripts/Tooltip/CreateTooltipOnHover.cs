using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Knowdes
{
    public class CreateTooltipOnHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        private const float _secondsTillDisplay = 0.66f;
        private const float _movementThreshold = 5;


       [SerializeField]
        private string _tooltipText = "XXX";

        private TooltipFactory _factory;
		private Vector3 _formerPointerPosition;
		private float _currentSecondsTillDisplay = _secondsTillDisplay;

		private Tooltip _tooltip = null;
		private bool _inArea = false;


		protected virtual void Start()
		{
            _factory = FindObjectOfType<TooltipFactory>();

        }

        protected virtual void OnDisable()
		{
			reset();
		}

		protected virtual void Update()
		{
			checkDisplayTooltip();
		}

		private void checkDisplayTooltip()
		{
			if (!_inArea)
				return;

			if ((_formerPointerPosition - Input.mousePosition).magnitude > _movementThreshold)
			{
				reset();
				return;
			}

			if (_tooltip == null)
			{
				_currentSecondsTillDisplay -= Time.deltaTime;
				if (_currentSecondsTillDisplay <= 0)
					_tooltip = _factory.Create(_tooltipText);
			}
		}

		private void reset()
		{
			_currentSecondsTillDisplay = _secondsTillDisplay;
			_formerPointerPosition = Input.mousePosition;
			if (_tooltip != null)
			{
				Destroy(_tooltip.Base.gameObject);
				_tooltip = null;
			}	
		}

		public void OnPointerEnter(PointerEventData eventData)
		{
			_inArea = true;
			_formerPointerPosition = Input.mousePosition;
		}

		public void OnPointerExit(PointerEventData eventData)
		{
			_inArea = false;
			reset();
		}
	}
}