using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Knowdes
{
    public class TextLayoutElement : MonoBehaviour
    {
        [SerializeField]
        private LayoutElement _layoutElement;
        public LayoutElement LayoutElement => _layoutElement;
        [SerializeField]
        private TextMeshProUGUI _text;
        public TextMeshProUGUI Text => _text;

        [SerializeField]
        private bool _useMaxHeight = true;

        [SerializeField]
        private float _maxHeight = 300f;
        private float _currentMaxHeight = 0f;
        public float MaxHeight
        {
            get => _currentMaxHeight;
            set
            {
                _currentMaxHeight = value;
                adjustLayout();
            }
        }

        protected virtual void Start()
		{
            ResetMaxHeight();
            TMPro_EventManager.TEXT_CHANGED_EVENT.Add(onTextChanged);
        }

		private void onTextChanged(UnityEngine.Object obj)
		{
            if (obj != _text)
                return;
            adjustLayout();
        }

		private void adjustLayout()
		{
            _text.autoSizeTextContainer = true;
            float preferedHeight = _text.GetPreferredValues(_text.text).y;
            _text.autoSizeTextContainer = false;
            _layoutElement.preferredHeight = MaxHeight > 0 ? Mathf.Min(preferedHeight, MaxHeight) : preferedHeight;
        }

        public void ResetMaxHeight()
		{
            MaxHeight = _useMaxHeight ? _maxHeight : -1f;
        }
    }
}