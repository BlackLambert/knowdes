using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Knowdes.Prototype
{
    public class DeleteTagOnClick : MonoBehaviour
    {
        [SerializeField]
        private Button _button;
        [SerializeField]
        private TagEditor _tagEditor;

        protected virtual void Start()
		{
            _button.onClick.AddListener(removeTag);

        }

		protected virtual void OnDestroy()
		{
            _button.onClick.RemoveListener(removeTag);
        }

        private void removeTag()
        {
            _tagEditor.Data.RemoveTag(_tagEditor.Tag);
        }
    }
}