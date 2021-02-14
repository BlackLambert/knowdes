using System;
using UnityEngine;

namespace Knowdes
{
    public class WorkspaceEntryVolumeContainer : VolumeContainer
    {
        [SerializeField]
        private WorkspaceEntry _entry;

		public override EntryVolume Volume => _entry.Volume;

		public override event Action OnVolumeSet;

        protected virtual void Start()
		{
			if (_entry.Volume == null)
				_entry.OnVolumeSet += onVolumeSet;
		}

		private void onVolumeSet()
		{
			OnVolumeSet?.Invoke();
			_entry.OnVolumeSet -= onVolumeSet;
		}
	}
}