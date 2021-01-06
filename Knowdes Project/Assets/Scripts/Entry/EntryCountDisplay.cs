using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Knowdes.Prototype
{
    public class EntryCountDisplay : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI _text = null;
        [SerializeField]
        private string _textContent = "({0})";
        [SerializeField]
        private EntriesList _entries = null;

        protected virtual void Start()
		{
            setText();
            _entries.OnCountChanged += setText;
        }

        protected virtual void OnDestroy()
		{

            _entries.OnCountChanged -= setText;
        }

        private void setText()
		{
            _text.text = string.Format(_textContent, _entries.Count);
        }
    }
}