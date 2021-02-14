using System;
using UnityEngine;

namespace Knowdes
{
    public abstract class VolumeContainer : MonoBehaviour
    {
        public abstract EntryVolume Volume { get; }
        public abstract event Action OnVolumeSet;
    }
}