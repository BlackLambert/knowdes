using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knowdes.Prototype
{
	public class ExpandEntryVolumeOnSelectOfEntry : ExpandEntryVolumeOnSelect
	{
		[SerializeField]
		private Entry _entry = null;


		protected override EntryVolume EntryContent => _entry.Volume;
	}
}