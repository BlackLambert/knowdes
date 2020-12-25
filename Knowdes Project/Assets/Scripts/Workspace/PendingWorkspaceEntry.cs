using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knowdes.Prototype
{
    public class PendingWorkspaceEntry : MonoBehaviour
    {
        [SerializeField]
        private WorkspaceEntry _workspaceEntry = null;
        public WorkspaceEntry WorkspaceEntry => _workspaceEntry;

        [SerializeField]
        private RectTransform _base = null;
        public RectTransform Base => _base;
    }
}