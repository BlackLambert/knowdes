using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Knowdes
{
    public class UnderlineOnHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField]
        private TextMeshProUGUI _text = null;
		private FontStyles _formerStyle;

		protected virtual void Reset()
		{
			_text = GetComponent<TextMeshProUGUI>();
		}

		public void OnPointerEnter(PointerEventData eventData)
		{
			_formerStyle = _text.fontStyle;
			_text.fontStyle = FontStyles.Underline;
		}

		public void OnPointerExit(PointerEventData eventData)
		{
			_text.fontStyle = _formerStyle;
		}
	}
}