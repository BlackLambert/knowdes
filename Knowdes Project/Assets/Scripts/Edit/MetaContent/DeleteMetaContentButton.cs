using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Knowdes.Prototype
{
    public class DeleteMetaContentButton : MonoBehaviour
    {
        [SerializeField]
        private Button _button;
        [SerializeField]
        private MetaContentEditorShell _shell;

        private EditPanel _editPanel;

        protected virtual void Start()
		{
            _button.onClick.AddListener(delete);
            _editPanel = FindObjectOfType<EditPanel>();
        }

		protected virtual void OnDestroy()
		{
            _button.onClick.RemoveListener(delete);
        }
        private void delete()
        {
            _editPanel.Entry.Data.RemoveMetaData(_shell.Data);
        }

    }
}