
using System;
using UnityEngine;

namespace Knowdes
{
    public class EditPanel : ContextSubPanel
    {
        private Entry _entry;
        public Entry Entry 
        {
            get => _entry;
            private set
			{
                if (_entry == value)
                    return;
                _entry = value;
                OnEntryChanged?.Invoke();
            } 
        }

		public override ContextSubPanelType Type => ContextSubPanelType.Edit;

		public event Action OnEntryChanged;

        public void InitWith(Entry entry)
		{
            Entry = entry;
        }

        public void Clear()
		{
            Entry = null;
        }
	}
}