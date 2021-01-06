
using System;
using UnityEngine;

namespace Knowdes.Prototype
{
    public class EditPanel : ContextSubPanel
    {


        private Entry _entry = null;
        public Entry Entry => _entry;

		public override ContextSubPanelType Type => ContextSubPanelType.Edit;

		public event Action OnEntryChanged;

        public void SetEntry(Entry entry)
		{
            if (_entry == entry)
                return;
            if (_entry != null)
                _entry.OnMarkedForDestruct -= onDestruct;
            _entry = entry;
            OnEntryChanged?.Invoke();
            if (_entry != null)
                _entry.OnMarkedForDestruct += onDestruct;
        }

		private void onDestruct(Entry entry)
		{
            if (_entry != entry)
                return;
            SetEntry(null);
		}
	}
}