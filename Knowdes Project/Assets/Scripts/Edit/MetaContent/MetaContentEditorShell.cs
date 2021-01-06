using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Knowdes.Prototype
{
    public class MetaContentEditorShell : MonoBehaviour
    {
        [SerializeField]
        private RectTransform _base;
        public RectTransform Base => _base;

        [SerializeField]
        private RectTransform _editorHook;

        private MetaContentEditor _editor;
        public MetaData Data => _editor != null ? _editor.MetaData : null;

        public void SetEditor(MetaContentEditor editor)
		{
            if(_editor != null)
			{
                Destroy(_editor.Base.gameObject);
			}

            _editor = editor;

            if (_editor != null)
            {
                _editor.Base.SetParent(_editorHook);
                _editor.Base.localScale = Vector3.one;
            }
        }
    }
}