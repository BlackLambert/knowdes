using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Knowdes
{
    public class TemporaryNotification : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField]
        private GameObject _base;
        [SerializeField]
        private TextMeshProUGUI _messageText;

        private float _displayTimeLeft = 0;
        private bool _isDisplayTimeLeft => _displayTimeLeft > 0;
        private bool _displaying = false;

        public event Action OnHidden;

        public void Display(string message, float displayTime)
		{
			validateDisplayTimeInput(displayTime);
			setDisplayTimeLeft(displayTime);
			setMessageText(message);
			show();
		}

		public void Destroy()
		{
            Destroy(_base.gameObject);
		}

		protected virtual void Awake()
		{
            hide();
        }

        protected virtual void Update()
		{
			updateDisplayTime();
            checkHide();
        }

		private void updateDisplayTime()
		{
			if (_displaying)
			    reduceDisplayTimeLeft(Time.deltaTime);
		}

		private void checkHide()
		{
            if (!_isDisplayTimeLeft)
			    hide();
		}

		private static void validateDisplayTimeInput(float displayTime)
        {
            if (displayTime <= 0)
                throw new ArgumentException("The display time has to be larger than 0");
        }

        private void reduceDisplayTimeLeft(float delta)
        {
            _displayTimeLeft -= delta;
        }

        private void show()
		{
            _base.SetActive(true);
            _displaying = true;
        }

        private void hide()
		{
            _base.SetActive(false);
            OnHidden?.Invoke();
            _displaying = false;
        }

        private void setDisplayTimeLeft(float displayTime)
        {
            _displayTimeLeft = displayTime;
        }

        private void setMessageText(string message)
        {
            _messageText.text = message;
        }

		public void OnPointerDown(PointerEventData eventData)
		{
            hide();
		}
	}
}