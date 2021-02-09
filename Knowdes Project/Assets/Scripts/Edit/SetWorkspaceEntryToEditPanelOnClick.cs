using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Knowdes.Prototype
{
    public class SetWorkspaceEntryToEditPanelOnClick : MonoBehaviour
    {
        [SerializeField]
        private Button _button;
        [SerializeField]
        private WorkspaceEntry _entry;

        private EditPanel _editPanel;

        protected virtual void Start()
        {
            _editPanel = FindObjectOfType<EditPanel>();
            _button.onClick.AddListener(onClick);
        }

        protected virtual void OnDestroy()
        {
            _button.onClick.RemoveListener(onClick);
        }

        private void onClick()
        {

            _editPanel.InitWith(_entry.LinkedEntry);
        }
    }
}