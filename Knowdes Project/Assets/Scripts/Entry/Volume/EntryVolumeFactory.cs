using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knowdes
{
    public class EntryVolumeFactory : MonoBehaviour
    {
        [SerializeField]
        private EntryVolume _volumePrefab = null;

        public EntryVolume Create(EntryVolume volumeToCopy)
		{
            EntryVolume result = Instantiate(_volumePrefab);
            result.Data = volumeToCopy.Data;
            result.Expanded = volumeToCopy.Expanded;
            return result;
        }
    }
}