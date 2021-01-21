using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Knowdes
{
    public class FitWidthToText : MonoBehaviour
    {
        [SerializeField]
        private LayoutElement _layoutElement;
        [SerializeField]
        private TextMeshProUGUI _text;

        protected virtual void Start()
        {
            TMPro_EventManager.TEXT_CHANGED_EVENT.Add(onTextChanged);
            adjustLayout();
        }

        protected virtual void Reset()
		{
            _layoutElement = GetComponent<LayoutElement>();
            _text = GetComponent<TextMeshProUGUI>();

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
            float preferedWidth = _text.GetPreferredValues(_text.text).x;
            _text.autoSizeTextContainer = false;
            _layoutElement.minWidth = preferedWidth;
        }
	}
}