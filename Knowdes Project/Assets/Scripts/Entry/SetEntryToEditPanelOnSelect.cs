using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knowdes
{
	public class SetEntryToEditPanelOnSelect : SetToEditPanelOnSelect
	{
		protected override Entry Entry => _entry;
		[SerializeField]
		private Entry _entry;
	}
}