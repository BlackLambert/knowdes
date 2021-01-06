using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knowdes.Prototype
{
	public class ExpandEntryVolumeOnSelectOfWorkspaceEntry : ExpandEntryVolumeOnSelect
	{
		[SerializeField]
		private WorkspaceEntry _entry = null;

		protected override EntryVolume EntryContent => _entry.Content;
	}
}