using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Knowdes
{
    public class LoadingOverlay : MonoBehaviour
    {
        [SerializeField]
        private Canvas _canvas;
        [SerializeField]
        private CanvasGroup _canvasGroup;
		[SerializeField]
		private TextMeshProUGUI _loadingText;
		[SerializeField]
		private Image _loadingIcon;
		[SerializeField]
		private float _rotationSpeed = 180f;

        protected virtual void Awake()
		{
			Hide();
		}

		protected virtual void Update()
		{
			rotateIcon();
		}

        public void Show(string loadingText)
		{
			_loadingText.text = loadingText;
			_canvas.enabled = true;
		}

		public void Hide()
		{
			_canvas.enabled = false;
		}

		private void rotateIcon()
		{
			_loadingIcon.transform.Rotate(Vector3.forward, _rotationSpeed * Time.deltaTime);
		}
	}
}