using System;
using UnityEngine;

namespace Knowdes
{
	public class EntryVolumeContainer : VolumeContainer
	{
		[SerializeField]
		private Entry _entry;

		public override EntryVolume Volume => _entry.Volume;

		public override event Action OnVolumeSet;

		protected virtual void Start()
		{
			OnVolumeSet?.Invoke();
		}
	}
}