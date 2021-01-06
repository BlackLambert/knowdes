using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Knowdes
{
    public class ContentTypeTextSetter : MonoBehaviour
    {
        [SerializeField]
        private EntryVolume _content;
        [SerializeField]
        private TextMeshProUGUI _text;

        protected virtual void Start()
		{
            updateText();
            _content.Data.OnContentChanged += updateText;
        }

        protected virtual void OnDestroy()
		{

            _content.Data.OnContentChanged -= updateText;
        }

        private void updateText()
		{
            _text.text = _content.Data.Content != null ? _content.Data.Content.Type.GetName() : ContentDataType.Unset.GetName();
		}
    }
}