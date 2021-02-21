
using UnityEngine;

namespace Knowdes
{
    public class ClearEditPanelOnEntryDestroyed : MonoBehaviour
    {
        [SerializeField]
        private EditPanel _editPanel;
		private Entry _entry;

        protected virtual void Start()
		{
			_editPanel.OnEntryChanged += onEntryChanged;
			onEntryChanged();
		}

		protected virtual void OnDestroy()
		{
			_editPanel.OnEntryChanged -= onEntryChanged;
		}

		private void onEntryChanged()
		{
			removeListenersFromEntry();
			_entry = _editPanel.Entry;
			addListenersToEntry();
		}

		private void removeListenersFromEntry()
		{
			if (_entry != null)
				_entry.OnMarkedForDestruct -= clearEditPanel;
		}

		private void addListenersToEntry()
		{
			if (_entry == null)
				return;
			_entry.OnMarkedForDestruct += clearEditPanel;
		}

		private void clearEditPanel()
		{
			_editPanel.Clear();
		}
	}
}