using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knowdes.Prototype
{
    public class DeleteWorkspaceEntryOnDeleteKey : MonoBehaviour
    {
        [SerializeField]
        private UISelectable _selectable = null;
        [SerializeField]
        private WorkspaceEntry _entry = null;

		protected virtual void Update()
		{
            checkDelete();

        }

        private void checkDelete()
		{
            if (!_selectable.Selected || !Input.GetKeyDown(KeyCode.Delete))
                return;
            FindObjectOfType<Workspace>().Remove(_entry);
        }
    }
}