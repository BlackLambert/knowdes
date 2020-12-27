using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Knowdes.Prototype
{
    public class DeleteWorkspaceEntryOnClick : MonoBehaviour
    {
        [SerializeField]
        private Button _button = null;
        [SerializeField]
        private WorkspaceEntry _workspaceEntry;
        
        private Workspace _workspace = null;

        protected virtual void Start()
        {
            _workspace = FindObjectOfType<Workspace>();
            _button.onClick.AddListener(onClick);

        }

        protected virtual void OnDestroy()
        {
            _button.onClick.RemoveListener(onClick);
        }

        private void onClick()
		{
            _workspace.Remove(_workspaceEntry);
		}
    }
}