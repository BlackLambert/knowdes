using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knowdes
{
    public class SetWorkspaceEntryToEditPanelOnSelect : SetToEditPanelOnSelect
    {
        [SerializeField]
        private WorkspaceEntry _entry;

		protected override Entry Entry => _entry.LinkedEntry;
	}
}