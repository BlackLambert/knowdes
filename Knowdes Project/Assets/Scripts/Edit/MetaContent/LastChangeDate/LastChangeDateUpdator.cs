using System;
using UnityEngine;

namespace Knowdes
{
    public class LastChangeDateUpdator : MonoBehaviour
    {
        [SerializeField]
        private EditPanel _editPanel;
        private Entry _currentEntry;

        protected virtual void Start()
		{
            _editPanel.OnEntryChanged += onEntryChanged;

        }

        protected virtual void OnDestroy()
		{
            _editPanel.OnEntryChanged -= onEntryChanged;
        }

		private void onEntryChanged()
		{
            if (_currentEntry != null)
                _currentEntry.Data.OnChanged -= updateLastChangeDate;
            _currentEntry = _editPanel.Entry;
            if (_currentEntry != null)
                _currentEntry.Data.OnChanged += updateLastChangeDate;
        }

		private void updateLastChangeDate()
        {
            LastChangeDateData lastChangeDateData = _currentEntry.Data.Get(MetaDataType.LastChangedDate) as LastChangeDateData;
            lastChangeDateData.Date = DateTime.Now;
        }
    }
}