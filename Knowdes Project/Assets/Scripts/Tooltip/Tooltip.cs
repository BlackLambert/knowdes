using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Knowdes
{
    public class Tooltip : MonoBehaviour
    {
        [SerializeField]
        private RectTransform _base = null;
        public RectTransform Base => _base;
        [SerializeField]
        private TextMeshProUGUI _text = null;


        public void SetText(string text)
		{
            _text.text = text;
		}
    }
}
