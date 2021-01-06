using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Knowdes
{
    public class Panel : MonoBehaviour
    {
        [SerializeField]
        private LayoutElement _layoutElement = null;
        public LayoutElement LayoutElement => _layoutElement;
        [SerializeField]
        private RectTransform _rectTransform = null;
        public RectTransform RectTransform => _rectTransform;
        [SerializeField]
        private CanvasGroup _canvasGroup = null;

        [SerializeField]
        private RectTransform _base = null;
        public RectTransform Base => _base;

        public RectTransform Parent => _rectTransform.parent as RectTransform;
        public Rect Rect => _rectTransform.rect;

        public event Action<Panel> OnCollapsedChanged;
        private bool _collapsed = false;
        public bool Collapsed { 
            get => _collapsed; 
            private set
			{
                _collapsed = value;
                OnCollapsedChanged?.Invoke(this);
                updateCanvasGroup();
            }
        }

        protected virtual void Start()
		{
            updateCanvasGroup();
        }

        protected virtual void Reset()
		{
            _layoutElement = GetComponent<LayoutElement>();
            _canvasGroup = GetComponent<CanvasGroup>();

        }

        public void Collapse()
		{
            if (Collapsed)
                throw new InvalidOperationException();

            _layoutElement.enabled = false;
            Collapsed = true;
            LayoutRebuilder.ForceRebuildLayoutImmediate(Parent);
        }

        public void Expand()
		{
            if (!Collapsed)
                throw new InvalidOperationException();
            _layoutElement.enabled = true;
            Collapsed = false;
            LayoutRebuilder.ForceRebuildLayoutImmediate(Parent);
        }

        private void updateCanvasGroup()
		{
            _canvasGroup.alpha = Collapsed ? 0 : 1;
            _canvasGroup.blocksRaycasts = !Collapsed;
		}
    }
}