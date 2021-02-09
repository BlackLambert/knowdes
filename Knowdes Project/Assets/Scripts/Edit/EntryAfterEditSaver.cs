
using UnityEngine;

namespace Knowdes
{
    public class EntryAfterEditSaver : MonoBehaviour
    {
		[SerializeField]
		private EditPanel _editPanel;
		private Entry _entry = null;
		private EntryDataRepository _entryDataRepository;

		protected virtual void Start()
		{
			_editPanel.OnEntryChanged += onEntryChanged;
			_entryDataRepository = FindObjectOfType<EntryDataRepository>();
		}

		protected virtual void OnDestroy()
		{
			_editPanel.OnEntryChanged -= onEntryChanged;
		}

		private void onEntryChanged()
		{
			if (_entry != null)
				_entryDataRepository.Save(_entry.Data);
			_entry = _editPanel.Entry;
		}
	}
}